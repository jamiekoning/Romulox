using System;
using System.IO;
using NUnit.Framework;
using Romulox.Core.NoIntro.Entities;
using Romulox.Core.NoIntro.Helpers;
using Romulox.Core.NoIntro.Transformers;

namespace Romulox.Tests.Romulox.Core.NoIntro.Transformers
{
    public class NoIntroHashTransformerTests
    {
        private NoIntroHashTransformer noIntroHashTransformer;
        private string datFilesDirectory;

        private static object[] hashNameCases =
        {
            new object[]
            {
                "Sega - Game Gear (20190419-061443).dat",
                "C50BC608D2F6708E255E98C324E93A13",
                "Galaga '91 (Japan)"
            },
            new object[]
            {
                "Sega - Game Gear (20190419-061443).dat",
                "7B089A66ACC807A71ADC58C9BD679ECC",
                "Junction (USA)"
            },
            new object[]
            {
                "Sega - Game Gear (20190419-061443).dat",
                "AA768D95B27CCB2A796379F7F01F1CE7",
                "Monster Truck Wars (USA, Europe)"
            },
            new object[]
            {
                "Sega - Game Gear (20190419-061443).dat",
                "8FAD420B7DC93CD7ED10BFC54EC04F07",
                "Phantasy Star Adventure (Japan)"
            },
            new object[]
            {
                "Sega - Game Gear (20190419-061443).dat",
                "E6BC90F958617E9BDE26367B5348518C",
                "Urban Strike (USA)"
            },
        };
        
        [OneTimeSetUp]
        public void NoIntroHashTransformerSetUp()
        {
            datFilesDirectory = Directory.GetCurrentDirectory() + "/Romulox.Core/NoIntro/DatFiles/";
        }
        
        [TestCaseSource(nameof(hashNameCases))]
        public void FindGameNameByMd5Hash_ExpectedName(string datFile, string md5Hash, string name)
        {
            var path = datFilesDirectory + datFile;
            var noIntroDatFile = NoIntroDatFile.CreateFromFile(path);
            
            Assert.That(noIntroDatFile.FindGameNameByMd5Hash(md5Hash), Is.EqualTo(name));
        }
    }
}