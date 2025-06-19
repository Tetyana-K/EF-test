using Bogus;
// dotnet add package Bogus

namespace CalculatorNS.Tests
{
    public class CalcualtorTestsByBogus
    {
        private Calculator calculator;
        private Faker faker;

        // [SetUp] - �����, ���� ���������� ����� ������ ������
        [SetUp]
        public void Setup()
        {
            // ���������� ��������� ������������ ����� ������ ������
            calculator = new Calculator();

            // ���������� Faker ��� ��������� ���������� �����
            faker = new Faker(); 
        }

        [Test]
        public void Calculator_Add_ReturnsCorrectSum()
        {
            // Arrange

            // ������������� Faker ��� ��������� ���������� �����
            double a = faker.Random.Double(-100, 100);
            double b = faker.Random.Double(-100, 100);

            // Act: �������� ��, ��� �� �������
            double result = calculator.Add(a, b);

            // Assert: ����������, �� ��������� ������� ����������� ��������
           
            Assert.That(result, Is.EqualTo(a + b));
        }
        
        [Test]
        public void Calculator_Divide_ByZero_ThrowsDivideByZeroException()
        {
        
            // Arrange
            double a = faker.Random.Double(-100, 100);
            // Act & Assert
            Assert.Throws<DivideByZeroException>( // assert.Throws - ��������, �� �������� �������
                () => calculator.Divide(a, 0), // �������, ��� ��������� ����������
                "Expected DivideByZeroException when dividing by zero"); // �����������, ��� ���� ��������, ���� ������� �� ���� ���������
        }
    }
}