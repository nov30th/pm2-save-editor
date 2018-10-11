using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Drawing;

namespace pm2_save_editor.CustomControls
{
    /// <summary>
    /// A smart label for StatContainerBindexTextBoxes that can update its contents when required
    /// </summary>
    class SmartLabel : Label
    {
        private int distanceFromTextBoxX = 5;
        private int distanceFromTextBoxY = 3;

        public SmartLabel()
        {
            TextAlign = ContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Update the labels contents
        /// </summary>
        /// <param name="parent">The StatContainerBindedTextBox to which this label is tied to</param>
        /// <param name="newContents">The new contents of the label</param>
        public void RecalcuateLabelContents(Control parent, string newContents = "Placeholder")
        {
            Text = newContents;

            Point textBoxLocation = parent.Location;
            Size textBoxSize = parent.Size;


            int labelX = textBoxLocation.X - Width - distanceFromTextBoxX;

            int labelY = textBoxLocation.Y; // Align the top left corners of both controls
            labelY -= distanceFromTextBoxY; // Couldn't make it work programatically so a bit of a magic number - won't scale up with changes in font size or text box size

            Location = new Point(labelX, labelY);
        }

    }
}
