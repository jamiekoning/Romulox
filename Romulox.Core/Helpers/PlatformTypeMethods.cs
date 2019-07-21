using System;
using Romulox.Core.Entities;

namespace Romulox.Core.Helpers
{
    public static class PlatformTypeMethods
    {
        public static PlatformType StringToGiantBombPlatformAbbreviation(string enumString)
        {
            Enum.TryParse(enumString, out PlatformType platformAbbreviation);

            return platformAbbreviation;
        }
    }
}