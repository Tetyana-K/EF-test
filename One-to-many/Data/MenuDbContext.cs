using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_to_many.Data
{
    public class MenuDbContext : DbContext
    {
        public DbSet<Models.Menu> Menus { get; set; } = null!;
        public DbSet<Models.Dish> Dishes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQL Server LocalDB
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EF_MenuDishesDb;Trusted_Connection=True;");
                //.LogTo(Console.WriteLine, LogLevel.Information); // буде виводити  SQL-запити у консоль

        }
    }
}
