using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{

    enum StatTypes { Int, UInt, String };

    /// <summary>
    /// Struct used to hold default initalization values for stat containers. 
    /// </summary>
    struct InitalizationStruct
    {
        public string name;
        public Stat statID;
        public StatTypes type;

        public int size;
        public int offset;

        public IntType intType;

        public long iMax;
        public long iMin;
        public ulong uMax;
        public ulong uMin;

        // String Type
        //public string stringValue;

        public int minLength;
        public int maxLength;
    }

    enum Stat { DaughtersName, FightingRep };

    static class Limits
    {
        public const int RepMax = 2000;
        public const int RepMin = 0;
    }
    

    static class StatInitalizationValues
    {

        internal static Dictionary<Stat, InitalizationStruct> statInitalizationMap = new Dictionary<Stat, InitalizationStruct>
        {
            { Stat.DaughtersName, new InitalizationStruct {
                name = "Daughter's Name",
                statID = Stat.DaughtersName,
                type = StatTypes.String,
                size = 48,
                offset = 0x74,
                minLength = 1,
                maxLength = 8 }
            },

            { Stat.FightingRep, new InitalizationStruct {
                name = "Fighting Reputation",
                statID = Stat.FightingRep,
                type = StatTypes.UInt,
                intType = IntType.UInt16,
                offset = 0x4E,
                uMax = Limits.RepMax,
                uMin = Limits.RepMin }
            },

        };

    }
}
