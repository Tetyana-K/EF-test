using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Dapper_intro.Models;
/*
Dapper - mini ORM, швидша порівняно з популярною EF Core. 
Dapper дозволяє писати SQL-запити до БД і мапити їх на C# класи, загалом дозволяє зв'язати .Net код і SQL.
З  мінусів -  даппер не автогенерує код і багато шаблонних запитів, таких як SELECT, INSERT, DELETE, їх доводиться писати вручну,
але він дозволяє писати складні запити практично не жертвуючи швидкістю роботи програми.
 
 
 https://www.learndapper.com/

Для використання Dapper необхідно встановити NuGet пакет Dapper:
dotnet add package Dapper
dotnet add package System.Data.SqlClient


Створили попередньо базу даних Dapper_UsersDb, таблицю Users, додали кілька записів у таблицю Users.
// Таблиця Users має такі колонки: Id, Name, Email, Password
CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Email]    NVARCHAR (100) NOT NULL,
    [Password] NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
// Додали кілька записів у таблицю Users:

Також створили модель User, яка відповідає таблиці Users у базі даних.
 */


//@"Server=(localdb)\mssqllocaldb;Database=EF_StudentProjectDb;Trusted_Connection=True;"
string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Dapper_UsersDb;Trusted_Connection=True;";
try
{
    // Створення підключення до бази даних LocalDB за допомогою Dapper
    using IDbConnection connection = new SqlConnection(connectionString);
    // Відкриття підключення
    connection.Open();
    Console.WriteLine("Successfully connected to LocalDB!");

    // Отримання скалярного значення; виконання запиту для обчислення кылькості записів у таблиці Users -  connection.ExecuteScalar<i>
    int count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Users;");
    Console.WriteLine($"___________Total number of users: {count}");

    // Отримання скалярного значення; виконання запиту для отримання максимального Id у таблиці Users
    int maxId = connection.ExecuteScalar<int>("SELECT MAX(Id) FROM Users;");
    Console.WriteLine($"Last Id: {maxId}");


    int id = 2; // 22
    Console.WriteLine($"\n______________Get user with Id = {id} ....");
    //var user = connection.QuerySingle<User>("SELECT * FROM Users WHERE Id = 2");
    try
    {
        // Отримання ОДНОГО РЯДКА - виконання запиту для отримання даних з таблиці Users за Id (QuerySingle)
        // @Id - це параметр запиту, який буде замінено на значення id
        var user = connection.QuerySingle<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        Console.WriteLine($"{user.Id}: {user.Name} - {user.Email}");
    }
    catch (InvalidOperationException) // якщо не знайдено жодного рядка, генерується виключення InvalidOperationException, тому обробляємо його
    {
        Console.WriteLine($"User with Id = {id} not found.");
    }

    id = 3;
    Console.WriteLine($"\n______________Get user with Id = {id} ....");
    // Отримання одного рядка - виконання запиту для отримання даних з таблиці Users за Id (QuerySingleOrDefault), повертає null, якщо не знайдено жодного рядка
    var user1 = connection.QuerySingleOrDefault<User>("SELECT * FROM Users WHERE Id = @searchId", new { searchId = id}); // id = 3, searchId = 3
    if (user1 != null)
    {
        Console.WriteLine($"{user1.Id}: {user1.Name} - {user1.Email}");
    }
    else
    {
        Console.WriteLine($"User with Id = {id} not found.");
    }


    // Отримання кількох рядків - виконання запиту для отримання даних з таблиці Users
    Console.WriteLine("\n_____________Get all users");
    string sql = "SELECT * FROM Users;";
    var users = connection.Query<User>(sql).ToList();

    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }

    // Отримання кількох рядків - виконання запиту для отримання даних з таблиці Users
    Console.WriteLine("\n_____________Get all users with name Ann");
    sql = "SELECT * FROM Users WHERE Name = @Name;";
    //sql = "SELECT * FROM Users WHERE Name = @Name AND Email = @Email AND Id >= @Id;";
    users = connection.Query<User>(sql, new { Name = "Ann" }).ToList();
    //users = connection.Query<User>(sql, new User {Name="Ann", Email="ann@gmail.com", Id = 8 }).ToList();

    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }

    // Виконання кількох запитів одночасно
    using var multi = connection.QueryMultiple("SELECT * FROM Users  WHERE email LIKE '%@gmail.com'; SELECT count(*) FROM Users WHERE email LIKE '%@gmail.com';");
    users = multi.Read<User>().ToList();
    count = multi.Read<int>().Single();
    Console.WriteLine($"\n___________ Number of users with gmail.com email: {count}");
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }

    //Виконання запитів, що оновлюють дані: вставка, поновлення, видалення
    //Вставлення даних

    Console.WriteLine("\n_____________Insert new user");
    Console.Write("Input user name : ");
    string name = Console.ReadLine() ?? "Noname";

    Console.Write("Input user password : ");
    string password = Console.ReadLine() ?? "1";

    Console.Write("Input user email : ");
    string email = Console.ReadLine() ?? "1";

    var newUser = new User() { Name = name, Email = email, Password = password };
    string insertSql = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password);";
    connection.Execute(insertSql, newUser);
    Console.WriteLine($"New user {newUser.Name} with email {newUser.Email} has been added.");

    sql = "SELECT * FROM Users;";
    users = connection.Query<User>(sql).ToList();
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }

    //Оновлення даних;
    var upateSql = "UPDATE Users SET Password = @Password WHERE Id = @Id;";
    Console.WriteLine("\n_____________Update user with Id = 3");
    count = connection.Execute(upateSql, new { Id = 3, Password = "new-password" });
    Console.WriteLine($"{count} records updated.");
    Console.WriteLine("User with Id = 2 has been updated.");
    sql = "SELECT * FROM Users;";
    users = connection.Query<User>(sql).ToList();
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}, {u.Password}");
    }
    //Видалення даних
    Console.Write("Input user Id to delete: ");
    int deleteId = int.Parse(Console.ReadLine());
    connection.Execute("DELETE FROM Users WHERE Id = @Id;", new { Id = deleteId });
    Console.WriteLine($"\n_____________Delete user with Id = {deleteId}");
    users = connection.Query<User>(sql).ToList();
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}, {u.Password}");
    }

}
catch (DbException ex)//SqlException ex)
{
    Console.WriteLine($"Connection error: {ex.Message}");
    return;
}

/*
 ORM (від англ. Object-Relational Mapping — об'єктно-реляційне відображення) — це технологія програмування, 
яка дозволяє розробникам взаємодіяти з базою даних, використовуючи об'єктно-орієнтований підхід замість написання SQL-запитів вручну.

ORM — це інструмент або бібліотека, яка:
    представляє таблиці бази даних у вигляді класів;
    рядки таблиць — як об'єкти цих класів;
    стовпці таблиць — як властивості об'єктів.
 
 */