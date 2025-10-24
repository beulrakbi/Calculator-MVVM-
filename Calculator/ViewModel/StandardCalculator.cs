using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    class CalculatorModelcs
    {
        #region variable
        #region public
        /// <summary>
        /// 처음 Display 숫자를 0으로 지정하는 변수
        /// </summary>
        public string DisplayValue { get; private set; } = "0";

        /// <summary>
        /// 현재 값 저장 변수
        /// </summary>
        public double currentVlaue = 0.0;

        /// <summary>
        /// 현재 연산자 저장 변수
        /// </summary>
        public string currentOperation = string.Empty;
        #endregion

        #region private
        /// <summary>
        /// 새 숫자를 입력 중인지 나타내는 플래그
        /// </summary>
        private bool isNewNumber = true;
        #endregion
        #endregion


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
        #endregion


        #region public
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
        #endregion

        #region public
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
        #endregion

        #region public
        /// <summary>
        /// 나눗셈
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double Divide(double x, double y)
        {

            if (y==0)
            {
                throw new ArgumentException("0으로 나눌 수 없습니다.");
            }
            return x / y;
        }
        #endregion

        #region public
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
        #endregion

        #region public
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
        #endregion

        #region public
        /// <summary>
        /// 부호 변경 (+/-)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double ToggleSign(double value)
        {
            return -value;
        }
        #endregion

        #region public
        /// <summary>
        /// 제곱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Square(double value)
        {
            return value * value;
        }
        #endregion

        #region public
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
        #endregion

        #region public
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
        #endregion

        #region public
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
        #endregion

        #region public 
        /// <summary>
        /// Clear 버튼을 눌렀을 때 동작하는 메서드
        /// </summary>
        public void Clear()
        {
            currentVlaue = 0.0;
            currentOperation = string.Empty;
            isNewNumber = true;
            DisplayValue = "0";
        }
        #endregion


    }
}
