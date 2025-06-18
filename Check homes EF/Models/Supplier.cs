using System;
using System.Collections.Generic;

namespace Check_homes_EF.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
