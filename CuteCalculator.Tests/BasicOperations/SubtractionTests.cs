using Xunit;
using CuteCalculator.ViewModels;
using CuteCalculator.Models;

namespace CuteCalculator.Tests.BasicOperations
{
    public class SubtractionTests
    {
        [Fact]
        public void Subtraction_Works_Correctly()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("20");
            vm.SetOperation(OperationType.Subtract);
            vm.AppendDigit("7");
            vm.ExecuteOperation();

            Assert.Equal("13", vm.DisplayText);
        }
    }
}
