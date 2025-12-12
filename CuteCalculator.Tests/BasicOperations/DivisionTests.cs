using Xunit;
using CuteCalculator.ViewModels;
using CuteCalculator.Models;

namespace CuteCalculator.Tests.BasicOperations
{
    public class DivisionTests
    {
        [Fact]
        public void Division_Works_Correctly()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("100");
            vm.SetOperation(OperationType.Divide);
            vm.AppendDigit("4");
            vm.ExecuteOperation();

            Assert.Equal("25", vm.DisplayText);
        }

        [Fact]
        public void Division_By_Zero_Shows_Error()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("20");
            vm.SetOperation(OperationType.Divide);
            vm.AppendDigit("0");
            vm.ExecuteOperation();

            Assert.Equal("Error", vm.DisplayText);
        }
    }
}
