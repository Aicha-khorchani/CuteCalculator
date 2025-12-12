using System;

namespace CuteCalculator.Services
{
    public class SqrtOperation : IScientificOperation
    {
        public double Calculate(double value) => Math.Sqrt(value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
