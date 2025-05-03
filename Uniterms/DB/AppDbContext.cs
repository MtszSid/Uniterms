using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniterms.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<DbContext> options) : base(options) { }

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Singleton> Singleton { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Singleton>().HasKey(s => s.Key);
        }
    }
}
