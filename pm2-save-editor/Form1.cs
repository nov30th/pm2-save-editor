using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{
    public partial class Form1 : Form
    {

        private bool fileHasBeenOpened = false;
        private bool unsavedChangesPresent = false; // not yet impelemeted
        private PrincessMakerFileBuffer workingFile;
        private string workingFileName = "";
        private Dictionary<string, object> panelDictionary;

        public Form1()
        {
            InitializeComponent();

            // Disabling Save/Save As until a file is opened
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

            workingFile = new PrincessMakerFileBuffer();

            openFileDialog1.Filter = "PM2 Save Files|*.GNX";
            saveFileDialog1.Filter = "PM2 Save Files|*.GNX";

            HideTabPages();

        }

        private void HideTabPages()
        {
            tabControl1.Enabled = false; // would have preffered to have kept the tabs enable but the child controls disabled but without having to manually cycle through ever control. might try again later.
        }

        private void ShowTabPages()
        {
            tabControl1.Enabled = true;
        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "") // user did not choose a file
            {
                return;
            }

            if (workingFile.LoadFile(openFileDialog1.FileName))
            {
                fileHasBeenOpened = true;
                saveAsToolStripMenuItem.Enabled = true; // perhaps these should be in seperate functions
                saveToolStripMenuItem.Enabled = true;
                workingFileName = openFileDialog1.FileName;
                ShowTabPages();
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workingFile.SaveFile(workingFileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";

            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName == "")
            {
                return;
            }

            if (!workingFile.SaveFile(saveFileDialog1.FileName))
            {
                MessageBox.Show("There are was an error when attempting to save the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileHasBeenOpened)
            {
                var result = MessageBox.Show("Are you sure you wish to exit? Any unsaved changes will be lost.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
