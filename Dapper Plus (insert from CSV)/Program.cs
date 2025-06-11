// https://www.mockaroo.com/ - генерував дані для .csv файлу
using System.Data.SqlClient;
using System.Globalization;
using Dapper;
using Z.Dapper.Plus;
using CsvHelper;
using CsvHelper.Configuration;


Console.WriteLine("Insert data from .csv");
string csvFilePath = "C:\\Users\\Ryzen\\source\\repos\\EF-test\\Dapper Plus (insert from CSV)\\MOCK_DATA _short.csv";// @"../../../MOCK_DATA_short.csv";
string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Dapper_FlightUsers;Trusted_Connection=True;";
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connected to database successfully!");

    // Створення таблиці Users, якщо вона не існує
    //string createTableQuery = @"
    //    CREATE TABLE IF NOT EXISTS FlightUsers (
    //        Id INT IDENTITY(1,1) PRIMARY KEY,
    //        Name NVARCHAR(100),
    //        Email NVARCHAR(100),
    //        Password NVARCHAR(100)
    //    );";
    //connection.Execute(createTableQuery);

    // Читання даних з CSV файлу
    //var users = new List<FlightUser>();
    //using (var reader = new StreamReader(csvFilePath))
    //{
    //    string line;
    //    while ((line = reader.ReadLine()) != null)
    //    {
    //        var values = line.Split(',');
    //        if (values.Length == 6)
    //        {
    //            users.Add(new FlightUser
    //            {
    //                FirstName = values[0],
    //                LastName = values[1],
    //                FlightNumber = values[2],
               
    //                ArrivalCity = values[5]
    //            });
    //        }
    //    }
    //}

    // Вставка даних у таблицю Users
    //connection.BulkInsert(users);

    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(), // приводимо заголовки до нижнього регістру
    };

    config.TypeConverterCache.AddConverter<TimeSpan>(new Dapper_Plus__insert_from_CSV_.TimeSpanConverter());

    List<FlightUser> users;
    using (var reader = new StreamReader(csvFilePath))
    using (var csv = new CsvReader(reader, config))
    {
       //csv.Context.TypeConverterCache.AddConverter<TimeOnly>(new TimeSpanConverter());
        users = csv.GetRecords<FlightUser>().ToList();
    }
    // Вставка даних у таблицю FlightUsers
    connection.BulkInsert(users);

    Console.WriteLine("Data inserted successfully from CSV file!");
    // Отримання всіх користувачів з таблиці Users
    var allUsers = connection.Query<FlightUser>("SELECT * FROM FlightUsers").ToList();
    Console.WriteLine("All users in the database:");
    Console.WriteLine(
        "Id\tFirstName\tLastName\tEmail\tFlightNumber\tDepartureCity\tDepartureTime\tArrivalCity");
    foreach (var user in allUsers)
    {
        Console.WriteLine($"{user.Id}\t{user.FirstName}\t{user.LastName}\t{user.FlightNumber}\t{user.DepartureCity}\t{user.DepartureTime}\t{user.ArrivalCity}");
    }
    Console.WriteLine(
        "Data retrieval completed successfully!");
    connection.Close();
    Console.WriteLine(
        "Connection closed.");

