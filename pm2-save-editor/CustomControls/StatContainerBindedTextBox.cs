﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace pm2_save_editor.CustomControls
{

    /// <summary>
    /// A custom text box used to provide a direct interface with a StatContainer
    /// </summary>
    class StatContainerBindedTextBox : TextBox
    {

        // consider having labels autogenerated and autoplaced beside the boxes based on the stat names in the future

        private Stat _bindTarget;
        private StatContainer _boundStat;
        private bool _autoGenerateLabel = true;
        private SmartLabel generatedLabel;

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

        [Category("Binding"), Description("Sets whether or not the TextBox should programatically create its own label.")]
        public bool autoGenerateLabel
        {
            get
            {
                return _autoGenerateLabel;
            }
            set
            {
                _autoGenerateLabel = value;
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
            this.Visible = false;
        }

        public void Bind(StatContainer stat)
        {
            _boundStat = stat;
            Text = stat.GetContents();
            TextChanged += TextUpdated; // adding the handler here avoids having the initial setting of the text trigger a change

            if (_autoGenerateLabel)
            {
                GenerateLabel();
            }

        }

        private void GenerateLabel()
        {
            generatedLabel = new SmartLabel();
            generatedLabel.RecalcuateLabelContents(this, _boundStat.GetStatName());
            this.Parent.Controls.Add(generatedLabel);
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
