using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{
    /// <summary>
    /// A set of return codes used the StatContainers to indicate whether or not an attempt at updating their contents was successful
    /// </summary>
    public enum StatContainerReturnCodes { OK = 0, UnderMinimumSize = -1, OverMaximumSize = -2, AccessingUnitalizedContainer = -3, InvalidType = -4, IntegerOverflow = -5 }

    /// <summary>
    /// A public interface for StatContainers using strings as input and output - primarily for usage by TextBoxes
    /// </summary>
    public interface StatContainer
    {
        StatTypes GetStatType();
        string GetContents();
        StatContainerReturnCodes SetContents(string newContents);
        void PushChanges();
        string GetStatName();
        int GetChecksum(); 
    }
    // It occurs to me that IntStatContainer and StringStatContainer have a lot of overlapping functionality and duplicated code
    // But I am as of yet unsure if or how to cobine them into one eloquently

}
