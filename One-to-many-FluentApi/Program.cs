using Microsoft.EntityFrameworkCore;
using One_to_many_FluentApi.Data;
using One_to_many_FluentApi.Models;
Console.WriteLine("One-to-Many and FluentApi");

Console.OutputEncoding = System.Text.Encoding.UTF8; // Для коректного відображення українських символів

using var db = new ShopDbContext();

db.Database.EnsureDeleted(); 
db.Database.EnsureCreated(); 

// Додаємо категорію і виробника
var category = new Category { Name = "Електроніка" };
var manufacturer = new Manufacturer { Name = "Samsung" };

db.Categories.Add(category);
db.Manufacturers.Add(manufacturer);
db.SaveChanges();

// Додаємо продукт
var product = new Product
{
    Name = "Смартфон",
    Price = 12000m,
    CategoryId = category.Id,
    ManufacturerId = manufacturer.Id
};

db.Products.Add(product);
db.SaveChanges();

Console.WriteLine("Продукт додано.");
ShowProducts(db);

// Тестуємо видалення категорії (має встановити CategoryId = null)
db.Categories.Remove(category);
db.SaveChanges();
Console.WriteLine("Категорію видалено (SetNull).");
ShowProducts(db);

// Тестуємо видалення виробника (має видалити продукт)
db.Manufacturers.Remove(manufacturer);
db.SaveChanges();
Console.WriteLine("Виробника видалено (Cascade).");
ShowProducts(db);
    

static void ShowProducts(ShopDbContext db)
{
    var products = db.Products.ToList();
    Console.WriteLine("Продукти у базі:");
    foreach (var p in products)
    {
        Console.WriteLine($" - {p.Name}, Ціна: {p.Price}, Category: {p.Category?.Name ?? "Unknown"}, Manufacturer: {p.Manufacturer?.Name}");
    }
    Console.WriteLine();
}
