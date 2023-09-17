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
        public DbSet<Country> Countries { get; set; }
        public DbSet<StudentType> StudentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c=> c.Name).IsUnique();
            modelBuilder.Entity<StudentType>().HasIndex(s => s.Name).IsUnique();
        }
    }
}
