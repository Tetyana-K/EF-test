using EF_connstring_in_json.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EF_connstring_in_json.Data
{
    
    internal class ProductDbContext : DbContext
    {
        private readonly String connectionString;
        public ProductDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DbSet<Product> Products { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
