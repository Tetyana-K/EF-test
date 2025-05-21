using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Student.Models
{
    public class Student // entity class
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public decimal AverageMark { get; set; }
    }
}
