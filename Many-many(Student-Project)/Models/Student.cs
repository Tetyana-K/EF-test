using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_many__junc_table_Student_Project_.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<StudentProject> StudentProjects { get; set; } = new List<StudentProject>();
    }
}
