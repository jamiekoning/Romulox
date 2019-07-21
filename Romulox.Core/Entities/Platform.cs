using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Romulox.Core.Entities
{
    public class Platform
    {
        public Platform()
        {
            Games = new List<Game>();
        }
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Path { get; set; }
        
        [Required]
        public PlatformType PlatformType { get; set; }
        
        public ICollection<Game> Games { get; set; }

        public string NoIntroDatFilePath { get; set; }
        public string Company { get; set; }
    }
}