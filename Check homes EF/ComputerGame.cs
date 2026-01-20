using System;
using System.ComponentModel.DataAnnotations;

namespace GameEFCoreApp
{
    public class ComputerGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }
    }
}
