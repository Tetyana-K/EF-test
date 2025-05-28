using System.ComponentModel.DataAnnotations;

namespace One_to_many.Models
{
    public class Menu
    {
        public int Id { get; set; } // первинний ключ (Primary Key) за замовчуванням (на основі домовленостей =conventions). EF Core автоматично розпізнає властивість з назвою Id

        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;

        // навігаційна властивість (список, часто роблять як ICollection) – одне меню має багато страв
        public List<Dish> Dishes { get; set; } = new List<Dish>();

    }
}
