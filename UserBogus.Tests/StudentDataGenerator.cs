using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBogus.Tests
{
    public static class StudentDataGenerator
    {
        private static readonly string[] Faculties = { "Computer Science", "Economics", "Cybersecurity", "Mathematics" };

        public static Faker<Student> GetStudentFaker()
        {
            return new Faker<Student>()
                .RuleFor(s => s.FullName, f => f.Name.FullName())
                .RuleFor(s => s.Email, f => f.Internet.Email())
                .RuleFor(s => s.Grade, f => f.Random.Int(0, 100))
                .RuleFor(s => s.BirthDate, f => f.Date.Past(30, DateTime.Today.AddYears(-18)))
                .RuleFor(s => s.Faculty, f => f.PickRandom(Faculties))
                .RuleFor(s => s.Degree, f => f.PickRandom<DegreeLevel>())
                .RuleFor(s => s.Course, (f, s) =>
                    s.Degree == DegreeLevel.Bachelor
                        ? f.Random.Int(1, 4)
                        : f.Random.Int(1, 2));
        }
    }

}
