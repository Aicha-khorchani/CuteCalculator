using Xunit;
using CuteCalculator.ViewModels;
using System.Globalization;

namespace CuteCalculator.Tests.Scientific
{
    public class TangentTests
    {
        [Fact]
        public void Tan_Works()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("0");
            vm.TanCommand.Execute(null);

            Assert.Equal("0", vm.DisplayText);
        }
    }
}
