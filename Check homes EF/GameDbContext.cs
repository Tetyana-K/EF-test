using Microsoft.EntityFrameworkCore;

namespace GameEFCoreApp
{
    public class GameDbContext : DbContext
    {
        public DbSet<ComputerGame> ComputerGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GameDb_M;Trusted_Connection=True;");
        }
    }
}
