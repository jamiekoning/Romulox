using System;

namespace Romulox.Core.Models
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid PlatformId { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Developers { get; set; }
        public string Publishers { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}

