using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBogus.Tests
{
    public class StudentValidator
    {
        public bool HasValidName(string name) =>
            !string.IsNullOrWhiteSpace(name);

        public bool HasValidEmail(string email) =>
            !string.IsNullOrWhiteSpace(email) &&
            email.Contains("@") &&
            email.Contains(".") &&
            email.IndexOf("@") < email.LastIndexOf(".")
            && !email.EndsWith(".");

        public bool HasPassed(int grade) => grade >= 60;

        public bool IsAdult(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }

        public bool IsCourseValid(int course, DegreeLevel degree)
        {
            return degree switch
            {
                DegreeLevel.Bachelor => course >= 1 && course <= 4,
                DegreeLevel.Master => course >= 1 && course <= 2,
                _ => false
            };
        }
    }

}
