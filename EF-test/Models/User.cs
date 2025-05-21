using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_test.Models
{
    public class User
    {
        /* Id буде первинний ключ (Primary Key) за замовчуванням (на основы домовленостей =conventions). EF Core автоматично розпізнає властивість з назвою Id 
         * або ClassNameId (наприклад, UserId) як первинний ключ, без додаткових атрибутів або Fluent API.*/
       // [Key] //  Необов'язково, якщо назва властивості - Id або UserId
        public int Id { get; set; }
       // [Required] //обов'язкове поле
        [StringLength(40, ErrorMessage = "Ім'я повинно містити від 2 до 40 символів")] //максимальна довжина 40 символів
        [Column("FullName")] // стовпець у таблиці буде називатися FullName
        public string Name { get; set; } = string.Empty; // властивості  (conventions) відповідатиме у таблиці users поле is not NULL
        public string? Email { get; set; } // цій властивості  (за домовленостями, conventions) відповідатиме у таблиці users поле is NULL
    }
}
