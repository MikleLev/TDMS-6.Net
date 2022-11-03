using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMS
{
    class LetterContext : DbContext
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=T;Trusted_Connection=True;";

        public DbSet<ParentLetter> Letters { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
}
