using System;
using System.Collections.Generic;

namespace Database_First.Models;

public partial class Player
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }

    public int Age { get; set; }
}
