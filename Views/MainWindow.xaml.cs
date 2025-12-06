using System.Windows;
using CuteCalculator.ViewModels;

namespace CuteCalculator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }
    }
}
