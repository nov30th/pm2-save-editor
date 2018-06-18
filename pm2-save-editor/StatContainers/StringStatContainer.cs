using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pm2_save_editor
{
    enum StatContainerReturnCodes { OK = 0, UnderMinimumSize = -1, OverMaximumSize = -2, AccessingUnitalizedContainer = -3, InvalidType = -4 }

    /// <summary>
    /// Container for a string variable
    /// </summary>
    class StringStatContainer
    {
        /// <summary>
        /// Internal ID used to identify the contents of this container
        /// </summary>
        Stat statId;
        /// <summary>
        /// Byte array representing the string block as it appears in the file
        /// </summary>
        byte[] stringAsBytes;
        /// <summary>
        /// Size of the string block in memory
        /// </summary>
        int sizeInMemory = 0;
        /// <summary>
        /// The maximum size of string
        /// </summary>
        /// <remarks>Note that the maximum size of the string must be under or equal to but is not necessarily equal to sizeInMemory</remarks>
        int maxSize = 0;
        /// <summary>
        /// The minimum size of the string
        /// </summary>
        int minSize = 0;
        /// <summary>
        /// Offset of the string buffer in the file
        /// </summary>
        int offset = 0;
        /// <summary>
        /// A handle to the file buffer in which this container represents a stat
        /// </summary>
        PrincessMakerFileBuffer attachedBuffer;

        /// <summary>
        /// Initalize the container
        /// </summary>
        /// <param name="defaultValues">A struct containing the default values for this container</param>
        /// <param name="workingFileBuffer">A handle to the file buffer in which this container represents a stat</param>
        public StringStatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer)
        {
            attachedBuffer = workingFileBuffer;
            this.statId = defaultValues.statID;
            this.sizeInMemory = defaultValues.size;
            this.offset = defaultValues.offset;
            this.maxSize = defaultValues.maxLength;
            this.minSize = defaultValues.minLength;

            stringAsBytes = attachedBuffer.ReadAtOffset(offset, sizeInMemory);
        }

        /// <summary>
        /// Return stringAsBytes as a string
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return ASCIIEncoding.ASCII.GetString(stringAsBytes);
        }

        /// <summary>
        /// Update the container contents (stringAsBytes) with a new string
        /// </summary>
        /// <param name="newString">New container contents</param>
        /// <returns>Returns true on successful set, false on failed set</returns>
        public StatContainerReturnCodes SetString(string newString)
        {

            if (sizeInMemory == 0)
            {
                return StatContainerReturnCodes.AccessingUnitalizedContainer; // trying to SetString without initalizing - this is an error and should be notified somehow. that said, the existence of the constructor makes it unlikely to crop up.
            }

            if (newString.Length > maxSize)
            {
                return StatContainerReturnCodes.OverMaximumSize; // string too big
            }

            if (newString.Length < minSize)
            {
                return StatContainerReturnCodes.UnderMinimumSize; // string too small
            }

            Array.Clear(stringAsBytes, 0, sizeInMemory);
            byte[] newStringBytes = ASCIIEncoding.ASCII.GetBytes(newString);
            Array.Copy(newStringBytes, stringAsBytes, newStringBytes.Length);

            return StatContainerReturnCodes.OK;

        }

        /// <summary>
        /// Write the container contents to its attached buffer
        /// </summary>
        public void CommitContents()
        {
            attachedBuffer.WriteAtOffset(offset, sizeInMemory, stringAsBytes);
        }

    }
}
