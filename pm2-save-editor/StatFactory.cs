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

        private static Dictionary<StatTypes, Type> typeDict = new Dictionary<StatTypes, Type>
        {
            { StatTypes.UInt16, typeof(UInt16StatContainer) },
            { StatTypes.Int16, typeof(Int16StatContainer) },
            { StatTypes.String, typeof(StringStatContainer) },
            { StatTypes.GNXFloat, typeof(GNXFloatStatContainer) },
            { StatTypes.Int32, typeof(Int32StatContainer) },
            { StatTypes.Sum, typeof(SumStatContainer) },
        };

        /// <summary>
        /// Used to created initalized StatContainers that can be used by the UI
        /// </summary>
        /// <param name="statID">The stat with the container will be associated</param>
        /// <param name="defaultValue">A struct containing the default values and metadata for the stat</param>
        /// <param name="fileBuffer">A handle to the file buffer which the container should interfact with</param>
        /// <returns>An initalized stat container</returns>
        public static T BuildStat<T>(Stat statID, InitalizationStruct defaultValue, PrincessMakerFileBuffer fileBuffer) where T: IStatContainer
        {
            var statType = defaultValue.type;

            var newContainer = (T)Activator.CreateInstance(typeDict[statType], defaultValue, fileBuffer);

            var containerType = newContainer.GetStatType();

            if (statType != containerType)
            {
                throw new Exception(String.Format("Incompatible types for stat {0} and container {1}", statID, containerType));
            }

            return newContainer;
        }
    }
}
