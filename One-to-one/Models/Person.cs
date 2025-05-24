using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace One_to_one.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(40, ErrorMessage = "First name must be between 2 and 40 characters")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Lastt name is required.")]
        [StringLength(40, ErrorMessage = "Last name must be between 2 and 40 characters")]
        public string LastName { get; set; } = String.Empty;

        // навігаційна властивість
        public Address? Address { get; set; }
        //public Address Address { get; set; } = null!; // якщо хочемо, щоб адреса була завжди
    }
}
