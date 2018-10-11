using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor.CustomControls
{
    class BloodTypeComboBox : StatContainerBindedComboBox
    {

        // Blood Type is represented by these numbers in file, so they map perfectly to the index of the ComboBox. This is just here for reference.
        enum BloodType { A, B, O, AB }

        public BloodTypeComboBox()
        { 
            AddItems();
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectionChangeCommitted += SelectionUpdated;
        }

        public override void AddItems()
        {
            Items.Add("A");
            Items.Add("B");
            Items.Add("O");
            Items.Add("AB");
        }

        public override void InitalizeSelectedIndex()
        {
            string currentContentsString = _boundStat.GetContents();
            int currentContentsInt;

            bool success = int.TryParse(currentContentsString, out currentContentsInt);

            if (!success)
            {
                MessageBox.Show("Could not load blood type. Corrupt or incompatible file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            SelectedIndex = currentContentsInt;

        }

        public override void SelectionUpdated(object sender, EventArgs e)
        {            
            int newValue = SelectedIndex;
            _boundStat.SetContents(newValue.ToString());
            _boundStat.PushChanges();
        }



    }
}
