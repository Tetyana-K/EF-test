// dotnet add package Microsoft.EntityFrameworkCore.InMemory - ��� ���������� � InMemory ����� �����
// InMemory ���� ����� ��������������� ��� ����������, ��� �� ���������� ������� ���� �����

using Company_EF.Data;
using Company_EF.Models;
using Company_EF.Services;
using Microsoft.EntityFrameworkCore;

namespace Company_EF.Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private CompanyContext context;
        private EmployeeService service;

        [SetUp]
        public void Setup()
        {
            // ������������� InMemory ���� ����� ��� ����������
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase("TestCompanyDb")
                .Options;

            context = new CompanyContext(options); // ��������� �������� � InMemory ����� �����
            service = new EmployeeService(context); // ��������� ����� � ��� ����������
        }

        [Test]
        public void AddEmployee_AddsEmployeeToDatabase()
        {
            var employee = new Employee 
            {
                FullName = "Sofiia Melnyk",
                Department = "IT",
                Salary = 4500
            };

            service.AddEmployee(employee); // ������ ����������� ����� �����
            var all = service.GetAll(); // �������� ��� �����������

            Assert.That(all.Count, Is.EqualTo(1)); // ����������, �� � ��� ���� ����������
            Assert.That(all[0].FullName, Is.EqualTo("Sofiia Melnyk"));// ����������, �� ��'� ����������� ���������
        }

        [Test]
        public void GetById_ReturnsCorrectEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                FullName = "Anna Petrova",
                Department = "HR",
                Salary = 3500
            };
            service.AddEmployee(employee);

            // Act
            var foundEmployee = service.GetById(employee.Id);

            // Assert
            Assert.That(foundEmployee, Is.Not.Null);
            Assert.That(foundEmployee.FullName, Is.EqualTo("Anna Petrova"));
            Assert.That(foundEmployee.Department, Is.EqualTo("HR"));
            Assert.That(foundEmployee.Salary, Is.EqualTo(3500));
        }

        [Test]
        public void GetById_ReturnsNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int invalidId = 999; // ��, ����� ���� � ���

            // Act
            var result = service.GetById(invalidId); // �������, �� ����������� null

            // Assert
            Assert.That(result, Is.Null);
        }
    }

}