using System;

namespace CuteCalculator.Services
{
    public class LogOperation : IScientificOperation
    {
        public double Calculate(double value) => Math.Log10(value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
