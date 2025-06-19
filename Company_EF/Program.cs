// See https://aka.ms/new-console-template for more information
using Company_EF.Data;
using Company_EF.Models;
using Company_EF.Services;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("____Employees_____");
//using var db = new CompanyContext();
//Employee e = new Employee() { FullName = "Maxym Ivanenko", Department = "Development", Salary = 2000};
//db.Employees.Add(e);
//db.SaveChanges();
//Console.WriteLine($"Employee {e.FullName} added with ID {e.Id}");
//var employees = db.Employees.ToList();
//Console.WriteLine("Employees in the database:");

//foreach (var emp in employees)
//{
//    Console.WriteLine($"ID: {emp.Id}, Name: {emp.FullName}, Department: {emp.Department}, Salary: {emp.Salary}");
//}

EmployeeService employeeService = new EmployeeService(new CompanyContext());
Employee e = new Employee() { FullName = "Ivan Bilenkiy", Department = "Development", Salary = 2400 };
employeeService.AddEmployee(e);
Console.WriteLine($"Employee {e.FullName} added with ID {e.Id}");
var employees = employeeService.GetAll();
Console.WriteLine("Employees in the database:");
foreach (var emp in employees)
{
    Console.WriteLine($"ID: {emp.Id}, Name: {emp.FullName}, Department: {emp.Department}, Salary: {emp.Salary}");
}