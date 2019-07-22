using System;
using NUnit.Framework;
using Romulox.Core.NoIntro.Transformers;

namespace Romulox.Tests.Romulox.Core.NoIntro.Transformers
{
    public class NoIntroStringTransformerTests
    {
        private NoIntroStringTransformer noIntroStringTransformer;

        [OneTimeSetUp]
        public void NoIntroStringTransformerSetUp()
        {
            noIntroStringTransformer = new NoIntroStringTransformer();
        }
        
        // Name with "Rev" tag
        [TestCase("Aidyn Chronicles - The First Mage (USA) (Rev 1)", 
            ExpectedResult = "Aidyn Chronicles - The First Mage")]
        // Name with "Alpha" and date tags
        [TestCase("Road Rash 3 (USA, Europe) (Alpha 5) (1994-11-19)", ExpectedResult = "Road Rash 3")]
        // Name with "Beta" and date tags
        [TestCase("RoboCop versus The Terminator (USA) (Beta) (1993-04-30)", ExpectedResult = "RoboCop versus The Terminator")]
        // Name with "Proto" tag
        [TestCase("California Speed (Europe) (Proto)", ExpectedResult = "California Speed")]
        // Name with "USA" region tag
        [TestCase("Castlevania (USA)", ExpectedResult = "Castlevania")]
        // Name with "Japan" region tag
        [TestCase("2020 Nen Super Baseball (Japan)", ExpectedResult = "2020 Nen Super Baseball")]
        // Name with "Europe" region tag
        [TestCase("Aaahh!!! Real Monsters (Europe)", ExpectedResult = "Aaahh!!! Real Monsters")]
        // Name with comma separated region tag
        [TestCase("Ogre Battle 64 - Person of Lordly Caliber (USA, Europe) (Wii Virtual Console)", 
            ExpectedResult = "Ogre Battle 64 - Person of Lordly Caliber")]
        // Name with comma separated region tag and comma separated language tag
        [TestCase("1080 TenEighty Snowboarding (Japan, USA) (En,Ja)", 
            ExpectedResult = "1080 TenEighty Snowboarding")]
        // Name with "Unl" tag
        [TestCase("Aladdin II (Taiwan) (En) (Unl)", ExpectedResult = "Aladdin II")]
        // Path with leading "/"
        [TestCase("/wwwroot/games/Aidyn Chronicles - The First Mage (USA) (Rev 1)", 
            ExpectedResult = "Aidyn Chronicles - The First Mage")]
        // Path without leading "/"
        [TestCase("wwwroot/games/Road Rash 3 (USA, Europe) (Alpha 5) (1994-11-19)", ExpectedResult = "Road Rash 3")]
        public string Transform_ExpectedName(string fileName)
        {
            return noIntroStringTransformer.Transform(fileName);
        }

        [TestCase("wwwroot/games/Road Rash 3")]
        [TestCase("/wwwroot/games/Road Rash 3")]
        [TestCase("Castlevania")]
        public void Transform_ArgumentExeption(string file)
        {
            Assert.Throws<ArgumentException>(() => noIntroStringTransformer.Transform(file));
        }
    }
}