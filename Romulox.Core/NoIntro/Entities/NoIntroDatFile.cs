using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Romulox.Core.NoIntro.Entities
{
    [Serializable, XmlRoot("datafile")]
    public class NoIntroDatFile
    {
        public Guid Id { get; set; }
        
        [XmlElement(ElementName = "header")] 
        public NoIntroHeader NoIntroHeader { get; set; }
        [XmlElement(ElementName = "game")] 
        public List<NoIntroGame> NoIntroGames { get; set; }
    }
}