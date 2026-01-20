
using NUnit.Framework;
using Src;
using System;

namespace Tests
{
    public class CarTests
    {
        [Test]
        public void Validate_ReturnsTrue_WhenCarIsValid()
        {
            var car = new Car { Brand = "Toyota", Model = "Camry", Year = 2020, Mileage = 10000 };
            Assert.That(CarValidator.Validate(car), Is.True);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenBrandIsEmpty()
        {
            var car = new Car { Brand = "", Model = "Camry", Year = 2020, Mileage = 10000 };
            Assert.That(CarValidator.Validate(car), Is.False);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenModelIsEmpty()
        {
            var car = new Car { Brand = "Toyota", Model = "", Year = 2020, Mileage = 10000 };
            Assert.That(CarValidator.Validate(car), Is.False);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenYearTooLow()
        {
            var car = new Car { Brand = "Toyota", Model = "Camry", Year = 1800, Mileage = 10000 };
            Assert.That(CarValidator.Validate(car), Is.False);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenYearTooHigh()
        {
            var car = new Car { Brand = "Toyota", Model = "Camry", Year = DateTime.Now.Year + 1, Mileage = 10000 };
            Assert.That(CarValidator.Validate(car), Is.False);
        }

        [Test]
        public void Validate_ReturnsFalse_WhenMileageNegative()
        {
            var car = new Car { Brand = "Toyota", Model = "Camry", Year = 2020, Mileage = -1 };
            Assert.That(CarValidator.Validate(car), Is.False);
        }
    }
}
