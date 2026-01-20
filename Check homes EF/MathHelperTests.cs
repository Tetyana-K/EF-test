
using NUnit.Framework;
using System;
using Src;

namespace Tests
{
    public class MathHelperTests
    {
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(5, 120)]
        public void Factorial_ReturnsCorrectResult(int n, long expected)
        {
            var result = MathHelper.Factorial(n);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Factorial_ThrowsArgumentException_WhenNegative()
        {
            Assert.Throws<ArgumentException>(() => MathHelper.Factorial(-1));
        }

        [Test]
        public void Factorial_ThrowsOverflowException_WhenTooLarge()
        {
            Assert.Throws<OverflowException>(() => MathHelper.Factorial(101));
        }
    }
}
