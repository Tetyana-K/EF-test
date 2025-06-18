using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Plus__import_csv_Products_.Models
{
    public enum Category
    {
        Electronics,
        Clothing,
        HomeAppliances,
        Books,
        Toys,
        SchoolSupplies,
    }
    public class Product
    {
        [Ignore] // цей атрибут вказує, що ця властивість (Id) не повинна бути включена при імпорті з CSV (якщо у таблиці Ід автогенерується)
        public int Id { get; set; }           // identity
        //[Name("Title")]  - атрибут для вказання імені колонки в CSV, якщо потрібно (коли назва колонки у CSV не співпадає з назвою властивості у моделі)
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category? Category { get; set; } // enum, який представляє категорію продукту
    }

}
