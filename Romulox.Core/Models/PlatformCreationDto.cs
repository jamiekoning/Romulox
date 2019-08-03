using Romulox.Core.Entities;

namespace Romulox.Core.Models
{
    public class PlatformCreationDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public PlatformType PlatformType { get; set; }
    }
}