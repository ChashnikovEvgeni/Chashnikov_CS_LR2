using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chashnikov_LR2_CS.Models
{
    public class DevelopersContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }

        public DbSet<Application> Applications { get; set; }
        public DevelopersContext(DbContextOptions<DevelopersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
