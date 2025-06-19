using CalculatorNS;

namespace TestProject1
{
    public class Tests
    {
        private Calculator calculator;
        
        [SetUp] // �����, ���� ���������� ����� ������ ������
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_ReturnsSum()
        {
            // AAA template: Arrange, Act, Assert
            // Arrange - ��������� ������������ ��� �����
            double a = 5;
            double b = 3;

            // Act - ��������� 䳿 (calculator.Add), ��� �� �������
            double result = calculator.Add(a, b);

            // Assert - ��������, �� ��������� ������� ����������� ��������
            Assert.AreEqual(a + b , result);
            //Assert.That(result, Is.EqualTo(8));
        }
    }
}