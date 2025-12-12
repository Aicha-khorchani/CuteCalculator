using Xunit;
using CuteCalculator.ViewModels;

namespace CuteCalculator.Tests.Memory
{
    public class MemoryTests
    {
        [Fact]
        public void Scientific_Result_Becomes_FirstOperand()
        {
            var vm = new CalculatorViewModel();

            vm.AppendDigit("9");
            vm.SqrtCommand.Execute(null); // √9 = 3

            vm.AppendDigit("2");
            vm.SetOperation(CuteCalculator.Models.OperationType.Add);
            vm.AppendDigit("5");
            vm.ExecuteOperation();

            Assert.Equal("7", vm.DisplayText);
        }
    }
}
