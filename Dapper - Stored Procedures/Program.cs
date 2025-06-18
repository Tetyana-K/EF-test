// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using Dapper_intro.Models; // беремо модель User з Dapper_intro.Models (іншого проекту - Dapper Intro)
using Dapper;
using System.Linq;
using System.Collections.Generic;
/*
попередньо створили таку процедуру  у БД Dapper_UsersDb
create procedure  GetUserById
    @Id int
as
begin
    select * from Users where Id = @Id;
end;
 
 */
Console.WriteLine("Using Stored Procedures in Dapper");

string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Dapper_UsersDb;Trusted_Connection=True;";
try
{
    // Створення підключення до бази даних LocalDB за допомогою Dapper
    using IDbConnection connection = new SqlConnection(connectionString);
    // Відкриття підключення
    connection.Open();
    Console.WriteLine("Successfully connected to LocalDB!\n");

    var userId = 2; // Id користувача, якого ми хочемо отримати
    var user = connection.QuerySingleOrDefault<User>(
        "GetUserById",
        new { Id = userId },
        commandType: CommandType.StoredProcedure // вказуємо, що це збережена процедура !!!
    );
    if (user != null)
    {
        Console.WriteLine($"User found: {user.Id}: {user.Name} - {user.Email}");
    }
    else
    {
        Console.WriteLine($"User with Id = {userId} not found.");
    }

    // Виконання збереженої процедури для вставки нового користувача
    // створюємо параметри для збереженої процедури
    // параметри для збереженої процедури InsertUser, використовуємо DynamicParameters з Dapper для out - параметра
    var parameters = new DynamicParameters();
    // додаємо параметри до колекції
    parameters.Add("@Name", "New User 2");
    parameters.Add("@Email", "new_user2@exmail.com");
    parameters.Add("@Password", "New password2");
    // додаємо вихідний параметр для отримання Id нового користувача
    parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output); // @NewId - Output parameter !!!

    connection.Execute(
        "InsertUser",
        parameters,
        commandType: CommandType.StoredProcedure // вказуємо, що це збережена процедура !!!
    );
    int newUserId = parameters.Get<int>("@NewId");
    Console.WriteLine($"New user inserted with Id = {newUserId}");
    GetAllUsers(connection);

}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to database: {ex.Message}");
    return;
}

static void GetAllUsers(IDbConnection connection)
{
    // Отримання всіх користувачів
    Console.WriteLine("\n_____________Get all users");
    string sql = "SELECT * FROM Users;";
    var users = connection.Query<User>(sql).ToList();
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }
}

//create procedure InsertUser
//    @Name nvarchar(100),
//    @Email nvarchar(100),
//    @Password nvarchar(100),
//    @NewId int output
//as
//begin
//    insert into Users (Name, Email, Password)
//    values (@Name, @Email, @Password);

//set @NewId = scope_identity();
//end;
