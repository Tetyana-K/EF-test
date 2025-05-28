using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_to_many_FluentApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        // назва категорії
        public string Name { get; set; } = String.Empty;

        // навігаційна властивість – одна категорія має багато товарів
        public List<Product> Products { get; set; } = new List<Product>();

        
    }
}
