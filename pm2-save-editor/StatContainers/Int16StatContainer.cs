using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{

    class Int16StatContainer : IntStatContainer
    {

        public Int16StatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer) : base(defaultValues, workingFileBuffer)
        {
            byte[] intAsBytes = workingFileBuffer.ReadAtOffset(offset, 2);
            currentValue = BitConverter.ToInt16(intAsBytes, 0);
        }

        public override void CommitContents()
        {
            short contents = Convert.ToInt16(currentValue);
            byte[] intAsBytes = BitConverter.GetBytes(contents);
            attachedBuffer.WriteAtOffset(offset, sizeof(short), intAsBytes);
        }

    }

}
