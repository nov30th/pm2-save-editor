using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{

    class UInt16StatContainer : IntStatContainer
    {

        public UInt16StatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer) : base(defaultValues, workingFileBuffer)
        {
            byte[] intAsBytes = workingFileBuffer.ReadAtOffset(offset, 2);
            currentValue = BitConverter.ToUInt16(intAsBytes, 0);
            this.statType = StatTypes.UInt16;
        }

        public override void CommitContents()
        {
            ushort contents = Convert.ToUInt16(currentValue);
            byte[] intAsBytes = BitConverter.GetBytes(contents);
            attachedBuffer.WriteAtOffset(offset, sizeof(ushort), intAsBytes);
        }

    }

}
