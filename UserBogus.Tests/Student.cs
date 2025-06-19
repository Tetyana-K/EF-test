using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBogus.Tests
{
    public enum DegreeLevel
    {
        Bachelor,
        Master
    }

    public class Student
    {
        public string FullName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int Grade { get; set; }
        public DateTime BirthDate { get; set; }
        public string Faculty { get; set; } = String.Empty;
        public int Course { get; set; } = 1;
        public DegreeLevel Degree { get; set; } = DegreeLevel.Bachelor;
    }

}
