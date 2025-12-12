using Xunit;
using CuteCalculator.ViewModels;
using CuteCalculator.Models;

namespace CuteCalculator.Tests.Scientific
{
    public class PercentageTests
    {
        [Fact]
        public void Percentage_Of_FirstOperand_Works()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("200");
            vm.SetOperation(OperationType.Multiply);
            vm.AppendDigit("10");
            vm.ApplyPercentage(); // should compute 10% of 200 → 20

            Assert.Equal("20", vm.DisplayText);
        }
    }
}
