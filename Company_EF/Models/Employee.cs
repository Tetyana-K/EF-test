using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_EF.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = String.Empty;
        public string? Department { get; set; }
        public decimal Salary { get; set; }
    }

}
