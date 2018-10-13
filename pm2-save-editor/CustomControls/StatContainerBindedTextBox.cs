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
    class StatContainerBindedTextBox : TextBox, IStatContainerBindedControl
    {

        private Stat _bindTarget;
        private IStatContainer _boundStat;
        private bool _autoGenerateLabel = true;
        private SmartLabel generatedLabel;
        private bool ignoreOnStatChangedEvent = false;
        private bool ignoreTextChangedEvent = false;

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

        public void Bind(IStatContainer stat)
        {
            _boundStat = stat;
            Text = stat.GetContents();
            TextChanged += TextUpdated; // adding the handler here avoids having the initial setting of the text trigger a change

            if (_autoGenerateLabel)
            {
                GenerateLabel();
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

        public void Initalize()
        {
            var temp = _boundStat as StatContainerBase;
            temp.OnStatChanged += HandleOnStatChanged;
        }

        public void HandleOnStatChanged(object sender, EventArgs e)
        {
            if (ignoreOnStatChangedEvent)
            {
                return;
            }

            ignoreTextChangedEvent = true;
            this.Text = _boundStat.GetContents();
            ignoreTextChangedEvent = false;

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

        private void GenerateLabel()
        {
            generatedLabel = new SmartLabel();
            generatedLabel.RecalcuateLabelContents(this, _boundStat.GetStatName());
            this.Parent.Controls.Add(generatedLabel);
        }

        public void TextUpdated(object sender, EventArgs e)
        {

            if (ignoreTextChangedEvent)
            {
                return;
            }

            ignoreOnStatChangedEvent = true;

            StatContainerReturnCodes retcode = _boundStat.SetContents(Text);

            if (retcode != StatContainerReturnCodes.OK)
            {
                BackColor = System.Drawing.Color.Red; // ideally could be expanded to tell the user exactly why their commit is being rejected (too big, too small, wrong type, etc)
            }
            else
            {
                BackColor = System.Drawing.Color.White;
            }

            ignoreOnStatChangedEvent = false;

        }
    }
}
