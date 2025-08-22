using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Dapper;
using Z.Dapper.Plus;
using Microsoft.Data.SqlClient;

public enum Genre
{
    Fiction, NonFiction, Science, History, Fantasy, Mystery, Romance, Thriller, Dystopia
}

public class Book
{
    public int Id { get; set; }  // auto-increment
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public Genre? Genre { get; set; }
}

class Program
{
    const string connStr = "Server=(localdb)\\mssqllocaldb;Database=DapperPlusCSV;Trusted_Connection=True;TrustServerCertificate=True;";

//    static void Main()
//    {
//        DapperPlusManager.Entity<Book>().Table("Books");

//        var books = ImportCsv("../../../books.csv");

//        using var connection = new SqlConnection(connStr);

//        connection.BulkInsert(books);
//        Console.WriteLine("Inserted:");
//        PrintAll(connection);

//        foreach (var b in books)
//        {
//            b.Price += 1;
//            b.PublishedYear += 1;
//        }
//        connection.BulkUpdate(books);
//        Console.WriteLine("Updated:");
//        PrintAll(connection);

//        var toDelete = books.Where(b => b.PublishedYear < 2000).ToList();
//        connection.BulkDelete(toDelete);
//        Console.WriteLine("Deleted < 2000:");
//        PrintAll(connection);

//        var mergeList = books.Where(b => b.Genre == Genre.Fantasy).Select(b =>
//        {
//            b.Price *= 0.8m;
//            return b;
//        }).ToList();

//        mergeList.AddRange(new[]
//        {
//            new Book { Title = "New A", Author = "New Author A", Genre = Genre.Thriller, Price = 15, PublishedYear = 2023 },
//            new Book { Title = "New B", Author = "New Author B", Genre = Genre.Mystery, Price = 17, PublishedYear = 2024 },
//        });

//        connection.BulkMerge(mergeList);
//        Console.WriteLine("After Merge:");
//        PrintAll(connection);
//    }

//    static List<Book> ImportCsv(string path)
//    {
//        using var reader = new StreamReader(path);
//        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null };
//        using var csv = new CsvReader(reader, config);
//        csv.Context.TypeConverterCache.AddConverter<Genre>(new EnumConverter(typeof(Genre)));
//        return csv.GetRecords<Book>().ToList();
//    }

//    static void PrintAll(SqlConnection conn)
//    {
//        var books = conn.Query<Book>("SELECT * FROM Books").ToList();
//        foreach (var b in books)
//            Console.WriteLine($"{b.Title} by {b.Author} - {b.Genre} - {b.Price:C} ({b.PublishedYear})");
//        Console.WriteLine();
//    }
//}



//public enum Genre
//{
//    Fiction, NonFiction, Science, History, Fantasy, Mystery, Romance, Thriller, Dystopia
//}

//public class Book
//{
//    public int Id { get; set; }
//    public string Title { get; set; } = "";
//    public string Author { get; set; } = "";
//    public decimal Price { get; set; }
//    public int PublishedYear { get; set; }
//    public Genre? Genre { get; set; }
}

class Program2
{
        const string connStr = "Server=(localdb)\\mssqllocaldb;Database=DapperPlusCSV;Trusted_Connection=True;TrustServerCertificate=True;";


        //const string connStr = "Server=localhost;Database=Library;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        using var conn = new SqlConnection(connStr);
        var genreName = "Fantasy";

        var books = conn.Query<Book>(
            "GetBooksByGenre",
            new { Genre = genreName },
            commandType: System.Data.CommandType.StoredProcedure
        ).ToList();

        Console.WriteLine($"Books in genre {genreName}:");
        foreach (var book in books)
            Console.WriteLine($"{book.Title} by {book.Author} - {book.Price:C} ({book.PublishedYear})");
    }
}