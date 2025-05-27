using System.ComponentModel.DataAnnotations;

namespace One_to_many.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;

        // навігаційна властивість – одне меню має багато страв
        public List<Dish> Dishes { get; set; } = new List<Dish>();

    }
}
