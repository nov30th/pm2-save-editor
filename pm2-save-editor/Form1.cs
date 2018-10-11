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
        Dictionary<Stat, StatContainer> statDictionary;
        private string workingFileName = "";
        internal HashSet<Type> statBindedComboBoxList = new HashSet<Type> { typeof(CustomControls.BloodTypeComboBox), typeof(CustomControls.EducationComboBox) };  // less than ideal

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

            HideTabPages();

        }

        public StatContainer RequestStat(Stat requestedStat)
        {
            bool success;
            StatContainer foundStat;

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
                    var childControlType = childControl.GetType();
                    if (childControlType == typeof(CustomControls.StatContainerBindedTextBox))
                    {
                        var bindedTextBox = childControl as CustomControls.StatContainerBindedTextBox;
                        Stat bindTarget = bindedTextBox.bindTarget;
                        StatContainer foundStat = RequestStat(bindTarget);
                        bindedTextBox.Bind(foundStat);
                        bindedTextBox.Visible = true;
                    }
                    else if (statBindedComboBoxList.Contains(childControlType))
                    {
                        var bindedComboBox = childControl as CustomControls.StatContainerBindedComboBox;
                        Stat bindTarget = bindedComboBox.bindTarget;
                        StatContainer foundStat = RequestStat(bindTarget);
                        bindedComboBox.Bind(foundStat);
                        bindedComboBox.Visible = true;
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatContainerListUpdate();
            if (workingFile.SaveFile(workingFileName))
            {
                MessageBox.Show("File saved!");
            }
            else
            {
                MessageBox.Show("There are was an error when attempting to save the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.FileName = "";

            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName == "")
            {
                return;
            }

            StatContainerListUpdate();

            if (workingFile.SaveFile(saveFileDialog1.FileName))
            {
                MessageBox.Show("File saved!");
            }
            else
            {
                MessageBox.Show("There are was an error when attempting to save the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            workingFileName = saveFileDialog1.FileName;


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

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
