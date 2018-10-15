using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{

    /// <summary>
    /// Container for an integer variable
    /// </summary>
    abstract class IntStatContainer : StatContainer
    {
        /// <summary>
        /// A long containing the current value ofthe container
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
        /// The contents of the container when it was first initalized or last saved
        /// </summary>
        protected long originalContents;

        /// <summary>
        /// Initalize the container
        /// </summary>
        /// <param name="defaultValues">A struct containing the default values for this container</param>
        /// <param name="workingFileBuffer">A handle to the file buffer in which this container represents a stat</param>
        public IntStatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer)
        {
            attachedBuffer = workingFileBuffer;
            statId = defaultValues.statID;
            statName = defaultValues.name;
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
            RaiseStatChangedEvent();
            return StatContainerReturnCodes.OK;
        }

        /// <summary>
        /// Write the container contents to its attached buffer
        /// </summary>
        public abstract void CommitContents();

        public override StatTypes GetStatType()
        {
            return statType;
        }

        /// <summary>
        /// Public interface for accessing container contents
        /// </summary>
        /// <returns>Container contents as string</returns>
        public abstract override string GetContents();

        /// <summary>
        /// Public interface for setting container contents
        /// </summary>
        /// <param name="newContents">New containers contents</param>
        /// <returns>Information on whether or not attempt to set contents was successful</returns>
        public abstract override StatContainerReturnCodes SetContents(string newContents);

        /// <summary>
        /// Public interface for requesting the StatContainer push its current contents for the attached file buffer
        /// </summary>
        public abstract override void PushChanges();

        /// <summary>
        /// Public interface for getting the name of the attached stat
        /// </summary>
        /// <returns>Name of stat which this container represents</returns>
        public override string GetStatName()
        {
            return statName;
        }

        /// <summary>
        /// Public interface for calculating PM2 checksum of the container
        /// </summary>
        /// <returns>Calculated checksum</returns>
        public override int GetChecksum()
        {
            return (int)currentValue;
        }

        public override bool QueryContentsHaveChanged()
        {
            return !(originalContents == currentValue);
        }

    }
}

