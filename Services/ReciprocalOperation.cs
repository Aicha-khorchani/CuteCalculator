using System;

namespace CuteCalculator.Services
{
    public class ReciprocalOperation : IScientificOperation
    {
        public double Calculate(double value) => 1 / value;
        public double Calculate(double value, double value2) => throw new NotImplementedException();
    }
}
