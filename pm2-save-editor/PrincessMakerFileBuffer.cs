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
    class PrincessMakerFileBuffer
    {
        const int PM2_SAVE_FILE_SIZE = 8192;
        byte[] pm2SaveFileBytes;
        const int CHECKSUM_OFFSET = 0x1B4C; // temporary storage of checksum offset here - will ideally pull the offset from full offset list later

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

            if (pm2SaveFileInfo.Length != PM2_SAVE_FILE_SIZE)
            {
                MessageBox.Show("File " + fileName + " did not match the expected file size. Are you sure it is a PM2 save file?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            pm2SaveFileBytes = new byte[PM2_SAVE_FILE_SIZE];

            pm2SaveFileStream.Read(pm2SaveFileBytes, 0, PM2_SAVE_FILE_SIZE);

            pm2SaveFileStream.Close();

            return true;
        }

        /// <summary>
        /// Write a loaded Princess Market 2 save file to disk
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

            int newChecksum = Checksum.CalculateChecksum(pm2SaveFileBytes);
            WriteAtOffset(CHECKSUM_OFFSET, 4, BitConverter.GetBytes(newChecksum));

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

    }
}
