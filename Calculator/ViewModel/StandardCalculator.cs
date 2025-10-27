using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Calculator.ViewModel;

namespace Calculator.ViewModel
{
    class CalculatorModelcs : INotifyPropertyChanged
    {
        #region ICommand
        /// <summary>
        /// ICommand 숫자키
        /// </summary>
        public ICommand NumberClickCommand { get; private set; }

        public ICommand DecimalClickCommand { get; private set; }

        public ICommand DeleteClickCommand { get; private set; }
        public ICommand UnrayOperationClickCommand { get; private set; }
        public ICommand ElementaryArithmeticClickCommand { get; private set; }
        public ICommand ResultClickCommand { get; private set; }
        public ICommand ClearClickCommand {  get; private set; }
        public ICommand ExceuteClearEntryClickCommand { get; private set; }
        #endregion


        public CalculatorModelcs()
        {
            NumberClickCommand = new RelayCommand(p => NumberCommand((string)p));
            DecimalClickCommand = new RelayCommand(() => DecimalCommand());
            DeleteClickCommand = new RelayCommand(() => DeleteCommand());
            UnrayOperationClickCommand = new RelayCommand(p => UnrayOperationCommand((string)p));
            ElementaryArithmeticClickCommand = new RelayCommand(p => ElementaryArithmeticCommand((string)p));
            ResultClickCommand = new RelayCommand(() => ResultCommand());
            ClearClickCommand = new RelayCommand(() => ClearCommand());
            ExceuteClearEntryClickCommand = new RelayCommand(() => ExceuteClearEntryCommand());
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region variable
        /// <summary>
        /// 처음 Display 숫자를 0으로 지정하는 변수
        /// </summary>
        private string _displayValue = "0";
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 숫자 버튼을 누르면 보이게 될 Disaply
        /// </summary>
        /// <summary>
        /// 현재 값 저장 변수
        /// </summary>
        public double currentValue = 0.0;
        /// <summary>
        /// 현재 연산자 저장 변수
        /// </summary>
        public string currentOperation = string.Empty;
        /// <summary>
        /// 새 숫자 입력 확인
        /// </summary>
        private bool isNewNumber = true;
        /// <summary>
        /// 기록 열려있는지 닫혀있는지
        /// </summary>
        private bool isSideBarOpen = false;
        /// <summary>
        /// 메모리 값
        /// </summary>
        private double memoryValue = 0;
        /// <summary>
        /// display 0번 행 식 기록
        /// </summary>
        private string _calculationHistory = string.Empty;
        #endregion

        /// <summary>
        /// display에서 1번 행 결과, 현재 숫자
        /// </summary>
        public string DisplayValue
        {
            get => _displayValue;
            set
            {
                if (_displayValue != value)
                {
                    _displayValue = value;
                    OnPropertyChanged(nameof(DisplayValue));
                }
            }
        }

        /// <summary>
        /// display에서 0번 행 식
        /// </summary>
        public string CalculationHistory
        {
            get => _calculationHistory;
            set
            {
                if (_calculationHistory != value)
                {
                    _calculationHistory = value;
                    OnPropertyChanged(nameof(CalculationHistory));
                }
            }
        }


        #region public
        /// <summary>
        /// 더하기
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Add(double x, double y)
        {
            return x + y;
        }


        /// <summary>
        /// 빼기
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Subtraction(double x, double y)
        {
            return x - y;
        }

        /// <summary>
        /// 곱셈
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Multiplication(double x, double y)
        {
            return x * y;
        }

        /// <summary>
        /// 나눗셈
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double Divide(double x, double y)
        {

            if (y == 0)
            {
                throw new ArgumentException("0으로 나눌 수 없습니다.");
            }
            return x / y;
        }


        /// <summary>
        /// 퍼센트 ( 기준 값에 대한 백분율 구할 때 )
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public double PercentageOf(double value, double total)
        {
            return value * (total / 100.0);
        }

        /// <summary>
        /// 퍼센트 ( 값 간 비교 퍼센트 계싼 )
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public double PercentChange(double oldValue, double newValue)
        {
            return ((newValue - oldValue) / oldValue) * 100.0;
        }

        /// <summary>
        /// 부호 변경 (+/-)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double ToggleSign(double value)
        {
            return -value;
        }

        /// <summary>
        /// 제곱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Square(double value)
        {
            return value * value;
        }

        /// <summary>
        /// 제곱근
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double SquareRoot(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("음수의 제곱근은 계산할 수 없습니다.,");
            }
            return Math.Sqrt(value);
        }

        /// <summary>
        /// x분의 1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public double Reciprocal(double value)
        {
            if (value == 0)
                throw new DivideByZeroException("0의 역수는 정의되지 않습니다.");
            return 1.0 / value;
        }

        /// <summary>
        /// 처음에 버튼을 눌르면 0을 지우고 숫자를 표시,
        /// </summary>
        /// <param name="digit"></param>
        public void HandleDigitInput(string digit)
        {
            if (isNewNumber)
            {
                DisplayValue = digit;
                isNewNumber = false;
            }
            else
            {
                DisplayValue += digit;
            }
        }

        /// <summary>
        /// Clear 버튼을 눌렀을 때 동작하는 메서드
        /// </summary>
        public void ClearCommand()
        {
            currentValue = 0.0;
            currentOperation = string.Empty;
            isNewNumber = true;
            DisplayValue = "0";
            CalculationHistory = string.Empty;
        }

        /// <summary>
        /// CE버튼
        /// </summary>
        public void ExceuteClearEntryCommand()
        {
            DisplayValue = "0";
        }

        /// <summary>
        /// 숫자 버튼 클릭 로직
        /// </summary>
        /// <param name="number"></param>
        public void NumberCommand(string number)
        {
            const int MaxLength = 16;

            switch (number)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (isNewNumber)
                    {
                        DisplayValue = number;
                        isNewNumber = false;
                    }
                    else
                    {
                        if (DisplayValue.Replace(".", "").Length > MaxLength)
                            return;

                        if (DisplayValue == "0" && number != ".")
                            DisplayValue = number;
                        else
                            DisplayValue += number;
                    }
                    break;

                default:
                    // 숫자가 아닌 경우 (혹시 잘못된 입력)
                    break;
            }
        }

        /// <summary>
        /// 현재 값 지우기
        /// </summary>
        public void DeleteCommand()
        {
            if (DisplayValue == "0" || DisplayValue.Length == 0 || DisplayValue == "0")
            {
                DisplayValue = "0";
                return;
            }

            if (DisplayValue.Length > 1)
            {
                DisplayValue = DisplayValue.Substring(0, DisplayValue.Length - 1);
            }
            else
            {
                DisplayValue = "0";
            }
        }

        /// <summary>
        /// 사치연산자 클릭 로직
        /// </summary>
        /// <param name="newValue"></param>
        public void ElementaryArithmeticCommand(string newOperation)
        {
            if (!double.TryParse(DisplayValue, out double operand))
            {
                return;
            }

            if (!string.IsNullOrEmpty(currentOperation) && !isNewNumber)
            {
                ExecutePendingCalculation(operand);
            }
            else
            {
                currentValue = operand;
            }
            CalculationHistory = $"{currentValue.ToString("G10")} {newOperation}";
            currentOperation = newOperation;
            isNewNumber = true;
        }


        /// <summary>
        /// 이전에 저장된 연산자와 현재 값을 사용하여 계산을 수행하고 결과를 DisplayValue에 반영합니다.
        /// </summary>
        /// <param name="operand2">두 번째 피연산자 (현재 DisplayValue의 값)</param>
        private void ExecutePendingCalculation(double operand2)
        {
            if (string.IsNullOrEmpty(currentOperation))
            {
                return;
            }

            try
            {
                // 1. 저장된 연산자를 기반으로 계산 수행
                switch (currentOperation)
                {
                    case "+": currentValue = Add(currentValue, operand2); break;
                    case "-": currentValue = Subtraction(currentValue, operand2); break;
                    case "*": currentValue = Multiplication(currentValue, operand2); break;
                    case "/":
                        if (operand2 == 0)
                            throw new DivideByZeroException("0으로 나눌 수 없습니다.");
                        currentValue = Divide(currentValue, operand2);
                        break;
                    default:
                        // 알 수 없는 연산자 무시
                        return;
                }

                DisplayValue = currentValue.ToString("G10");
            }
            catch (DivideByZeroException ex)
            {
                DisplayValue = "Error";
                currentOperation = string.Empty;
                isNewNumber = true;
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// 단일 연산자
        /// </summary>
        /// <param name="operation"></param>
        public void UnrayOperationCommand(string operation)
        {
            double operand = double.Parse(DisplayValue);
            double result = 0;

            if (!double.TryParse(DisplayValue, out operand))
            {
                return;
            }

            switch (operation)
            {
                case "x^2":
                    result = Math.Pow(operand, 2);
                    break;
                case "sqrt":
                    if (operand >= 0)
                    {
                        result = Math.Sqrt(operand);
                    }
                    else
                    {
                        MessageBox.Show("음수의 제곱근은 계산할 수 없습니다.", "Error");
                        return;
                    }
                    break;
                case "+/-":
                    if (operand == 0)
                    {
                        MessageBox.Show("0으로 나눌 수 없습니다.", "Error");
                        return;
                    }
                    break;
                case "1/x":
                    if (operand != 0)
                    {
                        result = 1.0 / operand;
                    }
                    else
                    {
                        MessageBox.Show("0으로 나눌 수 없습니다.", "Error");
                        return;
                    }
                    break;
                case "%":
                    if (operand == 0)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = operand / 100.0;
                    }
                    break;
                default:
                    return;
            }
            DisplayValue = result.ToString("G10");
            isNewNumber = true;
        }

        /// <summary>
        /// 결과 버튼
        /// </summary>
        public void ResultCommand()
        {
            double newValue = double.Parse(DisplayValue);
            if (!double.TryParse(DisplayValue, out newValue))
            {
                return;
            }

            if (string.IsNullOrEmpty(currentOperation))
            {
                return;
            }

            if (!string.IsNullOrEmpty(CalculationHistory))
            {
                CalculationHistory += $" {newValue.ToString("G10")} =";
            }

            try
            {
                switch (currentOperation)
                {
                    case "+": currentValue = Add(currentValue, newValue); break;
                    case "-": currentValue = Subtraction(currentValue, newValue); break;
                    case "*": currentValue = Multiplication(currentValue, newValue); break;
                    case "/":
                        if (newValue == 0) throw new DivideByZeroException("0으로 나눌 수 없습니다.");
                        currentValue = Divide(currentValue, newValue);
                        break;
                }

                DisplayValue = currentValue.ToString("G10");
            }
            catch (DivideByZeroException ex)
            {
                DisplayValue = "Error";
                CalculationHistory = string.Empty; // 오류 발생 시 식도 지움
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                currentOperation = string.Empty; // 다음 연산을 위해 연산자 상태 초기화
                isNewNumber = true;
            }
        }

        /// <summary>
        /// 소수점 
        /// </summary>
        public void DecimalCommand()
        {
            if (isNewNumber)
            {
                DisplayValue = "0.";
                isNewNumber = false;
                return;
            }

            if (!DisplayValue.Contains("."))
            {
                DisplayValue += ".";
            }
        }

        /// <summary>
        /// 기록 버튼
        /// </summary>
        public void HistoryButton_Click()
        {
            if (isSideBarOpen)
            {
                isSideBarOpen = false;
                CloseSideBar();
            }
            else
            {
                isSideBarOpen = true;
                OpenSideBar();
            }

        }

        /// <summary>
        /// MC 버튼 (메모리 값을 0으로 지움)
        /// </summary>
        public void MclearCommand()
        {
            memoryValue = 0;
        }

        /// <summary>
        /// MR 버튼 (메모리에 저장된 값 화면에 불러오는 역할)
        /// </summary>
        public void MreacallCommand()
        {
            DisplayValue = memoryValue.ToString("G10");
            isNewNumber = true;
        }

        /// <summary>
        /// M+ (메모리에 저장된 값을 현재 화면에 불러오는 역할)
        /// </summary>
        public void MplusCommand()
        {
            if (double.TryParse(DisplayValue, out double screenValue))
            {
                memoryValue += screenValue;
                //UpdateMemoryStatusUI();
            }
        }


        /// <summary>
        /// M- (현재 화면 값을 메모리 값에서 빼는 역할
        /// </summary>
        public void MminusCommand()
        {
            if (double.TryParse(DisplayValue, out double screenValue))
            {
                memoryValue -= screenValue;
                //UpdateMemoryStatusUI();
            }
        }

        /// <summary>
        /// MS (현재 화면 값을 메모리에 저장하고 기존 값을 덮어씀)
        /// </summary>
        public void MstoreCommand()
        {
            if (double.TryParse(DisplayValue, out double screenValue))
            {
                memoryValue = screenValue;
                //UpdateMemoryStatusUI();
            }
            isNewNumber = true;
        }

        #endregion


        #region private
        /// <summary>
        /// 기록버튼 열기
        /// </summary>
        private void OpenSideBar()
        {

        }

        /// <summary>
        /// 기록버튼 닫기
        /// </summary>
        private void CloseSideBar()
        {

        }

        #endregion
    }
}
