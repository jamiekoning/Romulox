using NUnit.Framework;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Tests.Romulox.Core.NoIntro.Services
{
    public class DatFileServices
    {
        [TestCase("Road Rash 3 (USA, Europe) (Alpha 5) (1994-11-19)", ExpectedResult = "Road Rash 3")]
        [TestCase("California Speed (Europe) (Proto)", ExpectedResult = "California Speed")]
        [TestCase("Castlevania (USA)", ExpectedResult = "Castlevania")]
        [TestCase("Castlevania (USA).nes", ExpectedResult = "Castlevania")]
        [TestCase("2020 Nen Super Baseball (Japan)", ExpectedResult = "2020 Nen Super Baseball")]
        [TestCase("Aaahh!!! Real Monsters (Europe)", ExpectedResult = "Aaahh!!! Real Monsters")]
        [TestCase("wwwroot/Platforms/Games/Aaahh!!! Real Monsters (Europe).zip", ExpectedResult = "Aaahh!!! Real Monsters")]
        [TestCase("wwwroot/games/Road Rash 3 (USA, Europe) (Alpha 5) (1994-11-19)", ExpectedResult = "Road Rash 3")]
        [TestCase("Aladdin II (Taiwan) (En) (Unl)", ExpectedResult = "Aladdin II")]
        [TestCase("Ogre Battle 64 - Person of Lordly Caliber (USA, Europe) (Wii Virtual Console)",
            ExpectedResult = "Ogre Battle 64 - Person of Lordly Caliber")]
        [TestCase("1080 TenEighty Snowboarding (Japan, USA) (En,Ja)",
            ExpectedResult = "1080 TenEighty Snowboarding")]
        [TestCase("/wwwroot/games/Aidyn Chronicles - The First Mage (USA) (Rev 1)",
            ExpectedResult = "Aidyn Chronicles - The First Mage")]
        [TestCase("Aidyn Chronicles - The First Mage (USA) (Rev 1)",
            ExpectedResult = "Aidyn Chronicles - The First Mage")]
        [TestCase("RoboCop versus The Terminator (USA) (Beta) (1993-04-30)", 
            ExpectedResult = "RoboCop versus The Terminator")]
        
        public string CleanName_ExpectedString(string fileName)
        {
            return DatFilesHelper.CleanName(fileName);
        }
    }
}