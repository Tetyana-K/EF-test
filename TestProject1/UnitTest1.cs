using CalculatorNS;

namespace TestProject1
{
    public class Tests
    {
        private Calculator calculator;
        
        [SetUp] // метод, який виконується перед кожним тестом
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_ReturnsSum()
        {
            // AAA template: Arrange, Act, Assert
            // Arrange - початкове налаштування для тесту
            double a = 5;
            double b = 3;

            // Act - виконання дії (calculator.Add), яку ми тестуємо
            double result = calculator.Add(a, b);

            // Assert - перевірка, чи результат відповідає очікуваному значенню
            Assert.AreEqual(a + b , result);
            //Assert.That(result, Is.EqualTo(8));
        }
    }
}