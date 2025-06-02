using Microsoft.EntityFrameworkCore;
using One_to_many_FluentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_to_many_FluentApi.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQL Server LocalDB
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EF_ShopDb;Trusted_Connection=True;");
            //.LogTo(Console.WriteLine, LogLevel.Information); // буде виводити  SQL-запити у консоль
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // конфігурація сутності Category
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories") // назва таблиці для Category
                .HasKey(c => c.Id) // первинний ключ для Category
                .HasName("PK_Category"); // ім'я первинного ключа

                e.Property(p => p.Name)
                .HasColumnName("CategoryName") // назва стовпця для Name
                .HasMaxLength(50) // максимальна довжина рядка
                .IsRequired(); // обов'язкове поле

                e.HasIndex(p => p.Name) // індекс для стовпця Name
                .IsUnique() // унікальний індекс
                .HasDatabaseName("IX_Category_Name"); // ім'я індексу
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(100) // максимальна довжина рядка для Name
                .IsRequired(); // обов'язкове поле

                e.Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // тип даних для Price
                e.HasCheckConstraint("CK_Product_Price", "[Price] >= 0"); // обмеження для Price, щоб воно було більше 0
            });

            // конфігурація зв'язків між сутностями Category та  Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // один продукт має одну категорію
                .WithMany(c => c.Products) // одна категорія має багато продуктів
                .HasForeignKey(p => p.CategoryId) // зовнішній ключ у Product
                .OnDelete(DeleteBehavior.SetNull); // видалення категорії не призведе до видалення продуктів, які до неї належать, при видаленні категорії CategoryId у продуктів стане null

            // конфігурація зв'язків між сутностями Manufacturer та  Product
            modelBuilder.Entity<Manufacturer>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Manufacturer)
                .HasForeignKey(m => m.ManufacturerId)
                //.OnDelete(DeleteBehavior.Restrict); // видалити виробника не можна, якщо є продукти, що до нього належать (буде помилка)
                .OnDelete(DeleteBehavior.Cascade); // при видаленні виробника, продукти, що до нього належать будуть видалені

            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Manufacturer) // один продукт має одного виробника
            //    .WithMany(m => m.Products) // один виробник має багато продуктів
            //    .HasForeignKey(p => p.ManufacturerId); // зовнішній ключ у Product
        }
    }
}
