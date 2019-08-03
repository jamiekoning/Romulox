using System.IO;
using NUnit.Framework;
using Romulox.Core.NoIntro.Entities;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Tests.Romulox.Core.NoIntro.Helpers
{
    public class NoIntroDatFileExtensionsTests
    {
        private string datFilesDirectory;
        
        private static object[] noIntroDatFileCases =
        {
            new object[]
            {
                "Sega - Game Gear (20190419-061443).dat", 
                new NoIntroHeader
                {
                    Name = "Sega - Game Gear",
                    Description = "Sega - Game Gear",
                    Version = "20190419-061443",
                    Author = "BigFred, BitLooter, C. V. Reynolds, fuzzball, Hiccup, jimmsu, kazumi213, omonim2007, Powerpuff, relax, Rifu, TeamEurope, xuom2",
                    HomePage = "No-Intro",
                    Url = "http://www.no-intro.org"
                },
                512, // expected game count
                512  // expected rom count
            }
            
        };
        
        private static object[] toSecDatFileCases =
        {
            new object[]
            {
                "Atari Jaguar - Games - [J64] (TOSEC-v2018-07-01_CM).dat", 
                new NoIntroHeader
                {
                    Name = "Atari Jaguar - Games - [J64]",
                    Description = "Atari Jaguar - Games - [J64] (TOSEC-v2018-07-01)",
                    Version = "2018-07-01",
                    Author = "mictlantecuhtle",
                    HomePage = "TOSEC",
                    Url = "http://www.tosecdev.org/",
                    Category = "TOSEC",
                    Email = "contact@tosecdev.org"
                },
                45, // expected game count
                45  // expected rom count
            }
            
        };
        
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
        public void NoIntroDatFileExtensionsSetUp()
        {
            datFilesDirectory = Directory.GetCurrentDirectory() + "/Romulox.Core/NoIntro/DatFiles/";
        }
        
        [TestCaseSource(nameof(noIntroDatFileCases))]
        public void LoadFromFile_NoIntroDatFile_ExpectedEntity(string datFile, NoIntroHeader header, int gameCount, int romCount)
        {
            var path = datFilesDirectory + datFile;
            var entity = NoIntroDatFile.CreateFromFile(path);
            
            Assert.That(entity.NoIntroHeader.Name, Is.EqualTo(header.Name));
            Assert.That(entity.NoIntroHeader.Description, Is.EqualTo(header.Description));
            Assert.That(entity.NoIntroHeader.Version, Is.EqualTo(header.Version));
            Assert.That(entity.NoIntroHeader.Author, Is.EqualTo(header.Author));
            Assert.That(entity.NoIntroHeader.HomePage, Is.EqualTo(header.HomePage));
            Assert.That(entity.NoIntroHeader.Url, Is.EqualTo(header.Url));
            Assert.That(entity.NoIntroHeader.Category, Is.Null);
            Assert.That(entity.NoIntroHeader.Email, Is.Null);
            
            Assert.That(entity.NoIntroGames.Count, Is.EqualTo(gameCount));

            var actualRomCount = 0;
            entity.NoIntroGames.ForEach(g => actualRomCount += g.NoIntroRoms.Count);
            
            Assert.That(actualRomCount, Is.EqualTo(romCount));
        }

        [TestCaseSource(nameof(toSecDatFileCases))]
        public void LoadFromFile_ToSecDatFile_ExpectedEntity(string datFile, NoIntroHeader header, int gameCount, int romCount)
        {
            var path = datFilesDirectory + datFile;
            var entity = NoIntroDatFile.CreateFromFile(path);
            
            Assert.That(entity.NoIntroHeader.Name, Is.EqualTo(header.Name));
            Assert.That(entity.NoIntroHeader.Description, Is.EqualTo(header.Description));
            Assert.That(entity.NoIntroHeader.Version, Is.EqualTo(header.Version));
            Assert.That(entity.NoIntroHeader.Author, Is.EqualTo(header.Author));
            Assert.That(entity.NoIntroHeader.HomePage, Is.EqualTo(header.HomePage));
            Assert.That(entity.NoIntroHeader.Url, Is.EqualTo(header.Url));
            Assert.That(entity.NoIntroHeader.Category, Is.EqualTo(header.Category));
            Assert.That(entity.NoIntroHeader.Email, Is.EqualTo(header.Email));
            
            Assert.That(entity.NoIntroGames.Count, Is.EqualTo(gameCount));

            var actualRomCount = 0;
            entity.NoIntroGames.ForEach(g => actualRomCount += g.NoIntroRoms.Count);
            
            Assert.That(actualRomCount, Is.EqualTo(romCount));
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