using Bogus;
using DemoGenerateBogus;
using System.ComponentModel.DataAnnotations;
namespace UserBogus.Tests
{
    [TestFixture]
    public class StudentValidatorTests
    {
        private StudentValidator validator;
        private Faker<Student> faker;
        [SetUp]
        public void Setup()
        {
            validator = new StudentValidator();
            faker = StudentDataGenerator.GetStudentFaker();
        }

        [Test]
        public void HasValidEmail_ReturnsFalse_WhenEmailIsInvalid()
        {
            var student = faker.Generate(); // Generate a random student using Faker
            student.Email = "invalid.email.com";

            bool isValid = validator.HasValidEmail(student.Email);

            Assert.That(isValid, Is.False);
        }
        
        [TestCase("")]
        [TestCase("    ")]
        [TestCase("plainaddress")]
        [TestCase("missingatsign.com")]
        [TestCase("missingdot@com")]
        [TestCase("wrong.order@com.")]
        public void HasValidEmail_ReturnsFalse_ForInvalidEmails(string email)
        {
            var student = faker.Generate();
            student.Email = email;

            bool isValid = validator.HasValidEmail(student.Email);

            Assert.That(isValid, Is.False);
        }
        [Test]
        public void HasValidEmail_ReturnsTrue_WhenEmailIsValid()
        {
            var student = StudentDataGenerator.GetStudentFaker().Generate();

            bool isValid = validator.HasValidEmail(student.Email);

            Assert.That(isValid, Is.True);
        }

    } 
}