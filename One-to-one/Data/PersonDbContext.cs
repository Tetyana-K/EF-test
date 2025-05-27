using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using One_to_one.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_to_one.Data;

public class PersonDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use SQL Server LocalDB
        optionsBuilder
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EF_PersonDb;Trusted_Connection=True;")
            .LogTo(Console.WriteLine, LogLevel.Information); // буде виводити  SQL-запити у консоль

        // також можна побачити sql-запити у SQL Server Management Studio
        // для цього у SSMS потрібно підключитися до бази даних LocalDB, зайти у меню Tools-Sql Server Profile, File->New Trace, Run 
        //запустити програму, і буде видно запити, які виконує наша програма

    }
}