using Bogus;
using DemoGenerateBogus;
namespace UserBogus.Tests
{
    public class UserBogusTests
    {
        Faker<User> userFaker;
        [SetUp]
        public void Setup()
        {
            userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexFaker + 1) // Генеруємо Id, починаючи з 1
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Age, f => f.Random.Int(18, 100))
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>));
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}