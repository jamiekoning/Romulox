using System;
using System.Xml.Serialization;

namespace Romulox.Core.NoIntro.Entities
{
    public class NoIntroHeader
    {
        public Guid Id { get; set; }
        
        [XmlElement(ElementName = "name")] 
        public string Name { get; set; }
        [XmlElement(ElementName = "description")] 
        public string Description { get; set; }
        [XmlElement(ElementName = "version")] 
        public string Version { get; set; }
        [XmlElement(ElementName = "author")] 
        public string Author { get; set; }
        [XmlElement(ElementName = "homepage")] 
        public string HomePage { get; set; }
        [XmlElement(ElementName = "url")] 
        public string Url { get; set; }
    }
}