using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Romulox.Core.Entities;

namespace Romulox.Core.NoIntro.Entities
{
    [Serializable, XmlRoot("datafile")]
    public class NoIntroDatFile
    {
        public static NoIntroDatFile LoadFromFile(string file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NoIntroDatFile));
            
            using (FileStream noIntroDatafileStream = new FileStream(file, FileMode.Open))
            {
                return (NoIntroDatFile) xmlSerializer.Deserialize(noIntroDatafileStream);
            }
        }

        [XmlElement(ElementName = "header")] 
        public NoIntroHeader NoIntroHeader { get; set; }
        [XmlElement(ElementName = "game")] 
        public List<NoIntroGame> NoIntroGames { get; set; }
    }
}