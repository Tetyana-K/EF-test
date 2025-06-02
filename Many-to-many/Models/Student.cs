using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_many.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;
        
        public DateTime BirthDate { get; set; }
        
        // навігаційна властивість для зв'язку з курсами
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
