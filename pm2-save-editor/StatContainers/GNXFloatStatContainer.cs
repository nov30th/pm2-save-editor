using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{
    
    /// <summary>
    /// A GNX float is an integer in memory which is translated into a float using its string representation, that is, 156.05 in game is represented as 15605 in memory
    /// </summary>
    class GNXFloatStatContainer : IntStatContainer
    {

        public GNXFloatStatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer) : base(defaultValues, workingFileBuffer)
        {
            byte[] intAsBytes = workingFileBuffer.ReadAtOffset(offset, 2);
            currentValue = BitConverter.ToInt16(intAsBytes, 0);
            originalContents = currentValue;
            this.statType = StatTypes.GNXFloat;
        }

        public override void CommitContents()
        {
            short contents = Convert.ToInt16(currentValue);
            byte[] intAsBytes = BitConverter.GetBytes(contents);
            attachedBuffer.WriteAtOffset(offset, sizeof(short), intAsBytes);
            originalContents = currentValue;
        }

        public StatContainerReturnCodes SetValueFromFloat(double newValue) // overloading SetValue with long and double parameters results in an infinite loop
        {
            try
            {
                double newValueAdjusted = newValue * 100; // 123.45 to 12345.00
                long newValueInteger = Convert.ToInt32(newValueAdjusted); // 12345.00 to 12345
                return SetValue(newValueInteger);
            }
            catch (OverflowException e)
            {
                return StatContainerReturnCodes.IntegerOverflow; 
            }
        }

        public override string GetContents()
        {
            double value = (double)currentValue;
            value /= 100;
            return value.ToString();
        }

        public override StatContainerReturnCodes SetContents(string newContents)
        {
            double newValue;
            bool success;

            success = double.TryParse(newContents, out newValue);

            if (!success)
            {
                return StatContainerReturnCodes.InvalidType;
            }

            return SetValueFromFloat(newValue);
        }

        public override void PushChanges()
        {
            this.CommitContents();
        }

    }
}
