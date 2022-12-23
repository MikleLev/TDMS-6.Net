using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMS
{
    class LetterContext : DbContext
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=Company9;Trusted_Connection=True;";

        public DbSet<ParentLetter> Letters { get; set; } = null!;
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ParentLetter>()
            //        .HasMany(c => c.To)
            //        .WithMany(s => s.Letters);
            //modelBuilder.Entity<User>()
            //        .HasMany(c => c.Letters)
            //        .WithMany(s => s.To);
            modelBuilder.Entity<ParentLetter>()
                    .HasMany(c => c.To)
                    .WithMany(s => s.Letters);

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Students)
            //    .WithMany(s => s.Courses)
        }
    }
}
