namespace Calculator.Tests
{
    public class CalcualtorTests
    {
        Calculator calculator;

        // [OneTimeSetUp] -метод, який виконується один раз перед запуском всіх тестів у класі

        // [SetUp]    - метод, який виконується перед кожним тестом
        [SetUp]
        public void Setup()
        {
            // Ініціалізуємо екземпляр калькулятора перед кожним тестом
            calculator = new Calculator();
        }

        [Test]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            // Arrange, Act, Assert pattern

            // Arrange: встановлюємо умови для тесту
            Calculator calculator = new Calculator();

            // Act: виконуємо дію, яку ми тестуємо
            double result = calculator.Add(2, 3);

            // Assert: перевіряємо, чи результат відповідає очікуваному значенню
            //Assert.AreEqual(5, result);
            Assert.That(result, Is.EqualTo(5));
        }
        [Test]
        public void Calculator_Subtract_ReturnsCorrectDifference()
        {
            // Act
            double result = calculator.Subtract(5, 3);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }
        [Test]
        public void Calculator_Multiply_ReturnsCorrectProduct()
        {
            // Act
            double result = calculator.Multiply(4, 5);

            // Assert
            Assert.That(result, Is.EqualTo(20));
        }
        [Test]
        public void Calculator_Divide_ReturnsCorrectQuotient()
        {
            // Act
            double result = calculator.Divide(10, 2);

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }
        [Test]
        public void Calculator_Divide_ByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            Calculator calculator = new Calculator();


            // Act & Assert
            Assert.Throws<DivideByZeroException>( // assert.Throws - перевіряє, що кидається виняток
                () => calculator.Divide(10, 0), // функція, яка спричиняє виключення
                "Expected DivideByZeroException when dividing by zero"); // повідомлення, яке буде виведено, якщо виняток не буде викликано
        }
    }
}