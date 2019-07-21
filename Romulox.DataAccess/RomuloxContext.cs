using Microsoft.EntityFrameworkCore;
using Romulox.Core.Entities;

namespace Romulox.DataAccess
{
    public class RomuloxContext : DbContext
    {
        public RomuloxContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        
    }
}