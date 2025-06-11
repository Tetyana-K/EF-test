using System;
using System.Collections.Generic;

namespace Database_First_again.Models;

public partial class Car
{
    public int Id { get; set; }

    public string? Brand { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? Date { get; set; }

    public int Count { get; set; }
}
