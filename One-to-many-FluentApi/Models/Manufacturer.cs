using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_to_many_FluentApi.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        // назва виробника  
        public string Name { get; set; } = String.Empty;

        // навігаційна властивість – один виробник має багато продуктів
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
