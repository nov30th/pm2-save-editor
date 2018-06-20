using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{
    public static class StatFactory
    {
        public static T BuildStat<T>(Stat statID, PrincessMakerFileBuffer fileBuffer) where T: StatContainer
        {
            InitalizationStruct defaultValue;
            bool statFound;

            statFound = StatInitalizationValues.statInitalizationMap.TryGetValue(statID, out defaultValue);

            if (!statFound)
            {
                throw new Exception(String.Format("No config exists for stat id {0}.", statID));
            }

            var newContainer = (T)Activator.CreateInstance(typeof(T), defaultValue, fileBuffer);

            var statType = defaultValue.type;
            var containerType = newContainer.GetStatType();

            if (statType != containerType)
            {
                throw new Exception(String.Format("Incompatible types for stat {0} and container {1}", statID, containerType));
            }

            return newContainer;
        }
    }
}
