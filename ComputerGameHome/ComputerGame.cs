﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerGameHome
{
    public class ComputerGame
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Genre { get; set; } = String.Empty;
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }

    }
}
