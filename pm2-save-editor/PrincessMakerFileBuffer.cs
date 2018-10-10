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
        public enum Version { EnglishRefine, JapaneseRefine }

        byte[] pm2SaveFileBytes;
        Version workingVersion = Version.EnglishRefine;


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

            if (!CheckChecksum())
            {
                String errorString =
                    "The checksum of the file could not be computed successfully. This could mean that:\n\n" +
                    "-The file was not a valid PM2 save file\n" +
                    "-The file was corrupt\n" + 
                    "-The file belongs to a version of the game which is incompatible with the editor\n";
                    
                MessageBox.Show(errorString, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            FileStream fs = new FileStream(fileName + "_BAK", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(pm2SaveFileBytes);

            bw.Close();
            fs.Close();

            return true;
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

            int newChecksum = Checksum.CalculateChecksum(pm2SaveFileBytes, workingVersion);

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
        public Dictionary<Stat, StatContainer> BuildStatDictionary()
        {
            Dictionary<Stat, StatContainer> statDictionary = new Dictionary<Stat, StatContainer>();

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
                var builtStat = StatFactory.BuildStat<StatContainer>(currentStat, currentValues, this);
                statDictionary[currentStat] = builtStat;
            }

            return statDictionary;
        }

    }
}
