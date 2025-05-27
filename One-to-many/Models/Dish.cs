using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_to_many.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;

        [Required, StringLength(100)]
        public string Description { get; set; } = String.Empty;

        [Range(0.0, 1000.0, ErrorMessage = "Price must be between 0 and 1000.")]
        public decimal Price { get; set; }

        //[ForeignKey("Menu")] можемо вказати явно, що це зовн ключ (1-й спосіб)
        public int MenuId { get; set; } // Foreign key для Меню, можна не задавати, EF Core сам створить це поле (але маємо кращий контроль та читабельність, якщо задамо)
        
        //[ForeignKey(nameof(MenuId))] // або так (2-й спосіб) – вказуємо, що MenuId є зовнішнім ключем для навігаційної властивості Menu
        public Menu? Menu { get; set; } // навігаційна властивість
    }
}