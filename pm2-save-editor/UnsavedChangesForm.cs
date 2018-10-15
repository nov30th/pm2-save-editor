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
    public partial class UnsavedChangesForm : Form
    {

        public enum SavedChangesReturnCodes { SaveAndExit, ExitWithoutSaving, CancelExit }

        string unsavedChangesPrefix = "";
        SavedChangesReturnCodes returnCode = SavedChangesReturnCodes.ExitWithoutSaving; // assume that if the user just closes the unsaved changes box they do not care about them

        public UnsavedChangesForm()
        {
            InitializeComponent();
            this.Text = "Unsaved Changes";
        }

        private string BuildTextBoxEntry(string baseString)
        {
            return unsavedChangesPrefix + baseString + "\r\n";
        }

        public SavedChangesReturnCodes CheckUnsavedChanges(Dictionary<Stat, IStatContainer> statDictionary)
        {
            foreach (var statContainer in statDictionary.Values)
            {
                if (statContainer.QueryContentsHaveChanged())
                {
                    textBox1.Text += BuildTextBoxEntry(statContainer.GetStatName());
                }
            }

            if (textBox1.Text != "")
            {
                this.ShowDialog();
            }

            return returnCode;
        }

        // Save and Exit
        private void button1_Click(object sender, EventArgs e)
        {
            returnCode = SavedChangesReturnCodes.SaveAndExit;
            this.Close();
        }

        // Exit without Saving
        private void button2_Click(object sender, EventArgs e)
        {
            returnCode = SavedChangesReturnCodes.ExitWithoutSaving;
            this.Close();
        }

        // Cancel
        private void button3_Click(object sender, EventArgs e)
        {
            returnCode = SavedChangesReturnCodes.CancelExit;
            this.Close();
        }
    }
}
