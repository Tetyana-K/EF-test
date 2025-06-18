// See https://aka.ms/new-console-template for more information
using Bogus;
using DemoGenerateBogus;
Console.WriteLine("Demo Bogus Generaten");

Faker faker = new Faker();
var number = faker.Random.Int(1, 100);
Console.WriteLine(number);
var date = faker.Date.Past(10);
Console.WriteLine(date);

var text = faker.Lorem.Sentence();
Console.WriteLine(text);

var user = new User
{
    Name = faker.Name.FullName(),
    Email = faker.Internet.Email(),
    Age = faker.Random.Int(18, 100),
    Gender = faker.PickRandomParam(Gender.Male, Gender.Female)
};//
Console.WriteLine(user.Name);
Console.WriteLine(user.Email);
Console.WriteLine(user.Age);
Console.WriteLine(user.Gender);
Console.WriteLine();

Faker<User> fakerUser = new Faker<User>();
fakerUser.RuleFor(u => u.Name, f => f.Name.FullName())
.RuleFor(u => u.Email, f => f.Internet.Email())
.RuleFor(u => u.Age, f => f.Random.Int(18, 100))
.RuleFor(u => u.Gender, f => f.PickRandom<Gender>());
//.RuleFor(u=>u.Gender, f => f.Random.Enum<Gender>())

var fakerUserInstance = fakerUser.Generate();
Console.WriteLine(fakerUserInstance.Name);
Console.WriteLine(fakerUserInstance.Email);
Console.WriteLine(fakerUserInstance.Age);
Console.WriteLine(fakerUserInstance.Gender);

var fakerUsers = fakerUser.Generate(5);
Console.WriteLine("_______________Fake people (users)");
foreach (var u in fakerUsers)
{
    Console.WriteLine($"Name  : {u.Name}");
    Console.WriteLine($"Email : {u.Email}");
    Console.WriteLine($"Age   : {u.Age}");
    Console.WriteLine($"Gender: {u.Gender}\n");
}


