using System;

namespace CuteCalculator.Services
{
    public class Percentage : IOperation
    {
        // Compute percentage based on first operand
        public double Compute(double firstOperand, double currentValue)
        {
            // percentage of firstOperand
            return firstOperand * currentValue / 100.0;
        }
    }
}
