using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGameHome
{
    internal class GameDbContext : DbContext
    {
        public DbSet<ComputerGame> ComputerGames { get; set; } = null!; // повязаний із таблицею ComputerGames

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EF_Games;Trusted_Connection=True;");
        }

    }
    
}
