using Company_EF.Data;
using Company_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_EF.Services
{
    public class EmployeeService
    {
        private readonly CompanyContext context;

        public EmployeeService(CompanyContext context)
        {
            this.context = context;
        }

        public void AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return  context.Employees.ToList();
        }

        public Employee? GetById(int id)
        {
            return  context.Employees.Find(id);
        }
    }

}
