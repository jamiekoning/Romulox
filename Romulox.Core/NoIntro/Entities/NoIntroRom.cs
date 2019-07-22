using System;
using System.Xml.Serialization;

namespace Romulox.Core.NoIntro.Entities
{
    public class NoIntroRom
    {
        [XmlAttribute(AttributeName = "name")] 
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "size")] 
        public string Size { get; set; }
        [XmlAttribute(AttributeName = "crc")] 
        public string Crc { get; set; }
        [XmlAttribute(AttributeName = "md5")] 
        public string Md5 { get; set; }
        [XmlAttribute(AttributeName = "sha1")] 
        public string Sha1 { get; set; }
        [XmlAttribute(AttributeName = "status")] 
        public string Status { get; set; }
    }
}