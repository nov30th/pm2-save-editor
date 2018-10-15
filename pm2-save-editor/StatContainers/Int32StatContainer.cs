using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{

    class Int32StatContainer : IntStatContainer
    {

        public Int32StatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer) : base(defaultValues, workingFileBuffer)
        {
            byte[] intAsBytes = workingFileBuffer.ReadAtOffset(offset, 4);
            currentValue = BitConverter.ToInt32(intAsBytes, 0);
            originalContents = currentValue;
            this.statType = StatTypes.Int32;
        }

        public override void CommitContents()
        {
            int contents = Convert.ToInt32(currentValue);
            byte[] intAsBytes = BitConverter.GetBytes(contents);
            attachedBuffer.WriteAtOffset(offset, sizeof(int), intAsBytes);
        }

        public override string GetContents()
        {
            return currentValue.ToString();
        }

        public override StatContainerReturnCodes SetContents(string newContents)
        {
            int newValue;
            bool success = int.TryParse(newContents, out newValue);

            if (!success)
            {
                return StatContainerReturnCodes.InvalidType;
            }

            return SetValue((long)newValue);

        }

        public override void PushChanges()
        {
            this.CommitContents();
        }

    }

}