using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Romulox.Core.NoIntro.Entities
{
    public class NoIntroGame
    {
        public Guid Id { get; set; }
        public string GiantBombApiGuid { get; set; }
        
        [XmlElement(ElementName = "description")] 
        public string Description { get; set; }
        [XmlElement(ElementName = "rom")] 
        public List<NoIntroRom> NoIntroRoms { get; set; }
    }
}