using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor
{
    /// <summary>
    /// A list of stat types supported by the program
    /// </summary>
    /// <remarks>
    /// New stat types can be added by
    /// - Adding a label for the type to the StatTypes snum
    /// - Creating a StatContainer class to handle the type
    /// - Adding the type to the typeDict in StatFactory
    /// - Ensuring the InitalizationStruct has all necessary info to initalize the stat
    /// </remarks>
    public enum StatTypes { Int16, UInt16, Int32, String, GNXFloat, Sum };

    /// <summary>
    /// Struct used to hold default initalization values for stat containers. 
    /// </summary>
    public struct InitalizationStruct
    {
        public string name;
        public Stat statID;
        public StatTypes type;

        public int size;
        public int offset;

        public long Max;
        public long Min;

        public int minLength;
        public int maxLength;

        public List<Stat> statsToSum;

    }

    /// <summary>
    /// A list of stats known to the program in rough order and grouping of their order in the file
    /// </summary>
    /// <remarks>
    /// Support for new stats can be added by
    /// - Creating a lavel for the stat in the Stat enum
    /// - Adding metadata for the stat to the statInitalizationMap
    /// - Creating a UI control bound to the stat
    /// - Adding the maximum and minimum bounds to the Limits class
    /// </remarks>
    public enum Stat {
        Gold,
        DaughtersName, FathersName,
        BloodType,
        Stamina, Strength, Intelligence, Elegance, Glamour, Morality, Faith, Sin, Sensetivity, Stress,
        FightingRep, MagicRep, SocialRep, HousekeepingRep,
        CombatSkill, Attack, Defence, MagicSkill, MagicAttack, MagicDefence,
        Decorum, ArtSkill, Speech, Cooking, Cleaning, Personality,
        Height, Weight, Bust, Waist, Hips,
        TotalClasses, Science, Poetry, Theology, Strategy, Fencing, KungFu, Magic, Manners, Painting, Dance

    };

    /// <summary>
    /// A list of constants used for conveniently noting or changing the minimum and maximum values of stats
    /// </summary>
    public static class Limits
    {
        public const int RepMax = 2000;
        public const int RepMin = 0;
        public const int CombatSkillMax = 500;
        public const int CombatSkillMin = 0;
        public const int GeneralPersonalStatMax = 1500;
        public const int GeneralPersonalStatMin = 0;
        public const int SocialPersonalStatMax = 500;
        public const int SocialPersonalStatMin = 0;
        public const int NameMax = 8;
        public const int NameMin = 1;
        public const int GoldMax = 2147483647;
        public const int GoldMin = -2147483647;
        public const int BodyProportionMax = 50000;
        public const int BodyProportionMin = -50000;
        public const int ClassMax = 300;
        public const int ClassMin = 0;
        public const int BloodTypeMax = 4;
        public const int BloodTypeMin = 0;
    }
    
}
