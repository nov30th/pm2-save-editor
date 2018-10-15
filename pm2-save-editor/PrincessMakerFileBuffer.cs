using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace pm2_save_editor
{
    
    /// <summary>
    /// Basic interface for interacting with Princess Maker 2 save files
    /// </summary>
    public class PrincessMakerFileBuffer
    {
        public enum Version { Unknown, EnglishRefine, JapaneseRefine, DOS }

        byte[] pm2SaveFileBytes;
        Version workingVersion = Version.EnglishRefine; // default is EnglishRefine
        Dictionary<Stat, IStatContainer> statDictionary;
        int oldChecksum;
        bool forceFullChecksum = false;

        /// <summary>
        /// Read a Princess Maker 2 save file into memory
        /// </summary>
        /// <param name="fileName">Name of the file to be read</param>
        /// <returns>Retrun true on successful load, return false on failed load</returns>
        public bool LoadFile(string fileName)
        {

            FileInfo pm2SaveFileInfo = new FileInfo(fileName);

            if (!pm2SaveFileInfo.Exists)
            {
                MessageBox.Show("Could not find file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FileStream pm2SaveFileStream = null;

            while (pm2SaveFileStream == null)
            {
                try
                {
                    pm2SaveFileStream = pm2SaveFileInfo.Open(FileMode.Open);
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show("Could not open file " + fileName + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result != DialogResult.Retry)
                    {
                        return false;
                    }
                }
            }

            int fileSize = (int)pm2SaveFileInfo.Length;

            pm2SaveFileBytes = new byte[fileSize];

            pm2SaveFileStream.Read(pm2SaveFileBytes, 0, fileSize);

            pm2SaveFileStream.Close();

            workingVersion = CheckVersion();

            if (workingVersion == Version.DOS)
            {
                String errorString =
                    "This files appears to have been produced by a DOS version of the game or other non-Refine version of the game. Non-Refine versions of Princess Maker 2 are not supported at this time.";
                MessageBox.Show(errorString, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1); // is a hard exit too extreme for errors like this?
            }

            if (workingVersion == Version.Unknown)
            {
                String errorString =
                    "The checksum of the file could not be computed successfully. This could mean that:\n\n" +
                    "-The file was not a valid PM2 save file\n" +
                    "-The file was corrupt\n" +
                    "-The file belongs to a version of the game which is incompatible with the editor\n";

                MessageBox.Show(errorString, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // need some form of version checking here
            statDictionary = BuildStatDictionary();

            oldChecksum = GetOrignalChecksum() - CalculatePartialChecksum();

            var backupCreator = new BackupCreator();
            backupCreator.CreateBackupFile(fileName, this, pm2SaveFileBytes);

            return true;
        }



        private Version CheckVersion()
        {

            // Check EnglishRefine
            if (CheckChecksum())
            {
                return Version.EnglishRefine; // We have a complete map of variable size and type of EnglishRefine, so checksumming the whole file and checking it against the expected checksum works very well as a test 
            }

            // Check DOS or other non-Refine versions
            const string DOSHeaderString = "PM2/ver1.02";
            int DOSHeaderSize = DOSHeaderString.Length;
            byte[] DOSHeader = ASCIIEncoding.GetEncoding("ascii").GetBytes(DOSHeaderString); // DOS files seem to have a header where Refine files do not. I am unsure if any other versions of the game produce files with this header, but it seems to be enough to rule out any Refine versions.
            byte[] possibleHeader = new byte[DOSHeaderSize];
            Array.Copy(pm2SaveFileBytes, 0, possibleHeader, 0, DOSHeaderSize);
            if (DOSHeader.SequenceEqual(possibleHeader))
            {
                return Version.DOS;
            }

            return Version.Unknown;

        }

        /// <summary>
        /// Determine whether or not the file is a valid PM2 file by calculating the files checksum and comparing it to the expected checksum taken from the file
        /// </summary>
        private bool CheckChecksum()
        {

            if (pm2SaveFileBytes.Length < Checksum.ENGLISH_REFINE_CHECKSUM_OFFSET)
            {
                return false;
            }

            int expectedChecksum = BitConverter.ToInt32(ReadAtOffset(Checksum.ENGLISH_REFINE_CHECKSUM_OFFSET, 4), 0);
            int calculatedChecksum = Checksum.CalculateChecksum(pm2SaveFileBytes,  workingVersion);

            if (expectedChecksum != calculatedChecksum)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Write a loaded Princess Marker 2 save file to disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool SaveFile(string fileName)
        {
            // This function is very basic and deviates from the planned model regarding how to handle parts of the file
            // Once the planned model is setup up the calculation and updating of checksum should be moved out of this function and integrated with it
            FileStream fs;

            try
            {
                fs = new FileStream(fileName, FileMode.Create);
            }
            catch
            {
                MessageBox.Show("Error creating file " + fileName);
                return false;
            }

            RunSumContainers();

            int newChecksum;

            if (forceFullChecksum)
            {
                newChecksum = Checksum.CalculateChecksum(pm2SaveFileBytes, workingVersion);
            }
            else
            {
                newChecksum = oldChecksum + CalculatePartialChecksum();
            }

            // Again, the actual workings and responsilities of the checksum are not entirely clear due to its unique function
            if (workingVersion == Version.EnglishRefine)
            {
                WriteAtOffset(Checksum.ENGLISH_REFINE_CHECKSUM_OFFSET, 4, BitConverter.GetBytes(newChecksum));
            }

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(pm2SaveFileBytes);

            bw.Close();
            fs.Close();

            return true;
        }

        /// <summary>
        /// Iterate through stat containers and find any sum containers that need to fired off while preparing to save a file
        /// </summary>
        private void RunSumContainers()
        {
            // Ideally in the future this will be written more effeciently with dictionaries
            foreach (IStatContainer container in statDictionary.Values)
            {
                if (container.GetStatType() == StatTypes.Sum)
                {
                    SumStatContainer sumContainer = container as SumStatContainer;
                    sumContainer.SumStats(statDictionary);
                }
            }
        }

        /// <summary>
        /// Read bytes from file in memory to a specified buffer
        /// </summary>
        /// <param name="offset">Offset in file to read from</param>
        /// <param name="size">Size of requested data</param>
        /// <returns>Byte array containing requested data</returns>
        public byte[] ReadAtOffset(int offset, int size)
        {
            byte[] bytesRead = new byte[size];
            Array.Copy(pm2SaveFileBytes, offset, bytesRead, 0, size);
            return bytesRead;
        }

        /// <summary>
        /// Write specified bytes to the file in memory
        /// </summary>
        /// <param name="offset">Offset in file to write to</param>
        /// <param name="size">Size of data to write</param>
        /// <param name="bytesToWrite">Data to write</param>
        public void WriteAtOffset(int offset, int size, byte[] bytesToWrite)
        {
            Array.Copy(bytesToWrite, 0, pm2SaveFileBytes, offset, size);
        }

        /// <summary>
        /// Build a dictionary of StatContainers based on the contents of this buffer
        /// </summary>
        /// <returns>Dictionary of publically accessible stats</returns>
        private Dictionary<Stat, IStatContainer> BuildStatDictionary()
        {
            Dictionary<Stat, IStatContainer> statDictionary = new Dictionary<Stat, IStatContainer>();

            var dictEnumerator = StatInitalizationValues.englishRefineStatInitalizationMap.GetEnumerator(); // should be removed at the earliest opportunity and replaced by below funtionality - only exists to force compilaiton

            if (workingVersion == Version.EnglishRefine)
            {
               dictEnumerator  = StatInitalizationValues.englishRefineStatInitalizationMap.GetEnumerator();
            }
            else
            {
                MessageBox.Show("Remember to change the dictionary in BuildStatDictionary when trying the new version!");
                Environment.Exit(1);
            }

            while (dictEnumerator.MoveNext() != false)
            {
                var currentStat = dictEnumerator.Current.Key;
                var currentValues = dictEnumerator.Current.Value;
                var builtStat = StatFactory.BuildStat<IStatContainer>(currentStat, currentValues, this);
                statDictionary[currentStat] = builtStat;
            }

            return statDictionary;
        }

        public Dictionary<Stat, IStatContainer> GetStatDictionary()
        {
            return statDictionary;
        }

        /// <summary>
        /// Attempt to fetch a specific stat from the buffers stat dictionary
        /// </summary>
        /// <param name="desiredStat">The desired stat</param>
        /// <returns>The container corresponding to the desired stat if found, null if not found</returns>
        public IStatContainer GetStat(Stat desiredStat)
        {
            IStatContainer foundContainer;
            bool success;

            success = statDictionary.TryGetValue(desiredStat, out foundContainer);

            if (success)
            {
                return foundContainer;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Calcuate the checksum value of supported stats
        /// </summary>
        /// <returns>Calcuated checksum</returns>
        /// <remarks>
        /// Checksum is found by adding the contents of all stats together. By subtracting the contents of supported stats from the checksum at load time and readding them at save time, we can calcuate partial checksums and create valid PM2 files without having to support every stat or write specific checksum fuctions for different file layouts.
        /// </remarks>
        private int CalculatePartialChecksum()
        {
            int partialChecksum = 0;

            foreach (IStatContainer container in statDictionary.Values)
            {
                partialChecksum += container.GetChecksum();
            }

            return partialChecksum;
        }

        /// <summary>
        /// Get the original file checksum from the file
        /// </summary>
        /// <returns>Found checksum</returns>
        private int GetOrignalChecksum()
        {
            return BitConverter.ToInt32(ReadAtOffset(Checksum.ENGLISH_REFINE_CHECKSUM_OFFSET, 4), 0);
        }

        /// <summary>
        /// Force the current buffer to use the full checksum calculation over partial checksum calcuation
        /// </summary>
        /// <remarks>
        /// Most useful when attempting to edit parts of the file that do not have registered stat containers
        /// </remarks>
        public void EnableForceFullChecksum()
        {
            if (workingVersion != Version.EnglishRefine)
            {
                MessageBox.Show("Full Checksum is only compatible with English Refine.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
            {
                forceFullChecksum = true;
            }
        }

        /// <summary>
        /// Disable force full checksum
        /// </summary>
        public void DisableForceFullChecksum()
        {
            forceFullChecksum = false;
        }

    }
}
