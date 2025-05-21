// See https://aka.ms/new-console-template for more information
using EF_test.Data;
using EF_test.Models;
using Microsoft.EntityFrameworkCore;
Console.WriteLine("Hello, EF Core");

using var db = new AppDbContext(); // using для автоматичного закриття контексту бази даних (IDisposable) Dispose()
//db.Database.EnsureDeleted();
//db.Database.EnsureCreated();

db.Users.Add(new User { Name = "Andrii Mykytuk", Email= "andr@gmail.com" });
db.Users.Add(new User { Name = "Olena Bila", Email = "olena@gmail.com" });

db.SaveChanges(); // зберегти зміни в базі даних

var users = db.Users.ToList();
foreach (var user in users)
{
    Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email : {user.Email}");
}
PrintUsers();

Console.WriteLine();

Console.WriteLine("Enter user name to add : ");
string name = Console.ReadLine();

Console.WriteLine("Enter user email to add : ");
string email = Console.ReadLine();
AddUser(name, email);
PrintUsers();

UpdateUser(name, "Olesia");
PrintUsers();

UpdateUser(name, "Dmytro"); // not found
// Add a new user
void AddUser(string name, string email ="")
{
    var user = new User { Name = name, Email = email };
    db.Users.Add(user);
    db.SaveChanges();
}   
void PrintUsers()
{
    var users = db.Users.ToList();
    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
        return;
    }
    Console.WriteLine("\nUsers in the database:");
    foreach (var user in users)
    {
        Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}");
    }
}
void UpdateUser(string name, string newName)
{
    var user = db.Users.FirstOrDefault(u => u.Name == name);
    if (user != null)
    {
        user.Name = newName;
        db.SaveChanges();
    }
    else
    {
        Console.WriteLine($"User with name {name} not found.");
    }
}   
