
using Bogus;

namespace Src
{
    public static class CarDataGenerator
    {
        public static Car Generate()
        {
            var faker = new Faker<Car>()
                .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(c => c.Model, f => f.Vehicle.Model())
                .RuleFor(c => c.Year, f => f.Date.Past(30).Year)
                .RuleFor(c => c.Mileage, f => f.Random.Int(0, 300000));

            return faker.Generate();
        }
    }
}
