using Bogus;
// dotnet add package Bogus

namespace CalculatorNS.Tests
{
    public class CalcualtorTestsByBogus
    {
        private Calculator calculator;
        private Faker faker;

        // [SetUp] - метод, який виконується перед кожним тестом
        [SetUp]
        public void Setup()
        {
            // Ініціалізуємо екземпляр калькулятора перед кожним тестом
            calculator = new Calculator();

            // Ініціалізуємо Faker для генерації випадкових даних
            faker = new Faker(); 
        }

        [Test]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            // Arrange

            // Використовуємо Faker для генерації випадкових чисел
            double a = faker.Random.Double(-100, 100);
            double b = faker.Random.Double(-100, 100);

            // Act: виконуємо дію, яку ми тестуємо
            double result = calculator.Add(a, b);

            // Assert: перевіряємо, чи результат відповідає очікуваному значенню
           
            Assert.That(result, Is.EqualTo(a + b));
        }
        
        [Test]
        public void Calculator_Divide_ByZero_ThrowsDivideByZeroException()
        {
        
            // Arrange
            double a = faker.Random.Double(-100, 100);
            // Act & Assert
            Assert.Throws<DivideByZeroException>( // assert.Throws - перевіряє, що кидається виняток
                () => calculator.Divide(a, 0), // функція, яка спричиняє виключення
                "Expected DivideByZeroException when dividing by zero"); // повідомлення, яке буде виведено, якщо виняток не буде викликано
        }
    }
}