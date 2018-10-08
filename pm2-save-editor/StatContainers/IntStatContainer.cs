using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{
    public enum IntType { Int8, UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64 };

    abstract class IntStatContainer : StatContainer
    {
        /// <summary>
        /// Internal ID used to identify the contents of this container
        /// </summary>
        Stat statId;
        /// <summary>
        /// Indicator of the specific type of stat which the container holds
        /// </summary>
        protected StatTypes statType;
        /// <summary>
        /// An enum indicating the kind of integer which the container holds
        /// </summary>
        protected long currentValue;
        /// <summary>
        /// Maximum value of the stat
        /// </summary>        
        long Max;
        /// <summary>
        /// Minimum value of the stat
        /// </summary>
        long Min;
        /// <summary>
        /// Offset of the int in the file
        /// </summary>
        protected int offset = 0;
        /// <summary>
        /// A handle to the file buffer in which this container represents a stat
        /// </summary>
        protected PrincessMakerFileBuffer attachedBuffer;

        /// <summary>
        /// Initalize the container
        /// </summary>
        /// <param name="defaultValues">A struct containing the default values for this container</param>
        /// <param name="workingFileBuffer">A handle to the file buffer in which this container represents a stat</param>
        public IntStatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer)
        {
            attachedBuffer = workingFileBuffer;
            statId = defaultValues.statID;
            offset = defaultValues.offset;
            Max = defaultValues.Max;
            Min = defaultValues.Min;
        }

        /// <summary>
        /// Get the current value of the container
        /// </summary>
        /// <returns>IntUnion containing the current valuet</returns>
        public long GetValue()
        {
            return currentValue;
        }

        /// <summary>
        /// Update the value of the container
        /// </summary>
        /// <param name="newValue">An IntUnion containing the new value</param>
        /// <returns></returns>
        public StatContainerReturnCodes SetValue(long newValue)
        {   
                     
            if (newValue < Min)
            {
                return StatContainerReturnCodes.UnderMinimumSize;
            }

            if (newValue > Max)
            {
                return StatContainerReturnCodes.OverMaximumSize;
            }

            currentValue = newValue;
            return StatContainerReturnCodes.OK;
        }

        /// <summary>
        /// Write the container contents to its attached buffer
        /// </summary>
        public abstract void CommitContents();

        public StatTypes GetStatType()
        {
            return statType;
        }

        /// <summary>
        /// Public interface for accessing container contents
        /// </summary>
        /// <returns>Container contents as string</returns>
        public abstract string GetContents();

        /// <summary>
        /// Public interface for setting container contents
        /// </summary>
        /// <param name="newContents">New containers contents</param>
        /// <returns>Information on whether or not attempt to set contents was successful</returns>
        public abstract StatContainerReturnCodes SetContents(string newContents);

        /// <summary>
        /// Public interface for requesting the StatContainer push its current contents for the attached file buffer
        /// </summary>
        public abstract void PushChanges();
    }
}

