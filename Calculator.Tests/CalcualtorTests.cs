namespace Calculator.Tests
{
    public class CalcualtorTests
    {
        Calculator calculator;

        // [OneTimeSetUp] -�����, ���� ���������� ���� ��� ����� �������� ��� ����� � ����

        // [SetUp]    - �����, ���� ���������� ����� ������ ������
        [SetUp]
        public void Setup()
        {
            // ���������� ��������� ������������ ����� ������ ������
            calculator = new Calculator();
        }

        [Test]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            // Arrange, Act, Assert pattern

            // Arrange: ������������ ����� ��� �����
            Calculator calculator = new Calculator();

            // Act: �������� ��, ��� �� �������
            double result = calculator.Add(2, 3);

            // Assert: ����������, �� ��������� ������� ����������� ��������
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
            Assert.Throws<DivideByZeroException>( // assert.Throws - ��������, �� �������� �������
                () => calculator.Divide(10, 0), // �������, ��� ��������� ����������
                "Expected DivideByZeroException when dividing by zero"); // �����������, ��� ���� ��������, ���� ������� �� ���� ���������
        }
    }
}