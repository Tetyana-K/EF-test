using EF_test.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_test.Data
{
    public class AppDbContext : DbContext // наш клас контексту бази даних успадковується від DbContext
    {
        public AppDbContext()
        {
            Database.EnsureDeleted(); // видалити базу даних, якщо вона існує
            Database.EnsureCreated(); // створити базу даних, якщо вона не існує
        }
        public DbSet<User> Users { get; set; } = null!; // колекція, яка буде повязана з таблицею  Users в базі даних
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ef_Users;Trusted_Connection=True;"); // connection string = рядок підключення до бази даних

        }

    }
}
