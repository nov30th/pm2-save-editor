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
    /// Main interface with which to interact with Princes Maker 2 save files
    /// </summary>
    class PrincessMaker
    {
        const int PM2_SAVE_FILE_SIZE = 8192;

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

            byte[] pm2SaveFileBytes = new byte[PM2_SAVE_FILE_SIZE];

            pm2SaveFileStream.Read(pm2SaveFileBytes, 0, PM2_SAVE_FILE_SIZE);

            MemoryStream ms = new MemoryStream(pm2SaveFileBytes);
            BinaryReader br = new BinaryReader(ms);

            StringStatContainer daughtersName = new StringStatContainer(48, 0x74, 8, 1, br);

            MessageBox.Show(daughtersName.GetString());


            ms.Close();
            br.Close();

            pm2SaveFileStream.Close();

            int checksum = Checksum.CalculateChecksum(pm2SaveFileBytes);


            return true;
        }

    }
}
