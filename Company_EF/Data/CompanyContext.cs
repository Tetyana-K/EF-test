using Company_EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_EF.Data
{
    public class CompanyContext : DbContext
    {
        // Конструктор за замовчуванням
        public CompanyContext() { }

        // Конструктор для тестів
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // <-- ця перевірка!
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EF_CompanyDb;Trusted_Connection=True;");
            }

        }
    }

}
