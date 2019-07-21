using System;
using Romulox.Core.GiantBomb.Entities;

namespace Romulox.Core.GiantBomb.Extensions
{
    public static class GiantBombPlatformAbbreviationMethods
    {
        // Extension method to handle the abbreviation names that are not valid enums
        // for instance 3DO is an invalid enum name because it begins with a number
        // so the enum is instead listed as ThreeDO
        // this method will transform ThreeDO into "3DO"
        public static string GiantBombPlatformAbbreviationToString(this GiantBombPlatformAbbreviation giantBombPlatformAbbreviation)
        {
            switch (giantBombPlatformAbbreviation)
            {
                case GiantBombPlatformAbbreviation.THREEDO:
                    return "3DO";
                case GiantBombPlatformAbbreviation.ATARI2600:
                    return "2600";
                case GiantBombPlatformAbbreviation.ATARI5200:
                    return "5200";
                case GiantBombPlatformAbbreviation.ATARI7800:
                    return "7800";
                case GiantBombPlatformAbbreviation.BSX:
                    return "BS-X";
                case GiantBombPlatformAbbreviation.NINTENDO64DD:
                    return "64DD";
                default:
                    return Enum.GetName(typeof(GiantBombPlatformAbbreviation), giantBombPlatformAbbreviation);
                    
            }
        }
    }
}