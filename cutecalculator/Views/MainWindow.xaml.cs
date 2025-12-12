using System.Windows;
using CuteCalculator.ViewModels;

namespace CuteCalculator.Views
{
    public partial class MainWindow : Window {
    public MainWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = new CalculatorViewModel();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error initializing window");
            }
        }
    }
}
