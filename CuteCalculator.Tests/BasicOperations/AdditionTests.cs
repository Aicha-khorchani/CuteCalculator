using Xunit;
using CuteCalculator.ViewModels;
using CuteCalculator.Models;

namespace CuteCalculator.Tests.BasicOperations
{
    public class AdditionTests
    {
        [Fact]
        public void Addition_Works_Correctly()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("8");
            vm.SetOperation(OperationType.Add);
            vm.AppendDigit("12");
            vm.ExecuteOperation();

            Assert.Equal("20", vm.DisplayText);
        }
    }
}
