using System;

namespace CuteCalculator.Services
{
    public class FactorialOperation : IScientificOperation
    {
        public double Calculate(double value) => Factorial((int)value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();

        private double Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }
    }
}
