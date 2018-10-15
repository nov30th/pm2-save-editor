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
        Dictionary<Stat, IStatContainer> statDictionary;
        private string workingFileName = "";

        public Form1()
        {
            InitializeComponent();

            this.Text = "Princess Maker 2 Save Editor";

            // Disabling Save/Save As until a file is opened
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

            workingFile = new PrincessMakerFileBuffer();

            openFileDialog1.Filter = "PM2 Save Files|*.GNX";
            saveFileDialog1.Filter = "PM2 Save Files|*.GNX";

            this.FormClosing += new FormClosingEventHandler(FormCloseCatcher);

            HideTabPages();

        }

        public IStatContainer RequestStat(Stat requestedStat)
        {
            bool success;
            IStatContainer foundStat;

            success = statDictionary.TryGetValue(requestedStat, out foundStat);

            if (!success)
            {
                MessageBox.Show("Could not find stat with name of " + requestedStat.ToString());
                Environment.Exit(1);
            }

            return foundStat;

        }

        // i really wanted to have this be part of the initalization or constructor of the binded text boxes but i couldn't find a way to do it without running into errors
        private void InitalizeTabChildControls()
        {
            foreach (var tabPage in tabControl1.TabPages)
            {
                var tab = tabPage as TabPage;
                foreach (Control childControl in tab.Controls)
                {
                    if (childControl is CustomControls.IStatContainerBindedControl)
                    {
                        var statContainerBindedControl = childControl as CustomControls.IStatContainerBindedControl;
                        Stat bindTarget = statContainerBindedControl.GetBindTarget();
                        IStatContainer foundStat = RequestStat(bindTarget);
                        statContainerBindedControl.Bind(foundStat);
                        statContainerBindedControl.Initalize();
                    }
                }
            }
        }

        private void HideTabPages()
        { 
            tabControl1.Enabled = false; // would have preferred to have kept the tabs enabled but the child controls disabled but without having to manually cycle through ever control. might try again later.
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
                statDictionary = workingFile.GetStatDictionary();
                InitalizeTabChildControls();
                ShowTabPages();
            }

        }

        /// <summary>
        /// Iterate through the StatContainers in the StatContainerDictionary and ask them to update their contents
        /// </summary>
        private void StatContainerListUpdate()
        {
            var dictEnumerator = statDictionary.GetEnumerator();

            while (dictEnumerator.MoveNext() != false)
            {
                var currentStatContainer = dictEnumerator.Current.Value;
                currentStatContainer.PushChanges();
            }

        }

        private bool Save(string fileName)
        {
            StatContainerListUpdate();

            if (workingFile.SaveFile(workingFileName))
            {
                MessageBox.Show("File saved!");
                return true;
            }
            else
            {
                MessageBox.Show("There are was an error when attempting to save the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(workingFileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.FileName = "";

            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName == "")
            {
                return;
            }

            if (Save(saveFileDialog1.FileName))
            {
                workingFileName = saveFileDialog1.FileName; // only update working filename if it is confirmed it is a valid file
            }      


        }

        /// <summary>
        /// Create an unsaved changes form and have it check if there are unsaved changes. User will be asked how to proceed if there are.
        /// </summary>
        /// <returns>true if no unsaved changes, true if unsaved changes but user wants to discard them, false if unsaved changes and user wants to cancel operation</returns>
        private bool CheckUnsavedChanges()
        {
            var unsavedChangesForm = new UnsavedChangesForm();
            bool safeToProceed = false;

            var returnCode = unsavedChangesForm.CheckUnsavedChanges(statDictionary);

            switch (returnCode)
            {
                case UnsavedChangesForm.SavedChangesReturnCodes.SaveAndExit:
                    if (Save(workingFileName))
                    {
                        safeToProceed = true;
                    }
                    else
                    {
                        safeToProceed = false;
                    }
                    break;
                case UnsavedChangesForm.SavedChangesReturnCodes.ExitWithoutSaving:
                    safeToProceed = true;
                    break;
                case UnsavedChangesForm.SavedChangesReturnCodes.CancelExit:
                    safeToProceed = false;
                    break;
            }

            return safeToProceed;
        }

        /// <summary>
        /// Check whether it is safe to exit the application
        /// </summary>
        /// <returns></returns>
        private bool SafeToExit()
        {
            if (fileHasBeenOpened)
            {
                if (!CheckUnsavedChanges())
                {
                    return false;
                }
            }
            return true;
        }

        private void FormCloseCatcher(object sender, CancelEventArgs e)
        {
            if (!SafeToExit())
            {
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SafeToExit())
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

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
