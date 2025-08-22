// https://www.mockaroo.com/  можна генерувти дані для .csv файлу
/*
 * CSV (Comma-Separated Values) — це текстовий формат файлів, який використовується для збереження табличних даних (як у Excel чи Google Sheets). 
 * У ньому кожен рядок — це один запис (рядок таблиці), а значення в рядку розділені комами чи іншими символами (наприклад, крапкою з комою).
 
Name,Price,Quantity
Notebook,15.5,10
Pen,1.2,100
Backpack,45.0,5
 */

/* * Для роботи з Dapper Plus та зручної обробки csv-файлів потрібно встановити наступні пакети NuGet:
 * dotnet add package Microsoft.Data.SqlClient
 * dotnet add package Dapper
 * dotnet add package Z.Dapper.Plus
 * dotnet add package CsvHelper
 */

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Data.SqlClient;
using Z.Dapper.Plus;
using Dapper_Plus__import_csv_Products_.Models;
using Dapper;
using CsvHelper.TypeConversion;

Console.WriteLine("Dapper Plus: import data from CSV");
string csvPath = @"../../../products.csv";
string connectionString = @"Server=(localdb)\mssqllocaldb;Database=DapperPlus_Products;Trusted_Connection=True;";

List<Product> products; // сюди (products)  будемо імпортувати список продуктів з CSV
// конфігурація CsvHelper для налаштування читання CSV файлу
var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.ToLower(), // коли будуть аналізуватися заголовки CSV, вони будуть приведені до нижнього регістру і властивості моделі Product також 
    MissingFieldFound = null // ігнорує зайві поля, які не знайдені у моделі Product
};

// Додати конвертер для перетворення рядків у enum Category
var categoryConverter = new EnumConverter(typeof(Category));

using (var reader = new StreamReader(csvPath)) // відкриваємо текстовий потік для читання CSV файлу
using (var csv = new CsvReader(reader, config)) // створюємо CsvReader для читання CSV файлу
{
    // Додаємо конвертер для Category, щоб CsvHelper міг правильно перетворювати рядки у enum Category
    csv.Context.TypeConverterCache.AddConverter<Category>(categoryConverter);// new EnumConverter(typeof(Category)));
    products = csv.GetRecords<Product>().ToList(); // зчитуємо всі записи з CSV і перетворюємо їх у список Product-ів (у пам'яті)
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
        Console.WriteLine($"{product.Id,3}: {product.Name,-20} {product.Price,-10}  ({product.Quantity}) {product.Category}");
    }
}


