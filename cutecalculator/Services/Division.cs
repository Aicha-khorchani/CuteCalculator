using System;

namespace CuteCalculator.Services
{
    public class Division : IOperation
    {
        public double Compute(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }
}
