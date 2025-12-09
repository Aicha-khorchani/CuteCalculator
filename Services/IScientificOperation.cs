namespace CuteCalculator.Services
{
    public interface IScientificOperation
    {
        double Calculate(double value);           // Single-operand operations (sqrt, sin, cos)
        double Calculate(double value, double value2); // Two-operand operations (pow)
    }
}
