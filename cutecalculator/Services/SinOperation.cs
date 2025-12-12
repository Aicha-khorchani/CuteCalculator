using System;

namespace CuteCalculator.Services
{
    public class SinOperation : IScientificOperation
    {
        public double Calculate(double value) => Math.Sin(value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
