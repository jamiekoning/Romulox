using System.Collections.Generic;
using System.Xml.Serialization;

namespace Romulox.Core.NoIntro.Entities
{
    public class NoIntroGame
    {
        [XmlAttribute(AttributeName = "name")] 
        public string Name { get; set; }
        [XmlElement(ElementName = "description")] 
        public string Description { get; set; }
        [XmlElement(ElementName = "rom")] 
        public List<NoIntroRom> NoIntroRoms { get; set; }
    }
}