// See https://aka.ms/new-console-template for more information
using Database_First.Models;

Console.WriteLine("__________________Database First Demo");

using var db = new PlayersContext();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

// Seed the database with initial data
db.Players.Add(new Player {Name = "Dima", Age = 12, Position= "Forward" });
db.Players.Add(new Player {Name = "Artem", Age = 12, Position= "Defender" });
db.Players.Add(new Player { Name = "Vova", Age = 12, Position = "Goalkeeper" });
db.SaveChanges();

var players = db.Players.ToList();

foreach (var player in players)
{
    Console.WriteLine($"{player.Id}: {player.Name} - {player.Position} - Age {player.Age}");
}
