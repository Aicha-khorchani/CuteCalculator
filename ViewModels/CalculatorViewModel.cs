using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CuteCalculator.Models;
using CuteCalculator.Services;

namespace CuteCalculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        // Services mapping
        private readonly Dictionary<OperationType, IOperation> _operations;

        private string _displayText = "0";
        public string DisplayText
        {
            get => _displayText;
            set { _displayText = value; OnPropertyChanged(); }
        }

        private double? _firstOperand = null;
        private OperationType _currentOperation = OperationType.None;
        private bool _isNewInput = true; // controls overwrite vs append

        // Commands
        public ICommand DigitCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand DecimalCommand { get; }

        public CalculatorViewModel()
        {
            // instantiate operation implementations
            _operations = new Dictionary<OperationType, IOperation>
            {
                { OperationType.Add, new Sum() },
                { OperationType.Subtract, new Subtraction() },
                { OperationType.Multiply, new Multiplication() },
                { OperationType.Divide, new Division() }
            };

            DigitCommand = new RelayCommand<string>(digit => AppendDigit(digit));
            OperationCommand = new RelayCommand<string>(op => SetOperation(ParseOperation(op)));
            EqualsCommand = new RelayCommand(_ => ExecuteOperation());
            ClearCommand = new RelayCommand(_ => Clear());
            BackspaceCommand = new RelayCommand(_ => Backspace());
            DecimalCommand = new RelayCommand(_ => AppendDecimal());
        }

        #region Core methods mapped from MainWindow logic

        public void AppendDigit(string digit)
        {
            if (_isNewInput)
            {
                DisplayText = digit == "0" ? "0" : digit;
                _isNewInput = digit == "0";
            }
            else
            {
                if (DisplayText == "0")
                    DisplayText = digit;
                else
                    DisplayText += digit;
            }
        }

        public void AppendDecimal()
        {
            if (_isNewInput)
            {
                DisplayText = "0.";
                _isNewInput = false;
            }
            else if (!DisplayText.Contains("."))
            {
                DisplayText += ".";
            }
        }

        public void SetOperation(OperationType op)
        {
            // If we already had a first operand and the user presses an operator,
            // we can optionally compute intermediate result.
            if (_firstOperand.HasValue && !_isNewInput && _currentOperation != OperationType.None)
            {
                ExecuteOperation();
            }

            _firstOperand = double.TryParse(DisplayText, out var val) ? val : 0;
            _currentOperation = op;
            _isNewInput = true;
        }

        public void ExecuteOperation()
        {
            try
            {
                if (_currentOperation == OperationType.None || !_firstOperand.HasValue)
                {
                    return; // nothing to do
                }

                double secondOperand = double.TryParse(DisplayText, out var v) ? v : 0;
                var opHandler = _operations[_currentOperation];
                double result = opHandler.Compute(_firstOperand.Value, secondOperand);

                DisplayText = result.ToString();
                _firstOperand = result;
                _currentOperation = OperationType.None;
                _isNewInput = true;
            }
            catch (DivideByZeroException)
            {
                DisplayText = "Error";
                _firstOperand = null;
                _currentOperation = OperationType.None;
                _isNewInput = true;
            }
            catch (Exception)
            {
                DisplayText = "Error";
                _firstOperand = null;
                _currentOperation = OperationType.None;
                _isNewInput = true;
            }
        }

        public void Clear()
        {
            DisplayText = "0";
            _firstOperand = null;
            _currentOperation = OperationType.None;
            _isNewInput = true;
        }

        public void Backspace()
        {
            if (_isNewInput)
            {
                DisplayText = "0";
                _isNewInput = true;
                return;
            }

            if (DisplayText.Length <= 1)
            {
                DisplayText = "0";
                _isNewInput = true;
            }
            else
            {
                DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            }
        }

        #endregion

        private OperationType ParseOperation(string op)
        {
            return op switch
            {
                "+" => OperationType.Add,
                "-" => OperationType.Subtract,
                "×" or "*" => OperationType.Multiply,
                "÷" or "/" => OperationType.Divide,
                _ => OperationType.None
            };
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion
    }
}
