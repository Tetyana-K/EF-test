using Microsoft.EntityFrameworkCore;
using One_to_one.Data;
using One_to_one.Models;

Console.WriteLine("One-to-one relation demo");
Console.WriteLine("_____________________________________");

using var db = new PersonDbContext();

db.Database.EnsureDeleted(); // видалити базу даних, якщо вона існує
db.Database.EnsureCreated(); // створити базу даних, якщо вона не існу

var ann = new Person 
{
    FirstName = "Ann",
    LastName = "Mykytuk",
    Address = new Address
    {
        Street = "Vustavkova",
        City = "Kyiv"
    }
};

var dmytro = new Person
{
    FirstName = "Dmytro",
    LastName = "Dudarchuk",
    Address = new Address
    {
        Street = "Zelena",
        City = "Lviv"
    }
};

var ihor = new Person
{
    FirstName = "Ihor",
    LastName = "Petrenko",
    Address = new Address
    {
        Street = "Lisova",
        City = "Lutsk"
    }
};
db.Persons.Add(ann);
db.Persons.Add(dmytro);
db.Persons.Add(ihor);

db.SaveChanges();

Console.WriteLine("Persons:");

foreach(var p in db.Persons.Include(p=> p.Address)) // Include(p => p.Address) — завантажує навігаційну властивість (адресу)
{
    Console.WriteLine($"{p.Id}: {p.FirstName} {p.LastName}, {p.Address?.Street}, {p.Address?.City}");
}

Address newAddress = new Address { Street = "Korzo", City = "Uzgorod" };

int id = 2;

if (UpdatePersonAddress(id, newAddress))
{
    Console.WriteLine($"\nUpdated person with Id {id}:");
    PrintPerson(db.Persons.Include(p=> p.Address)
        .FirstOrDefault(p => p.Id == id));
    Console.WriteLine();
}

RemovePerson(3);
PrintAllPersons();
bool UpdatePersonAddress(int personId, Address newAddress)
{
    var person = db.Persons.Include(p => p.Address)
        .FirstOrDefault(p => p.Id == personId);
    if (person == null)
    {
        Console.WriteLine($"Person with Id : {personId} not found");
        return false;
    }
    person.Address = newAddress;
    db.SaveChanges();
    return true;
}

void RemovePerson(int personId)
{
    var person = db.Persons.FirstOrDefault(p => p.Id == personId);
    db.Persons.Remove(person);
    db.SaveChanges();
}

void PrintPerson(Person person)
{
    if (person == null)
    {
        Console.WriteLine("Person not found");
        return;
    }
    Console.WriteLine($"Id: {person.Id}, FirstName: {person.FirstName}, LastName: {person.LastName}");
    if(person.Address != null)
    {
        Console.WriteLine($"Address: {person.Address.Street}, {person.Address.City}");
    }
    else
    {
        Console.WriteLine("Address: null");
    }
}

void PrintAllPersons()
{
    var persons = db.Persons.Include(p => p.Address);
    foreach (var person in persons)
    {
        PrintPerson(person);
        Console.WriteLine("_____________________________________");
    }
}