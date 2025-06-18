using System;
using System.Collections.Generic;

namespace Check_homes_EF.Models;

public partial class ProductLog
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public DateTime? InsertedAt { get; set; }
}
