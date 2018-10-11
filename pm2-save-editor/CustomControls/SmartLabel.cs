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
    /// A smart label for StatContainerBindedControls that can update its contents when required
    /// </summary>
    class SmartLabel : Label
    {
        private int distanceFromParentX = 5;
        private int distanceFromParentY = 3;

        public SmartLabel()
        {
            TextAlign = ContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Update the labels contents
        /// </summary>
        /// <param name="parent">The StatContainerBindedControl to which this label is tied to</param>
        /// <param name="newContents">The new contents of the label</param>
        public void RecalcuateLabelContents(Control parent, string newContents = "Placeholder")
        {
            Text = newContents;

            Point parentLocation = parent.Location;
            Size parentSize = parent.Size;


            int labelX = parentLocation.X - Width - distanceFromParentX;

            int labelY = parentLocation.Y; // Align the top left corners of both controls
            labelY -= distanceFromParentY; // Couldn't make it work programatically so a bit of a magic number - won't scale up with changes in font size or text box size

            Location = new Point(labelX, labelY);
        }

    }
}
