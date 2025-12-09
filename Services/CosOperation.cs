using System;

namespace CuteCalculator.Services
{
    public class CosOperation : IScientificOperation
    {
        public double Calculate(double value) => Math.Cos(value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
