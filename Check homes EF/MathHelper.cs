
using System;

namespace Src
{
    public static class MathHelper
    {
        public static long Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentException();
            if (n > 100)
                throw new OverflowException();

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
