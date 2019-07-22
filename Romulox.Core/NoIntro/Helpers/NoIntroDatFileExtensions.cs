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
            
            throw new GameNotFoundException("No NoIntroGame was found for the Md5: " + md5Hash);
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
            
            throw new GameNotFoundException("No NoIntroGame was found for the Md5: " + md5Hash); 
        }
        
        public static string FindGameNameByMd5Hash(this NoIntroDatFile noIntroDatFile, string md5Hash)
        {
            return noIntroDatFile.FindGameByMd5Hash(md5Hash).Name;
        }
        
        public static string FindRomNameByMd5Hash(this NoIntroDatFile noIntroDatFile, string md5Hash)
        {
            return noIntroDatFile.FindRomByMd5Hash(md5Hash).Name;
        }
    }
    
    
}