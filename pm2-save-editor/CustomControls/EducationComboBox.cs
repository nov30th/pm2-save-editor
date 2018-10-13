using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor.CustomControls
{
    class EducationComboBox : StatContainerBindedComboBox
    {

        enum Level { C = 0, B = 6, A = 11 , S = 16 }

        bool ignoreOnStatChangedEvent = false;

        public override void Initalize()
        {
            
            AddItems();
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectionChangeCommitted += SelectionUpdated;

            _boundStat.SubscribeToOnStatChanged(HandleOnStatChanged);

            InitalizeSelectedIndex();

            this.Visible = true;
        }

        public void HandleOnStatChanged(object sender, EventArgs e)
        {
            if (ignoreOnStatChangedEvent)
            {
                return;
            }

            InitalizeSelectedIndex();
        }

        public override void AddItems()
        {
            Items.Add("C");
            Items.Add("B");
            Items.Add("A");
            Items.Add("S");
        }

        public override void InitalizeSelectedIndex()
        {

            string currentContentsString = _boundStat.GetContents();
            int currentContentsInt;

            bool success = int.TryParse(currentContentsString, out currentContentsInt);

            if (!success)
            {
                MessageBox.Show("Could not load education. Corrupt or incompatible file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // A bit ugly, but the only alternative I could remember off the top of my head was converting back and forth between enum multiple times
            if (currentContentsInt < (int)Level.B)
            {
                SelectedIndex = 0;
            }
            else if (currentContentsInt < (int)Level.A)
            {
                SelectedIndex = 1;
            }
            else if (currentContentsInt < (int)Level.S)
            {
                SelectedIndex = 2;
            }
            else
            {
                SelectedIndex = 3;
            }

        }

        public override void SelectionUpdated(object sender, EventArgs e)
        {
            int newValue = 0;
            ignoreOnStatChangedEvent = true;

            switch (SelectedIndex)
            {
                case 0:
                    newValue = (int)Level.C;
                    break;
                case 1:
                    newValue = (int)Level.B;
                    break;
                case 2:
                    newValue = (int)Level.A;
                    break;
                case 3:
                    newValue = (int)Level.S;
                    break;
            }

            ignoreOnStatChangedEvent = true;
            _boundStat.SetContents(newValue.ToString());
            _boundStat.PushChanges();
            ignoreOnStatChangedEvent = false;
        }

    }
}
