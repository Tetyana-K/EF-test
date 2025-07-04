﻿using Microsoft.EntityFrameworkCore;
using Check_homes_EF.Models;

using Bogus;
using Check_homes_EF;
using Azure.Identity;

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
.RuleFor(u=>u.Gender, f => f.PickRandom<Gender>());
//.RuleFor(u=>u.Gender, f => f.Random.Enum<Gender>())

var fakerUserInstance = fakerUser.Generate();
Console.WriteLine(fakerUserInstance.Name);
Console.WriteLine(fakerUserInstance.Email);
Console.WriteLine(fakerUserInstance.Age);
Console.WriteLine(fakerUserInstance.Gender);
