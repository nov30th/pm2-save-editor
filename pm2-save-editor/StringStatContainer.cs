using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pm2_save_editor
{
    enum StatContainerReturnCodes { OK = 0, UnderMinimumSize = -1, OverMaximumSize = -2, AccessingUnitalizedContainer = -3 }

    /// <summary>
    /// Container for a string variable
    /// </summary>
    class StringStatContainer
    {
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
        /// Initalize the container
        /// </summary>
        /// <param name="size">Size of the string block in bytes</param>
        /// <param name="offset">Offset of the string block in the file buffer</param>
        /// <param name="br">BinaryReader that has opened the file buffer</param>
        public StringStatContainer(int size, int offset, int maxSize, int minSize, BinaryReader br)
        {
            this.sizeInMemory = size;
            this.offset = offset;
            this.maxSize = maxSize;
            this.minSize = minSize;
            br.BaseStream.Position = offset;
            stringAsBytes = br.ReadBytes(size);
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
        /// Write the container contents to a buffer representing the file
        /// </summary>
        /// <param name="bw">BinaryWriter that has opened the file buffer</param>
        public void CommitContents(BinaryWriter bw)
        {
            bw.BaseStream.Position = offset;
            bw.Write(stringAsBytes, 0, sizeInMemory);
        }

    }
}
