using System;

namespace CuteCalculator.Services
{
    public class TanOperation : IScientificOperation
    {
        public double Calculate(double value) => Math.Tan(value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
