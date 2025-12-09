using System;

namespace CuteCalculator.Services
{
    public class PowOperation : IScientificOperation
    {
        public double Calculate(double value) => throw new NotImplementedException();
        public double Calculate(double value, double value2) => Math.Pow(value, value2);
    }
}
