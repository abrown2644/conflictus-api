using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Battle> Battle { get; set; }
        public DbSet<War> War { get; set; }
        public DbSet<Location> Location { get; set; }        
        public DbSet<SideA> SideA { get; set; }
        public DbSet<SideB> SideB { get; set; }
        public DbSet<Participant> Participant { get; set; }        
        
    }
}
