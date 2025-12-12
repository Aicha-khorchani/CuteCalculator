using Xunit;
using CuteCalculator.ViewModels;
using System.Globalization;

namespace CuteCalculator.Tests.Scientific
{
    public class NaturalLogTests
    {
        [Fact]
        public void Ln_Of_One_Is_Zero()
        {
            var vm = new CalculatorViewModel();

            vm.DisplayText = "1";
            vm.LnCommand.Execute(null);

            double result = double.Parse(vm.DisplayText, CultureInfo.InvariantCulture);
            Assert.InRange(result, -0.000001, 0.000001); // ln(1) = 0
        }

        [Fact]
        public void Ln_Of_E_Is_One()
        {
            var vm = new CalculatorViewModel();

            vm.DisplayText = "2.718281828"; // e
            vm.LnCommand.Execute(null);

            double result = double.Parse(vm.DisplayText, CultureInfo.InvariantCulture);
            Assert.InRange(result, 0.999999, 1.000001); // ln(e) ~ 1
        }

        [Fact]
        public void Ln_Of_Negative_Shows_Error()
        {
            var vm = new CalculatorViewModel();

            vm.DisplayText = "-5";
            vm.LnCommand.Execute(null);

            Assert.Equal("Error", vm.DisplayText);
        }
    }
}
