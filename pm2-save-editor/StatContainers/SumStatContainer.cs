using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{
    /// <summary>
    /// A sum stat is a stat whose entire purpose is to the sum of the contents of other stats. They are represented as Int16 in the file, so it inherits from Int16StatContainer to write its contents
    /// </summary>
    class SumStatContainer : Int16StatContainer
    {

        private List<Stat> statsToSum;

        public SumStatContainer(InitalizationStruct defaultValues, PrincessMakerFileBuffer workingFileBuffer) : base(defaultValues, workingFileBuffer)
        {
            this.statType = StatTypes.Sum;
            this.statsToSum = defaultValues.statsToSum;
        }

        public void SumStats(Dictionary<Stat, IStatContainer> statDictionary)
        {
            long newValue = 0;

            foreach (Stat stat in statsToSum)
            {
                IntStatContainer container = (IntStatContainer)statDictionary[stat];
                newValue += container.GetValue();
            }

            this.SetValue(newValue);
            this.PushChanges(); // Having the sum be calculated only once at the end could cause serious problems if the summed parts go over the limit - so the limit of the sum container should be decided in such a way that the combination of the summed parts should never be able to exceed the limit of the sum container.

            RaiseStatChangedEvent();

        }

    }
}
