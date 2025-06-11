// https://www.mockaroo.com/  можна генерувти дані для .csv файлу
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Data.SqlClient;
using Z.Dapper.Plus;
using Dapper_Plus__import_csv_Products_.Models;
using Dapper;

Console.WriteLine("Dapper Plus: import data from CSV");
string csvPath = @"../../../products.csv";
string connectionString = @"Server=(localdb)\mssqllocaldb;Database=DapperPlus_Products;Trusted_Connection=True;";

List<Product> products; // сюди (products)  будемо імпортувати список продуктів з CSV
var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.ToLower() // коли будуть аналізуватися заголовки CSV, вони будуть приведені до нижнього регістру і властивості моделі Product також 
};

using (var reader = new StreamReader(csvPath)) // відкриваємо текстовий потік для читання CSV файлу
using (var csv = new CsvReader(reader, config)) // створюємо CsvReader для читання CSV файлу
{
    products = csv.GetRecords<Product>().ToList(); // зчитуємо всі записи з CSV і перетворюємо їх у список Product
}

DapperPlusManager.Entity<Product>().Table("Products"); // вказуємо Dapper Plus, що наш C# клас Product відповідає таблиці Products у базі даних

using var connection = new SqlConnection(connectionString);
connection.Open();

// вставка продуктів у таблицю Products за допомогою BulkInsert
connection.BulkInsert(products);
Console.WriteLine("Products inserted successfully.");

// Отримання всіх продуктів з таблиці
PrintAllProducts(connection);

void PrintAllProducts(SqlConnection connection)
{
    var allProducts = connection.Query<Product>("SELECT * FROM Products").ToList();
    Console.WriteLine("All products:");
    foreach (var product in allProducts)
    {
        Console.WriteLine($"{product.Id,3}: {product.Name,-20} {product.Price,-10}  ({product.Quantity})");
    }
}


