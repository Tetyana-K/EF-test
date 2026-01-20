
using System;

namespace Src
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
    }

    public static class CarValidator
    {
        public static bool Validate(Car car)
        {
            if (string.IsNullOrWhiteSpace(car.Brand)) return false;
            if (string.IsNullOrWhiteSpace(car.Model)) return false;
            if (car.Year < 1900 || car.Year > DateTime.Now.Year) return false;
            if (car.Mileage < 0) return false;
            return true;
        }
    }
}
