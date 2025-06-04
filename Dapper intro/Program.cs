using System.Data.SqlClient;
using Dapper;
using Dapper_intro.Models;

//@"Server=(localdb)\mssqllocaldb;Database=EF_StudentProjectDb;Trusted_Connection=True;"
string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Dapper_UsersDb;Trusted_Connection=True;";
try
{
    using SqlConnection connection = new(connectionString);
    connection.Open();
    Console.WriteLine("Successfully connected to LocalDB!");

    // Отримання скалярного значення; виконання запиту для обчислення кылькості записів у таблиці Users
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
        var user = connection.QuerySingle<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });

        Console.WriteLine($"{user.Id}: {user.Name} - {user.Email}");

    }
    catch (InvalidOperationException)
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

    // Виконання кількох запитів одночасно
    using var multi = connection.QueryMultiple("SELECT * FROM Users  WHERE email LIKE '%@gmail.com'; SELECT count(*) FROM Users WHERE email LIKE '%@gmail.com';");
    users = multi.Read<User>().ToList();
    count = multi.Read<int>().Single();
    Console.WriteLine($"\n___________ Number of users with gmail email: {count}");
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }
    //Виконання запитів, що оновлюють дані: вставка, поновлення, видалення
    //Вставлення даних

    Console.WriteLine("\n_____________Insert new user");
    var newUser = new User() { Name = "Ann", Email = "ann@gmail.com", Password = "111"};
    string insertSql = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password);";
   // connection.Execute(insertSql, newUser);
    Console.WriteLine($"New user {newUser.Name} with email {newUser.Email} has been added.");
    sql = "SELECT * FROM Users;";
    users = connection.Query<User>(sql).ToList();

    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }

    
    //Оновлення даних;
    var upateSql = "UPDATE Users SET Password = @Password WHERE Id = @Id;";
    Console.WriteLine("\n_____________Update user with Id = 2");
    connection.Execute(upateSql, new User { Id =2, Password= "super-admin"});
    Console.WriteLine("User with Id = 2 has been updated.");
    sql = "SELECT * FROM Users;";
    users = connection.Query<User>(sql).ToList();
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}, {u.Password}");
    }

    //Видалення даних
    connection.Execute("DELETE FROM Users WHERE Id = @Id;", new { Id = 6 });
    Console.WriteLine("\n_____________Delete user with Id = 6");
    users = connection.Query<User>(sql).ToList();
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}, {u.Password}");
    }

}
catch (SqlException ex)
{
    Console.WriteLine($"Connection error: {ex.Message}");
    return;
}

