using Microsoft.EntityFrameworkCore;
using OralData.Shared.Entities;
using System.Security.Principal;

namespace OralData.Backend.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {  
        }

        public DbSet<Specialtie> Specialties { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c=> c.Name).IsUnique();
            modelBuilder.Entity<Specialtie>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
