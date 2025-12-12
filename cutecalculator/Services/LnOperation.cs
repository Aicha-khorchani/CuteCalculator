using System;

namespace CuteCalculator.Services
{
    public class LnOperation : IScientificOperation
    {
        public double Calculate(double value) => Math.Log(value);
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
