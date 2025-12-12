using Xunit;
using CuteCalculator.ViewModels;

namespace CuteCalculator.Tests.Scientific
{
    public class PowerTests
    {
        [Fact]
        public void Power_Operation_Works()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("5");
            vm.PreparePow();
            vm.AppendDigit("3");
            vm.ExecutePow();

            Assert.Equal("125", vm.DisplayText);
        }
    }
}
