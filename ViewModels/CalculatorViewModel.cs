using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CuteCalculator.Models;
using CuteCalculator.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CuteCalculator.Services.ScientificOperations;

namespace CuteCalculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<OperationType, IOperation> _operations;

        private string _displayText = "0";
        public string DisplayText
        {
            get => _displayText;
            set { _displayText = value; OnPropertyChanged(); }
        }
        public ObservableCollection<OperationResult> History { get; } = new();
        private string _formulaText = "";
        // History toggle
        private bool _isHistoryVisible = false;
        public bool IsHistoryVisible
        {
            get => _isHistoryVisible;
            set
            {
                _isHistoryVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentDisplay));
            }
        }


        // History preview (last 1–2 operations)
        public ObservableCollection<OperationResult> HistoryPreview
        {
            get
            {
                var preview = new ObservableCollection<OperationResult>();
                for (int i = 0; i < Math.Min(2, History.Count); i++)
                    preview.Add(History[History.Count - 1 - i]); // newest first
                return preview;
            }
        }

        // Display content: switches between formula/input and history preview
        public DisplayState CurrentDisplay => IsHistoryVisible
                ? null
                : new DisplayState { Formula = FormulaText, Result = DisplayText };

        public string FormulaText
        {
            get => _formulaText;
            set
            {
                _formulaText = value;
                OnPropertyChanged();
            }
        }
        private double? _firstOperand = null;
        private OperationType _currentOperation = OperationType.None;
        private bool _isNewInput = true; // overwrite vs append

        // Commands
        public ICommand DigitCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand DecimalCommand { get; }
        public ICommand ToggleSignCommand { get; }
        public ICommand PercentageCommand { get; }
        public ICommand ToggleHistoryCommand { get; }
        public ICommand ExpCommand { get; }
        public ICommand DeleteLastCommand { get; }
public ICommand SqrtCommand { get; }
public ICommand SquareCommand { get; }
public ICommand ReciprocalCommand { get; }
public ICommand PowCommand { get; }
public ICommand SinCommand { get; }
public ICommand CosCommand { get; }
public ICommand TanCommand { get; }
public ICommand LnCommand { get; }
public ICommand LogCommand { get; }
public ICommand FactorialCommand { get; }
// this is fully for ui purpuses i struggle to fit in all my buttons while keeping ui consistent 
//number of buttons in row consistent and number of button in colom as well so i added this 
public ICommand HeartAnimationCommand { get; }

private bool _isScientificVisible;

public bool IsScientificVisible
{
    get => _isScientificVisible;
    set
    {
        if (_isScientificVisible != value)
        {
            _isScientificVisible = value;
            OnPropertyChanged();
        }
    }
}

private double? _secondOperandForPow = null;

        public CalculatorViewModel()
        {
            _operations = new Dictionary<OperationType, IOperation>
            {
                { OperationType.Add, new Sum() },
                { OperationType.Subtract, new Subtraction() },
                { OperationType.Multiply, new Multiplication() },
                { OperationType.Divide, new Division() },
            };
            PercentageCommand = new RelayCommand(_ => ApplyPercentage());
            DigitCommand = new RelayCommand<string>(AppendDigit);
            OperationCommand = new RelayCommand<string>(op => SetOperation(ParseOperation(op)));
            EqualsCommand = new RelayCommand(_ => ExecuteOperation());
            ClearCommand = new RelayCommand(_ => Clear());
            BackspaceCommand = new RelayCommand(_ => Backspace());
            DecimalCommand = new RelayCommand(_ => AppendDecimal());
            ToggleSignCommand = new RelayCommand(_ => ToggleSign());
            ToggleHistoryCommand = new RelayCommand(_ => { IsHistoryVisible = !IsHistoryVisible; });
          // Scientific command initialization
            SqrtCommand = new RelayCommand(_ => ExecuteScientific(new SqrtOperation()));
            SquareCommand = new RelayCommand(_ => ExecuteScientific(new SquareOperation()));
            ReciprocalCommand = new RelayCommand(_ => ExecuteScientific(new ReciprocalOperation()));
            PowCommand = new RelayCommand(_ => PreparePow());
            SinCommand = new RelayCommand(_ => ExecuteScientific(new SinOperation()));
            CosCommand = new RelayCommand(_ => ExecuteScientific(new CosOperation()));
            TanCommand = new RelayCommand(_ => ExecuteScientific(new TanOperation()));
            LnCommand = new RelayCommand(_ => ExecuteScientific(new LnOperation()));
            LogCommand = new RelayCommand(_ => ExecuteScientific(new LogOperation()));
            FactorialCommand = new RelayCommand(_ => ExecuteScientific(new FactorialOperation()));
            HeartAnimationCommand = new RelayCommand(_ => OnHeartAnimation());
            ExpCommand = new RelayCommand(_ => ApplyExpOperation());
            DeleteLastCommand = new RelayCommand(_ => DeleteLastCharacter());

        }

        #region Core Methods


private void DeleteLastCharacter()
{
    if (!string.IsNullOrEmpty(DisplayText))
    {
        DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
    }
}
private readonly ExpOperation _expOperation = new ExpOperation();

private void AddToHistory(string formula, string result = "")
{
    History.Add(new OperationResult
    {
        Formula = formula,
        Result = string.IsNullOrEmpty(result) ? DisplayText : result
    });

    // keep last 2 only
    while (History.Count > 2)
        History.RemoveAt(0);

    OnPropertyChanged(nameof(CurrentDisplay));
}

private void ApplyExpOperation()
{
    if (double.TryParse(DisplayText, out double value))
    {
        double result = _expOperation.Execute(value);
        DisplayText = result.ToString();
        AddToHistory($"e^{value} = {result}");
    }
}

private bool _isHeartAnimated;
public bool IsHeartAnimated
{
    get => _isHeartAnimated;
    set
    {
        _isHeartAnimated = value;
        OnPropertyChanged(nameof(IsHeartAnimated));
    }
}

private async void OnHeartAnimation()
{
    IsHeartAnimated = true;

    // Animation lasts 1 second
    await Task.Delay(1000);

    IsHeartAnimated = false;
}
private void ExecuteScientific(IScientificOperation operation)
{
    try
    {
        double currentValue = ParseDisplayText();
        double result = operation.Calculate(currentValue);

        // Update display
        DisplayText = result.ToString(CultureInfo.InvariantCulture);

        // Save history (latest 2)
        AddToHistory(FormulaText + currentValue, result.ToString(CultureInfo.InvariantCulture));

        // Prepare for next input
        _firstOperand = result;
        _currentOperation = OperationType.None;
        _isNewInput = true;

        OnPropertyChanged(nameof(CurrentDisplay));
    }
    catch
    {
        DisplayText = "Error";
        _firstOperand = null;
        _currentOperation = OperationType.None;
        _isNewInput = true;
    }
}

// For two-operand operation: x^y
private void PreparePow()
{
    _firstOperand = ParseDisplayText();
    _isNewInput = true;
    FormulaText += "^";
    OnPropertyChanged(nameof(CurrentDisplay));
}

// Call this after entering the second value for x^y
public void ExecutePow()
{
    if (!_firstOperand.HasValue) return;

    try
    {
        double secondOperand = ParseDisplayText();
        var powOp = new PowOperation();
        double result = powOp.Calculate(_firstOperand.Value, secondOperand);

        DisplayText = result.ToString(CultureInfo.InvariantCulture);

        AddToHistory($"{_firstOperand}^{secondOperand}", result.ToString(CultureInfo.InvariantCulture));


        _firstOperand = result;
        _currentOperation = OperationType.None;
        _isNewInput = true;
        OnPropertyChanged(nameof(CurrentDisplay));
    }
    catch
    {
        DisplayText = "Error";
        _firstOperand = null;
        _currentOperation = OperationType.None;
        _isNewInput = true;
    }
}

        private void ApplyPercentage()
        {
            try
            {
                double currentValue = ParseDisplayText();

                if (_firstOperand.HasValue && _currentOperation != OperationType.None)
                {
                    // Treat as percentage of the first operand
                    var percentageService = new Percentage();
                    double percentValue = percentageService.Compute(_firstOperand.Value, currentValue);
                    DisplayText = percentValue.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    // If no operator, just divide by 100
                    DisplayText = (currentValue / 100.0).ToString(CultureInfo.InvariantCulture);
                }

                _isNewInput = true; // Next digit overwrites display
            }
            catch
            {
                DisplayText = "Error";
                _firstOperand = null;
                _currentOperation = OperationType.None;
                _isNewInput = true;
            }
        }

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

            // Update formula
            FormulaText += digit;
            OnPropertyChanged(nameof(CurrentDisplay));
        }


        public void AppendDecimal()
        {
            if (_isNewInput)
            {
                DisplayText = "0.";
                _isNewInput = false;
                FormulaText += "0.";
            }
            else if (!DisplayText.Contains("."))
            {
                DisplayText += ".";
                FormulaText += ".";
            }
            OnPropertyChanged(nameof(CurrentDisplay));
        }


        public void ToggleSign()
        {
            if (DisplayText == "0") return;

            if (DisplayText.StartsWith("-"))
                DisplayText = DisplayText.Substring(1);
            else
                DisplayText = "-" + DisplayText;
        }
        private double ParseDisplayText()
        {
            if (double.TryParse(DisplayText, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                return val;
            return 0;
        }
  public void SetOperation(OperationType op)
{
    if (_firstOperand.HasValue && !_isNewInput && _currentOperation != OperationType.None)
    {
        ExecuteOperation();
    }

    _firstOperand = ParseDisplayText(); 
    _currentOperation = op;
    _isNewInput = true;

    // Update formula display
    FormulaText += op switch
    {
        OperationType.Add => " + ",
        OperationType.Subtract => " - ",
        OperationType.Multiply => " × ",
        OperationType.Divide => " ÷ ",
        _ => ""
    };

    OnPropertyChanged(nameof(CurrentDisplay));
}
        public void ExecuteOperation()
        {
            try
            {
                if (_currentOperation == OperationType.None || !_firstOperand.HasValue) return;

                double secondOperand = ParseDisplayText();
                var opHandler = _operations[_currentOperation];
                double result = opHandler.Compute(_firstOperand.Value, secondOperand);

                // Save history
                AddToHistory(FormulaText + secondOperand, result.ToString(CultureInfo.InvariantCulture));

                DisplayText = result.ToString(CultureInfo.InvariantCulture);
                _firstOperand = result;
                _currentOperation = OperationType.None;
                _isNewInput = true;

                OnPropertyChanged(nameof(CurrentDisplay));
            }
            catch (DivideByZeroException)
            {
                DisplayText = "Error";
                _firstOperand = null;
                _currentOperation = OperationType.None;
                _isNewInput = true;

                OnPropertyChanged(nameof(CurrentDisplay));
            }
            catch (Exception)
            {
                DisplayText = "Error";
                _firstOperand = null;
                _currentOperation = OperationType.None;
                _isNewInput = true;

                OnPropertyChanged(nameof(CurrentDisplay));
            }
        }

        public void Clear()
        {
            DisplayText = "0";
            FormulaText = "";
            _firstOperand = null;
            _currentOperation = OperationType.None;
            _isNewInput = true;
            OnPropertyChanged(nameof(CurrentDisplay));
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

        private OperationType ParseOperation(string op) => op switch
        {
            "+" => OperationType.Add,
            "-" => OperationType.Subtract,
            "×" or "*" => OperationType.Multiply,
            "÷" or "/" => OperationType.Divide,
            _ => OperationType.None
        };

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }

}
