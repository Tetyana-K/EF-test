// See https://aka.ms/new-console-template for more information
using ComputerGameHome;

Console.WriteLine("---Games------!");

using var db = new GameDbContext();
//db.Database.EnsureDeleted();
db.Database.EnsureCreated();
// Додати нові ігри
var game1 = new ComputerGame { Name = "The Witcher 4", Genre = "RPG", ReleaseDate = new DateTime(2015, 1, 2), Price = 200 };
var game2 = new ComputerGame { Name = "Cyberpunk 2088", Genre = "RPG", ReleaseDate = new DateTime(2020, 12, 10), Price = 300 };

db.ComputerGames.Add(game1);
db.ComputerGames.Add(game2);


db.SaveChanges();

// Отримати всі ігри
var games = db.ComputerGames.ToList();
Console.WriteLine("Games in the database:");
foreach (var game in games)
{
    Console.WriteLine($"ID: {game.Id}, Name: {game.Name}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}, Price: {game.Price}");
}

