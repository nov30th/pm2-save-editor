using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace pm2_save_editor.CustomControls
{
    class StatContainerBindedTextBox : TextBox
    {

        private Stat _bindTarget;
        private StatContainer _boundStat;

        [Category("Binding"), Description("The stat to which the TextBox should be bound.")]
        public Stat bindTarget
        {
            get
            {
                return _bindTarget;
            }
            set
            {
                _bindTarget = value;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        public StatContainerBindedTextBox()
        {
            InitializeComponent();
        }

        public void Bind(StatContainer stat)
        {
            _boundStat = stat;
            Text = stat.GetContents();
            TextChanged += TextUpdated; // adding the handler here avoids having the initial setting of the text trigger a change
        }

        public void TextUpdated(object sender, EventArgs e)
        {
            StatContainerReturnCodes retcode = _boundStat.SetContents(Text);

            if (retcode != StatContainerReturnCodes.OK)
            {
                BackColor = System.Drawing.Color.Red; // ideally could be expanded to tell the user exactly why their commit is being rejected (too big, too small, wrong type, etc)
            }
            else
            {
                BackColor = System.Drawing.Color.White;
            }

        }
    }
}
