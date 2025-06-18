namespace UserValidator.Tests
{
    // ��� ���� ��� ���� ���, ��� �������� ���� ����������� � ������ ������
    public class UserValidator
    {
        public bool IsAdult(int age)
        {
            return age >= 18;
        }
        public bool IsEmail(string email)
        {
            return email.Contains("@") && email.Contains(".") && email.IndexOf("@") < email.LastIndexOf(".");
        }
    }
    // ���� ��� ���������� UserValidator
    [TestFixture]
    public class TestUserValidator
    {
        UserValidator userValidator;

        [SetUp] // �����, ���� ���������� ����� ������ ������
        public void SetUp()
        {
            userValidator = new UserValidator();
        }

        [Test]
        public void IsAdult_ReturnsTrue_IfAgeIsEqualsOrGreater18()
        {
            // Arrange
            int age = 19;

            //Act
            bool result = userValidator.IsAdult(age);
            // Assert
            Assert.That(result, Is.True, "Expected IsAdult to return true for age 19");
        }
        [TestCase(18)] // �������� �������, ���� �� 18
        [TestCase(22)]
        [TestCase(100)]
        public void IsAdult_ReturnsTrue_IfValidAge(int age) // age - ��������, ���� ���� ������������ � �������� ����� � �������� TestCase
        {
            //Act
            bool result = userValidator.IsAdult(age);
            // Assert
            Assert.That(result, Is.True, "Expected IsAdult to return false for age>= 18");
        }

        [TestCase(0)] // �������� �������, ���� �� 0
        [TestCase(17)]
        [TestCase(-1)]
        public void IsAdult_ReturnsFalse_IfInvalidAge(int age)
        {
            //Act
            bool result = userValidator.IsAdult(age);
            // Assert
            Assert.That(result, Is.False, "Expected IsAdult to return false for age < 18");
        }

        [TestCase("user@gmail.com")]
        [TestCase("user_123@ex.com")]
        public void  IsEmail_ReturnsTrue_IfValidEmail(string email)
        {
            //Act
            var result = userValidator.IsEmail(email);
            //Assert.IsTrue(result, $"Expected IsEmail to return true for valid email: {email}");
            Assert.That(result, Is.True, $"Expected IsEmail to return true for valid email: {email}");
        }
    }
}