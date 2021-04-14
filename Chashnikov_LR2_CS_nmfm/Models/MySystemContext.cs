using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chashnikov_LR2_CS.Models
{
    public class MySystemContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersApps> UsersApps { get; set; }
      



        public MySystemContext(DbContextOptions<MySystemContext> options)
             : base(options)
        {
           //Database.EnsureDeleted();
            Database.EnsureCreated();
        }



    }
}
