namespace One_to_many_FluentApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        // назва продукту
        public string Name { get; set; } = String.Empty;

        // опис продукту
        public string Description { get; set; } = String.Empty;

        // ціна продукту
        public decimal Price { get; set; }

        // зовнішній ключ для категорії
        public int? CategoryId { get; set; } // тут для прикладу взято nullable, тобто продукт може не мати категорії

        // навігаційна властивість для категорії
        public Category? Category { get; set; }

        public int ManufacturerId { get; set; } // not nullable, тобто продукт повинен мати виробника
        // навігаційна властивість для виробника
        public Manufacturer? Manufacturer { get; set; }
    }
}