// dotnet add package Z.Dapper.Plus
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using Dapper;
using Dapper_intro.Models;
using Z.Dapper.Plus; // беремо модель User з Dapper_intro.Models (іншого проекту - Dapper Intro)



Console.WriteLine("____Dapper Plus____________");

string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Dapper_UsersDb;Trusted_Connection=True;";
using var connection = new SqlConnection(connectionString);
connection.Open();

// вказуємо Dapper Plus, що наш C# клас User (модель) відповідає таблиці Users.
DapperPlusManager.Entity<User>().Table("Users");

List<User> newUsers = new()
{
    new User { Name = "Olesia", Email = "olesia@example.com", Password = "o1234" },
    new User { Name = "Danylo", Email = "dan@egmail.com", Password = "dabylo-1" }
};

// використовуємо BulkInsert для вставки списку користувачів у таблицю Users
//connection.BulkInsert(newUsers);
//Console.WriteLine("New users inserted successfully!");

// Отримання всіх користувачів з таблиці Users
PrintAllUsers(connection);

// Використання BulkUpdate для оновлення всіх користувачів, зміна імені на верхній регістр
var users = connection.Query<User>("SELECT * FROM Users").ToList();
foreach (var user in users)
    user.Name = user.Name.ToUpper();
// використовуємо BulkUpdate для оновлення списку користувачів у таблиці Users
//connection.BulkUpdate(users);
Console.WriteLine("\nUsers updated successfully!");
PrintAllUsers(connection);

// Використання BulkDelete для видалення користувачів, які мають Email, що закінчується на "@example.com"
var usersToDelete = connection.Query<User>("SELECT * FROM Users WHERE Email LIKE '%@example.com'").ToList();
// використовуємо BulkDelete для видалення списку користувачів з таблиці Users
//connection.BulkDelete(usersToDelete);
Console.WriteLine("\nUsers deleted successfully!");
PrintAllUsers(connection);

// Використання BulkMerge для вставки (нових) або оновлення (вже існуючих) користувачів
users = connection.Query<User>("SELECT * FROM Users WHERE Id < 10").ToList();
users.Add(new User {Name ="Olena", Email="olena.example.com", Password= "olena_12" });
foreach (var user in users)
{
    user.Name += " (merged)"; // змінюємо ім'я, щоб показати, що це оновлення
    user.Email = user.Email.Replace("@example.com", "@newdomain.com"); // змінюємо домен email
}
// Змішуємо нових користувачів з існуючими, якщо користувач  вже існує, то він оновиться, якщо ні, то буде доданий до таблиці
connection.BulkMerge(users);
Console.WriteLine("\nUsers updated successfully!");
PrintAllUsers(connection);


void PrintAllUsers(SqlConnection connection)
{
    var users = connection.Query<User>("SELECT * FROM Users").ToList();
    Console.WriteLine("All users:");
    foreach (var user in users)
    {
        Console.WriteLine($"{user.Id}: {user.Name} - {user.Email}");
    }
}


