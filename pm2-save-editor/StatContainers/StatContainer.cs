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
    public interface IStatContainer
    {
        StatTypes GetStatType();
        string GetContents();
        StatContainerReturnCodes SetContents(string newContents);
        void PushChanges();
        string GetStatName();
        int GetChecksum();
        void SubscribeToOnStatChanged(EventHandler handler);
        bool HaveContentsChanged(); 
    }
    // It occurs to me that IntStatContainer and StringStatContainer have a lot of overlapping functionality and duplicated code
    // But I am as of yet unsure if or how to cobine them into one eloquently

    public abstract class StatContainer : IStatContainer
    {
        protected event EventHandler OnStatChanged;

        protected void RaiseStatChangedEvent()
        {
            OnStatChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SubscribeToOnStatChanged(EventHandler handler)
        {
            OnStatChanged += handler;
        }

        /// <summary>
        /// Internal ID used to identify the contents of this container
        /// </summary>
        protected Stat statId;
        /// <summary>
        /// A string representation of the stat's name used for generating labels
        /// </summary>
        protected string statName;
        /// <summary>
        /// Indicator of the specific type of stat which the container holds
        /// </summary>
        protected StatTypes statType;
        /// <summary>
        /// Offset of the int in the file
        /// </summary>
        protected int offset = 0;
        /// <summary>
        /// A handle to the file buffer in which this container represents a stat
        /// </summary>
        protected PrincessMakerFileBuffer attachedBuffer;

        public abstract StatTypes GetStatType();
        public abstract string GetContents();
        public abstract StatContainerReturnCodes SetContents(string newContents);
        public abstract void PushChanges();
        public abstract string GetStatName();
        public abstract int GetChecksum();
        public abstract bool HaveContentsChanged();

    }

}
