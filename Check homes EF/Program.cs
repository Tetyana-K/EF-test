using Dapper;
using DapperNew;
using System;
using System.Data.SqlClient;
using System.Linq;
using Z.Dapper.Plus;

using Dapper;
using Z.Dapper.Plus;
using Microsoft.Data.SqlClient;
//string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DapperPlus_Products;Trusted_Connection=True;";


public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = string.Empty;
}

class Program
{
    static void Main()
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DapperPlus_Products;Trusted_Connection=True;";
        using var connection = new System.Data.SqlClient.SqlConnection(connectionString);

        var products = new List<Product>
        {
            new Product { Name="Ручка", Price=10, Quantity=50, Category="Канцтовари" },
            new Product { Name="Олівець", Price=5, Quantity=100, Category="Канцтовари" },
            new Product { Name="Зошит", Price=25, Quantity=30, Category="Канцтовари" },
            new Product { Name="Калькулятор", Price=150, Quantity=10, Category="Електроніка" },
            new Product { Name="Чайник", Price=500, Quantity=5, Category="Побутова техніка" }
        };
        connection.BulkInsert(products);
        var allProducts = connection.Query<Product>("SELECT * FROM Products").ToList();
        Console.WriteLine("Після BulkInsert:");
        allProducts.ForEach(p => Console.WriteLine($"{p.Id} {p.Name} {p.Price} {p.Quantity} {p.Category}"));

        var updatedProducts = allProducts.Select(p =>
        {
            if (p.Name == "Ручка") p.Quantity += 20;
            if (p.Name == "Чайник") p.Price = 450;
            return p;
        }).ToList();
        connection.BulkUpdate(updatedProducts);
        var afterUpdate = connection.Query<Product>("SELECT * FROM Products").ToList();
        Console.WriteLine("Після BulkUpdate:");
        afterUpdate.ForEach(p => Console.WriteLine($"{p.Id} {p.Name} {p.Price} {p.Quantity} {p.Category}"));

        var toDelete = afterUpdate.Where(p => p.Quantity < 10).ToList();
        connection.BulkDelete(toDelete);
        var afterDelete = connection.Query<Product>("SELECT * FROM Products").ToList();
        Console.WriteLine("Після BulkDelete:");
        afterDelete.ForEach(p => Console.WriteLine($"{p.Id} {p.Name} {p.Price} {p.Quantity} {p.Category}"));

        var stationery = afterDelete.Where(p => p.Category == "Канцтовари").ToList();
        foreach (var s in stationery) s.Price *= 0.75m;
        stationery.Add(new Product { Name = "Маркер", Price = 20, Quantity = 40, Category = "Канцтовари" });
        stationery.Add(new Product { Name = "Папка", Price = 15, Quantity = 25, Category = "Канцтовари" });
        connection.BulkMerge(stationery);
        var afterMerge = connection.Query<Product>("SELECT * FROM Products").ToList();
        Console.WriteLine("Після BulkMerge:");
        afterMerge.ForEach(p => Console.WriteLine($"{p.Id} {p.Name} {p.Price} {p.Quantity} {p.Category}"));
    }
}