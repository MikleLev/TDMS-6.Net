using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
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
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=Company11;Trusted_Connection=True;";

        public DbSet<ParentLetter> Letters { get; set; } = null!;
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public LetterContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(c => c.LettersTo)
                    .WithMany(s => s.To);
            modelBuilder.Entity<User>()
                    .HasMany(c => c.LettersFrom)
                    .WithOne(s => s.From);
        }
    }
}
