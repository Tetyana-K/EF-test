using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_many.Models
{
    public  class Course : IValidatableObject
    {
        public int Id { get; set; }

        [MinLength(5)] //  'Computer Science 100'
        [RegularExpression(@"^[A-Za-zА-Яа-яЇїІіЄєҐґ0-9 \-]+$", ErrorMessage = "Name can contain only letters, digits, spaces, and hyphens.")]
        public string Name { get; set; } = String.Empty;

        // додаткові властивості курсу
        public string Description { get; set; } = String.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // навігаційна властивість для зв'язку зі студентами
        public ICollection<Student> Students { get; set; } = new List<Student>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "End date cannot be earlier than start date.",
                    new[] { nameof(EndDate) });
            }
        }
    }
}
