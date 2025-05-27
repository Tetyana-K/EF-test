using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_to_one.Models
{
    public class Address
    {
        [Key, ForeignKey("Person")] // PersonId одночасно є первинним ключем ([Key]) і зовнішнім ключем ([ForeignKey("Person")]) до Person
        public int PersonId { get; set; } 

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 100 characters.")]
        public string Street { get; set; } = String.Empty;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        public string City { get; set; } = String.Empty;

        Person? Person { get; set; } // навігаційна властивість, яка вказує на особу, до якої належить адреса
    }
}