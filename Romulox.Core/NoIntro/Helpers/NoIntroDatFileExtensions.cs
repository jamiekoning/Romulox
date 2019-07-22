using System.IO;
using System.Xml.Serialization;
using Romulox.Core.Entities;
using Romulox.Core.Exceptions;
using Romulox.Core.NoIntro.Entities;

namespace Romulox.Core.NoIntro.Helpers
{
    public static class NoIntroDatFileExtensions
    {
        public static NoIntroGame FindGameByMd5Hash(this NoIntroDatFile noIntroDatFile, string md5Hash)
        {
            foreach (var game in noIntroDatFile.NoIntroGames)
            {
                foreach (var rom in game.NoIntroRoms)
                {
                    if (rom.Md5.Equals(md5Hash))
                    {
                        return game;
                    }
                }
                
            }

            return null;
        }

        public static NoIntroRom FindRomByMd5Hash(this NoIntroDatFile noIntroDatFile, string md5Hash)
        {
            foreach (var game in noIntroDatFile.NoIntroGames)
            {
                foreach (var rom in game.NoIntroRoms)
                {
                    if (rom.Md5.Equals(md5Hash))
                    {
                        return rom;
                    }
                }
                
            }

            return null;
        }
        
        public static string FindGameNameByMd5Hash(this NoIntroDatFile noIntroDatFile, string md5Hash)
        {
            var game = noIntroDatFile.FindGameByMd5Hash(md5Hash);

            return game?.Name;
        }
        
        public static string FindRomNameByMd5Hash(this NoIntroDatFile noIntroDatFile, string md5Hash)
        {
            var rom = noIntroDatFile.FindRomByMd5Hash(md5Hash);

            return rom?.Name;
        }
    }
    
    
}