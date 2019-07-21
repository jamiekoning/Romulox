using System.IO;
using System.Xml.Serialization;
using Romulox.Core.Exceptions;
using Romulox.Core.NoIntro.Entities;

namespace Romulox.Core.NoIntro.Helpers
{
    public static class NoIntroDatFileExtensions
    {
        public static NoIntroDatFile LoadFromFile(this NoIntroDatFile noIntroDatFile, string noIntroDataFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NoIntroDatFile));
            using (FileStream noIntroDatafileStream = new FileStream(noIntroDataFilePath, FileMode.Open))
            {
                noIntroDatFile  = (NoIntroDatFile) xmlSerializer.Deserialize(noIntroDatafileStream);
            }

            return noIntroDatFile;
            
        }
        
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
    }
    
    
}