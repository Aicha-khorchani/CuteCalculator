using System;

namespace CuteCalculator.Services
{
    public class SquareOperation : IScientificOperation
    {
        public double Calculate(double value) => value * value;
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
