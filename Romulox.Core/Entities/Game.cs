using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Romulox.Core.Entities
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Path { get; set; }
        
        [ForeignKey("PlatformId")]
        public Platform Platform { get; set; }
        public Guid PlatformId { get; set; }
        
        [DefaultValue(false)]
        public bool Processed { get; set; }

        // Metadata
        public DateTime? ReleaseDate { get; set; }
        public string Developers { get; set; }
        public string Publishers { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
        
        
    }
}