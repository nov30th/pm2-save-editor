using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{
    enum IntType { Int8, UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64 };

    struct IntUnion
    {
        // Integer Type
        public IntType intType;

        public sbyte Int8Value;
        public byte UInt8Value;
        public short Int16Value;
        public ushort UInt16Value;
        public int Int32Value;
        public uint UInt32Value;
        public long Int64Value;
        public ulong UInt64Value;
    }

    class IntStatContainer
    {
        /// <summary>
        /// Internal ID used to identify the contents of this container
        /// </summary>
        Stat statId;
        /// <summary>
        /// An enum indicating the kind of integer which the container holds
        /// </summary>
        IntType intType;
        /// <summary>
        /// A pseudo-union containing the current value of the container
        /// </summary>
        IntUnion currentValue;
        /// <summary>
        /// Size of the int in memory
        /// </summary>
        int sizeInMemory = 0;
        /// <summary>
        /// Maximum value of the stat if the stat is signed
        /// </summary>
        long iMax;
        /// <summary>
        ///  Manimum value of the stat if the stat is signed
        /// </summary>
        long iMin;
        /// <summary>
        /// Maximum value of the stat if the stat is unsigned
        /// </summary>
        ulong uMax;
        /// <summary>
        /// Manimum value of the stat if the stat is unsigned
        /// </summary>
        ulong uMin;
        /// <summary>
        /// Offset of the int in the file
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
        public IntStatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer)
        {
            attachedBuffer = workingFileBuffer;
            statId = defaultValues.statID;
            intType = defaultValues.intType;
            offset = defaultValues.offset;

            currentValue = new IntUnion();

            byte[] intAsBytes;

            switch (intType)
            {
                case IntType.Int16:
                    currentValue.intType = intType;
                    sizeInMemory = 2;
                    intAsBytes = workingFileBuffer.ReadAtOffset(offset, sizeInMemory);
                    currentValue.Int16Value = BitConverter.ToInt16(intAsBytes, 0);
                    iMax = defaultValues.iMax;
                    iMin = defaultValues.iMin;
                    break;

                case IntType.UInt16:
                    currentValue.intType = intType;
                    sizeInMemory = 2;
                    intAsBytes = workingFileBuffer.ReadAtOffset(offset, sizeInMemory);
                    currentValue.UInt16Value = BitConverter.ToUInt16(intAsBytes, 0);
                    uMax = defaultValues.uMax;
                    uMin = defaultValues.uMin;
                    break;
            }

        }

        /// <summary>
        /// Get the current value of the container
        /// </summary>
        /// <returns>IntUnion containing the current valuet</returns>
        public IntUnion GetValue()
        {
            return currentValue;
        }

        /// <summary>
        /// Update the value of the container
        /// </summary>
        /// <param name="newValue">An IntUnion containing the new value</param>
        /// <returns></returns>
        public StatContainerReturnCodes SetValue(IntUnion newValue)
        {

            if (newValue.intType != this.intType)
            {
                return StatContainerReturnCodes.InvalidType;
            }

            switch (newValue.intType)
            {
                case IntType.Int16:
                    if (newValue.Int16Value >= iMax)
                    {
                        return StatContainerReturnCodes.OverMaximumSize;
                    }
                    if (newValue.Int16Value <= iMin)
                    {
                        return StatContainerReturnCodes.UnderMinimumSize;
                    }
                    currentValue.Int16Value = newValue.Int16Value;
                    break;
                case IntType.UInt16:
                    if (newValue.UInt16Value >= uMax)
                    {
                        return StatContainerReturnCodes.OverMaximumSize;
                    }
                    if (newValue.UInt16Value <= uMin)
                    {
                        return StatContainerReturnCodes.UnderMinimumSize;
                    }
                    currentValue.UInt16Value = newValue.UInt16Value;
                    break;
            }

            return StatContainerReturnCodes.OK;
        }

        /// <summary>
        /// Write the container contents to its attached buffer
        /// </summary>
        public void CommitContents()
        {
            byte[] intAsBytes;

            switch (currentValue.intType)
            {
                case IntType.Int16:
                    intAsBytes = BitConverter.GetBytes(currentValue.Int16Value);
                    attachedBuffer.WriteAtOffset(offset, sizeInMemory, intAsBytes);
                    break;
                case IntType.UInt16:
                    intAsBytes = BitConverter.GetBytes(currentValue.UInt16Value);
                    attachedBuffer.WriteAtOffset(offset, sizeInMemory, intAsBytes);
                    break;
            }

        }

    }
}
