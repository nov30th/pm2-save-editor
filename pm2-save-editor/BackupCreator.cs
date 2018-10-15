using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace pm2_save_editor
{
    class BackupCreator
    {

        private PrincessMakerFileBuffer workingBuffer;
        private const string backupDir = "Backups";

        private string ExtractFileNameFromPath(string fileName)
        {
            Regex regex = new Regex(@"[^/\\]+\.GNX$");
            Match match = regex.Match(fileName);
            if (match.Success)
            {
                return match.Value;
            }
            return "UNKNOWN.GNX";
        }

        private string GetStatContents(Stat stat)
        {
            string statContents = workingBuffer.GetStat(stat)?.GetContents();
            if (statContents == null)
            {
                return "UKNOWN";
            }
            return statContents;
        }

        private string GetName()
        {
            string daughtersName = GetStatContents(Stat.DaughtersName).Replace("\0", ""); // if the null terminators are not removed they will show up in the actual string
            string fathersName = GetStatContents(Stat.FathersName).Replace("\0", "");
            string nameString = string.Format("{0} {1}", daughtersName, fathersName);
            return nameString;
        }

        private string GetDate()
        {
            string year = GetStatContents(Stat.Year);
            string month = GetStatContents(Stat.Month);
            string date = GetStatContents(Stat.Date);
            string dateString = string.Format("{0}-{1}-{2}", year, month, date);
            return dateString;
        }

        public void CreateBackupFile(string fileName, PrincessMakerFileBuffer workingBuffer, byte[] file)
        {

            this.workingBuffer = workingBuffer;

            string realFileName = ExtractFileNameFromPath(fileName);
            string namePart = GetName();
            string datePart = GetDate();
            string currentDate = DateTime.Now.ToString();
            currentDate = currentDate.Replace("/", "-"); // stripping troublesome characters
            currentDate = currentDate.Replace(":", "-"); // NTFS does not like colons in file names

            string fullFileName = string.Format("[{0}]_[{1}]_[{2}]_[{3}].GNX", realFileName, namePart, datePart, currentDate);

            try
            {
                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }

                string backupFilePath = Path.Combine(backupDir, fullFileName);

                FileStream fs = new FileStream(backupFilePath, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(file);

                bw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error when attempting to create a backup of the working file\n\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
