using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{

    enum StatTypes { Int, String };
    enum IntType   { Int8, UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64 };

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

        // Integer Type
        IntType intType;

        //public sbyte Int8Value;
        //public byte UInt8Value;
        //public short Int16Value;
        //public ushort UInt16Value;
        //public int Int32Value;
        //public uint UInt32Value;
        //public long Int64Value;
        //public ulong UInt64Value;

        public int minValue;
        public int maxValue;

        // String Type
        //public string stringValue;

        public int minLength;
        public int maxLength;
    }

    enum Stat { DaughtersName };

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
        };

    }
}
