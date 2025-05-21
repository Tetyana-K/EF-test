// See https://aka.ms/new-console-template for more information
using EF_connstring_in_json.Data;
using EF_connstring_in_json.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, EF (Product)!");

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())  // папка, де запускається програма
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = builder.Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"Connection string (for check): {connectionString}");

ProductDbContext db = new ProductDbContext(connectionString);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

db.Products.Add(new Product { Name = "Product 1", Price = 10.99m, Quantity = 100, Category ="Category A" });
db.Products.Add(new Product { Name = "Product 2", Price = 2.55m, Quantity = 55, Category ="Category B" });
db.Products.Add(new Product { Name = "Product 3", Price = 4.80m, Quantity = 88, Category = "Category A" });

db.SaveChanges();

foreach (var product in db.Products)
{
    Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category: {product.Category}");
}