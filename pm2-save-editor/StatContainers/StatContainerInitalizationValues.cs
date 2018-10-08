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
    public enum StatTypes { Int16, UInt16, Int32, String, GNXFloat };

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
        Stamina, Strength, Intelligence, Elegance, Glamour, Morality, Faith, Sin, Sensetivity, Stress,
        FightingRep, MagicRep, SocialRep, HousekeepingRep,
        CombatSkill, Attack, Defence, MagicSkill, MagicAttack, MagicDefence,
        Decorum, ArtSkill, Speech, Cooking, Cleaning, Personality,
        Height, Weight, Bust, Waist, Hips
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
    }
    
    public static class StatInitalizationValues
    {

        internal static Dictionary<Stat, InitalizationStruct> statInitalizationMap = new Dictionary<Stat, InitalizationStruct>
        {
            { Stat.Gold, new InitalizationStruct {
                name = "Gold",
                statID = Stat.Gold,
                type = StatTypes.Int32,
                offset = 0x70,
                Max = Limits.GoldMax,
                Min = Limits.GoldMin, }
            },
            { Stat.DaughtersName, new InitalizationStruct {
                name = "Daughter's Name",
                statID = Stat.DaughtersName,
                type = StatTypes.String,
                size = 48,
                offset = 0x74,
                minLength = Limits.NameMin,
                maxLength = Limits.NameMax }
            },

            { Stat.FathersName, new InitalizationStruct {
                name = "Fathers's Name",
                statID = Stat.DaughtersName,
                type = StatTypes.String,
                size = 48,
                offset = 0xA4,
                minLength = Limits.NameMin,
                maxLength = Limits.NameMax }
            },

            { Stat.Stamina, new InitalizationStruct {
                name = "Stamina",
                statID = Stat.Stamina,
                type = StatTypes.UInt16,
                offset = 0x3A,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Strength, new InitalizationStruct {
                name = "Strength",
                statID = Stat.Strength,
                type = StatTypes.UInt16,
                offset = 0x3C,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Intelligence, new InitalizationStruct {
                name = "Intelligence",
                statID = Stat.Intelligence,
                type = StatTypes.UInt16,
                offset = 0x3E,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Elegance, new InitalizationStruct {
                name = "Elegance",
                statID = Stat.Elegance,
                type = StatTypes.UInt16,
                offset = 0x40,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Glamour, new InitalizationStruct {
                name = "Glamour",
                statID = Stat.Glamour,
                type = StatTypes.UInt16,
                offset = 0x42,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Morality, new InitalizationStruct {
                name = "Morality",
                statID = Stat.Morality,
                type = StatTypes.UInt16,
                offset = 0x44,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Faith, new InitalizationStruct {
                name = "Faith",
                statID = Stat.Faith,
                type = StatTypes.UInt16,
                offset = 0x46,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Sin, new InitalizationStruct {
                name = "Sin",
                statID = Stat.Sin,
                type = StatTypes.UInt16,
                offset = 0x48,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Sensetivity, new InitalizationStruct {
                name = "Sensetivity",
                statID = Stat.Sensetivity,
                type = StatTypes.UInt16,
                offset = 0x4A,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Stress, new InitalizationStruct {
                name = "Stress",
                statID = Stat.Stress,
                type = StatTypes.UInt16,
                offset = 0x4C,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.FightingRep, new InitalizationStruct {
                name = "Fighting Reputation",
                statID = Stat.FightingRep,
                type = StatTypes.UInt16,
                offset = 0x4E,
                Max = Limits.RepMax,
                Min = Limits.RepMin }
            },

            { Stat.MagicRep, new InitalizationStruct {
                name = "Magic Reputation",
                statID = Stat.MagicRep,
                type = StatTypes.UInt16,
                offset = 0x50,
                Max = Limits.RepMax,
                Min = Limits.RepMin }
            },

            { Stat.SocialRep, new InitalizationStruct {
                name = "Social Reputation",
                statID = Stat.SocialRep,
                type = StatTypes.UInt16,
                offset = 0x52,
                Max = Limits.RepMax,
                Min = Limits.RepMin }
            },

            { Stat.HousekeepingRep, new InitalizationStruct {
                name = "Housekeeping Reputation",
                statID = Stat.HousekeepingRep,
                type = StatTypes.UInt16,
                offset = 0x54,
                Max = Limits.RepMax,
                Min = Limits.RepMin }
            },

            { Stat.CombatSkill, new InitalizationStruct {
                name = "Combat Skill",
                statID = Stat.CombatSkill,
                type = StatTypes.UInt16,
                offset = 0x56,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Attack, new InitalizationStruct {
                name = "Attack",
                statID = Stat.Attack,
                type = StatTypes.UInt16,
                offset = 0x58,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.Defence, new InitalizationStruct {
                name = "Defence",
                statID = Stat.Defence,
                type = StatTypes.UInt16,
                offset = 0x5A,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.MagicSkill, new InitalizationStruct {
                name = "Magic Skill",
                statID = Stat.MagicSkill,
                type = StatTypes.UInt16,
                offset = 0x5C,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.MagicAttack, new InitalizationStruct {
                name = "Magic Attack",
                statID = Stat.MagicAttack,
                type = StatTypes.UInt16,
                offset = 0x5E,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },

            { Stat.MagicDefence, new InitalizationStruct {
                name = "Magic Defence",
                statID = Stat.MagicDefence,
                type = StatTypes.UInt16,
                offset = 0x60,
                Max = Limits.CombatSkillMax,
                Min = Limits.CombatSkillMin }
            },
            
            { Stat.Decorum, new InitalizationStruct {
                name = "Decorum",
                statID = Stat.Decorum,
                type = StatTypes.UInt16,
                offset = 0x62,
                Max = Limits.SocialPersonalStatMax,
                Min = Limits.SocialPersonalStatMin }
            },

            { Stat.ArtSkill, new InitalizationStruct {
                name = "Art Skill",
                statID = Stat.ArtSkill,
                type = StatTypes.UInt16,
                offset = 0x64,
                Max = Limits.SocialPersonalStatMax,
                Min = Limits.SocialPersonalStatMin }
            },

            { Stat.Speech, new InitalizationStruct {
                name = "Speech",
                statID = Stat.Speech,
                type = StatTypes.UInt16,
                offset = 0x66,
                Max = Limits.SocialPersonalStatMax,
                Min = Limits.SocialPersonalStatMin }
            },

            { Stat.Cooking, new InitalizationStruct {
                name = "Cooking",
                statID = Stat.Cooking,
                type = StatTypes.UInt16,
                offset = 0x68,
                Max = Limits.SocialPersonalStatMax,
                Min = Limits.SocialPersonalStatMin }
            },

            { Stat.Cleaning, new InitalizationStruct {
                name = "Cleaning",
                statID = Stat.Cleaning,
                type = StatTypes.UInt16,
                offset = 0x6A,
                Max = Limits.SocialPersonalStatMax,
                Min = Limits.SocialPersonalStatMin }
            },

            { Stat.Personality, new InitalizationStruct {
                name = "Personality",
                statID = Stat.Personality,
                type = StatTypes.UInt16,
                offset = 0x6C,
                Max = Limits.SocialPersonalStatMax,
                Min = Limits.SocialPersonalStatMin }
            },

            { Stat.Height, new InitalizationStruct {
                name = "Height",
                statID = Stat.Height,
                type = StatTypes.GNXFloat,
                offset = 0xF0,
                Max = Limits.BodyProportionMax,
                Min = Limits.BodyProportionMin }
            },
            { Stat.Weight, new InitalizationStruct {
                name = "Weight",
                statID = Stat.Weight,
                type = StatTypes.GNXFloat,
                offset = 0xF2,
                Max = Limits.BodyProportionMax,
                Min = Limits.BodyProportionMin }
            },
            { Stat.Bust, new InitalizationStruct {
                name = "Bust",
                statID = Stat.Bust,
                type = StatTypes.GNXFloat,
                offset = 0xF4,
                Max = Limits.BodyProportionMax,
                Min = Limits.BodyProportionMin }
            },
            { Stat.Waist, new InitalizationStruct {
                name = "Waist",
                statID = Stat.Waist,
                type = StatTypes.GNXFloat,
                offset = 0xF6,
                Max = Limits.BodyProportionMax,
                Min = Limits.BodyProportionMin }
            },
            { Stat.Hips, new InitalizationStruct {
                name = "Hips",
                statID = Stat.Hips,
                type = StatTypes.GNXFloat,
                offset = 0xF8,
                Max = Limits.BodyProportionMax,
                Min = Limits.BodyProportionMin }
            },

    };

    }
}
