// dotnet add package Microsoft.EntityFrameworkCore.InMemory - для тестування з InMemory базою даних
// InMemory база даних використовується для тестування, щоб не створювати реальну базу даних

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
            // Використовуємо InMemory базу даних для тестування
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase("TestCompanyDb")
                .Options;

            context = new CompanyContext(options); // створюємо контекст з InMemory базою даних
            service = new EmployeeService(context); // створюємо сервіс з цим контекстом
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

            service.AddEmployee(employee); // додаємо співробітника через сервіс
            var all = service.GetAll(); // отримуємо всіх співробітників

            Assert.That(all.Count, Is.EqualTo(1)); // перевіряємо, що в базі один співробітник
            Assert.That(all[0].FullName, Is.EqualTo("Sofiia Melnyk"));// перевіряємо, що ім'я співробітника правильне
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
            int invalidId = 999; // Ід, якого немає в базі

            // Act
            var result = service.GetById(invalidId); // очікуємо, що повернеться null

            // Assert
            Assert.That(result, Is.Null);
        }
    }

}