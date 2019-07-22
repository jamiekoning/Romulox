using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Romulox.Core.Interfaces;
using Romulox.Core.NoIntro.Entities;
using Romulox.Core.NoIntro.Helpers;

namespace Romulox.Core.NoIntro.Transformers
{
    public class NoIntroHashTransformer : IGameNameTransformer
    {
        private NoIntroDatFile noIntroDatFile;

        public NoIntroHashTransformer(string noIntroDataFilePath)
        {
            noIntroDatFile = DeserializeDataFile(noIntroDataFilePath);
        }
        
        public string Transform(string filePath)
        {
            string md5Hash = ComputeMd5Hash(filePath);
            string gameName = noIntroDatFile.FindGameNameByMd5Hash(md5Hash);

            return gameName;
        }

        private string ComputeMd5Hash(string noIntroDataFilePath)
        {
            MD5 md5Provider = new MD5CryptoServiceProvider();

            byte[] fileHash;
            using (FileStream fileStream = File.OpenRead(noIntroDataFilePath))
            {
                fileHash = md5Provider.ComputeHash(fileStream);
            }
            
            return BitConverter.ToString(fileHash).Replace("-", "");
        }

        private NoIntroDatFile DeserializeDataFile(string noIntroDataFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NoIntroDatFile));
            using (FileStream noIntroDatafileStream = new FileStream(noIntroDataFilePath, FileMode.Open))
            {
                noIntroDatFile  = (NoIntroDatFile) xmlSerializer.Deserialize(noIntroDatafileStream);
            }

            return noIntroDatFile;
        }
    }
}