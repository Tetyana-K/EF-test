using System.ComponentModel.DataAnnotations;

namespace One_to_one.Models
{
    public class Person
    {
        public int Id { get; set; } // первинний ключ (Primary Key) за замовчуванням (на основі домовленостей =conventions). EF Core автоматично розпізнає властивість з назвою Id
        
        [Required(ErrorMessage = "First name is required.")] 
        [StringLength(40, ErrorMessage = "First name must <= 40 characters")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Lastt name is required.")]
        [StringLength(40, ErrorMessage = "Last name must be <= 40 characters")]
        public string LastName { get; set; } = String.Empty;

        // навігаційна властивість
        public Address? Address { get; set; }
        //public Address Address { get; set; } = null!; // якщо хочемо, щоб адреса була завжди
    }
}
