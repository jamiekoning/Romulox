using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Romulox.Core.NoIntro.Entities;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Tests.Romulox.Core.NoIntro.Helpers
{
    public class NoIntroDatFileExtensionsTests
    {
        private string datFilesDirectory;
        
        static object[] DatFileCases =
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

        [OneTimeSetUp]
        public void NoIntroDatFileExtensionsSetUp()
        {
            datFilesDirectory = Directory.GetCurrentDirectory() + "/Romulox.Core/NoIntro/DatFiles/";
        }
        
        [TestCaseSource(nameof(DatFileCases))]
        public void LoadFromFile_ExpectedEntity(string datFile, NoIntroHeader header, int gameCount, int romCount)
        {
            var path = datFilesDirectory + datFile;
            var entity = NoIntroDatFile.CreateFromFile(path);
            
            Assert.That(entity.NoIntroHeader.Name, Is.EqualTo(header.Name));
            Assert.That(entity.NoIntroHeader.Description, Is.EqualTo(header.Description));
            Assert.That(entity.NoIntroHeader.Version, Is.EqualTo(header.Version));
            Assert.That(entity.NoIntroHeader.Author, Is.EqualTo(header.Author));
            Assert.That(entity.NoIntroHeader.HomePage, Is.EqualTo(header.HomePage));
            Assert.That(entity.NoIntroHeader.Url, Is.EqualTo(header.Url));
            
            Assert.That(entity.NoIntroGames.Count, Is.EqualTo(gameCount));

            var actualRomCount = 0;
            entity.NoIntroGames.ForEach(g => actualRomCount += g.NoIntroRoms.Count);
            
            Assert.That(actualRomCount, Is.EqualTo(romCount));
        }
    }
}