using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ParsingOutput : IEquatable<ParsingOutput>
    {
        #region nested classes
        public ParsingOutput()
        {
            this.IntegersArray = new List<int>();
            this.Operands = new List<char>();
        }
        public List<int> IntegersArray { get; set; }

        public List<char> Operands { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ParsingOutput;
            return this.Equals(other);
        }

        public bool Equals(ParsingOutput other)
        {
            if (this.IntegersArray.Count() != other.IntegersArray.Count())
                return false;

            if (this.Operands.Count() != other.Operands.Count())
                return false;

            if (this.IntegersArray.Except(other.IntegersArray).Any())
                return false;

            if (this.Operands.Except(other.Operands).Any())
                return false;

            return true;               
        }
    }

    #endregion

    public class Calculator
    {
        // a list of integers [3,2,1]
        // a list of operands [ '+', '-']
        //
        // number of integers is one more than the number of operands
        // foreach integers
        // 


        /// <summary>
        /// Returns the result of an arithmetic expression
        /// </summary>
        /// <example>
        /// Calculate("3+2-1"); // returns 4
        /// 
        /// operand = +, -
        /// all positive/negative integers
        /// 
        /// </example>
        static public int Calculate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException("null input.");

            // 3, +, 2, -, 1
            // a3, +, 2z, -, 1

            var parsedData = Parse(expression);

            //var ints = new int[] { -3, 2, 1 };
            //var operands = new char[]{'+', '-'};

            var ints = parsedData.IntegersArray;
            var operands = parsedData.Operands;


            var isValidNumber = (ints.Count() == operands.Count() + 1);
            if (!isValidNumber)
                throw new Exception("invalid string");

            int result = ints[0]; // 3

            for (var i = 0; i < operands.Count(); i++)
            {
                var currentDigit = ints[i+1]; // 2, 1

                var o = operands[i];// +, -
                if( o == '+')
                {
                    result += currentDigit; //3+2 = 5
                }
                else if( o == '-') // 5-1 = 4
                {
                    result -= currentDigit;
                }
                else
                {
                    throw new Exception("invalid operand");
                }
            }

            return result;

        }

        // 3 + -2
        // a list of integers [3,2,1], [-3,2,1], [3,2]
        // a list of operands [ '+', '-']
        internal static ParsingOutput Parse(string input)
        {
            var expression = input;

            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException("Input cannot be empty string.");

            var result = new ParsingOutput();

            bool isFirstNegative = false;
            var tempDigits = new StringBuilder();

            if (expression[0] == '+')
            {
                // ignore
            }
            else if (expression[0] == '-')
            {
                // -1 * first
                isFirstNegative = true;
                tempDigits.Append(expression[0]);
            }

            // "3+2-1"

            try
            {
                var wasDigit = false; // flag whether the last character was a digit

                for (var i = (isFirstNegative ? 1 : 0); i < expression.Length; i++)
                {
                    var c = expression[i];
                    // check if i is a digit or '+' or '-'
                    if (IsDigit(c))
                    {
                        // if is digit, store the digit
                        tempDigits.Append(c);
                        wasDigit = true;
                    }
                    else if( c == '+' || c == '-')
                    {
                        // if is + or -...

                        // process the number
                        if (wasDigit)
                        {
                            var number = TryParseToInt(tempDigits);
                            result.IntegersArray.Add(number);
                            tempDigits.Clear();

                            //var numberString = tempDigits.ToString();
                            //int number = 0;

                            //if( int.TryParse(numberString, out number))
                            //{
                            //    result.IntegersArray.Add(number);
                            //    tempDigits.Clear();
                            //}
                            //else
                            //{
                            //    throw new ArgumentException($"Failed to convert to an integer: {numberString}");
                            //}
                        }
                        else // do not allow multiple operands together (such as "++" or "--" or "+-")
                        {
                            throw new ArgumentException("Multiple operands together are not allowed");
                        }

                        // save operand
                        result.Operands.Add(c);


                        wasDigit = false;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid character found: {c}");
                    }

                }


                if (wasDigit) // process the last integer
                {
                    var number = TryParseToInt(tempDigits);
                    result.IntegersArray.Add(number);
                    tempDigits.Clear();
                }

            }
            catch (OverflowException)
            {
                throw;
            }

            if (result.IntegersArray.Count() == 1)
                throw new ArgumentException("Must have at least 2 integers");
            if (result.IntegersArray.Count() != result.Operands.Count() + 1)
                throw new ArgumentException("The integer count must be one more than the operand count");

            return result;
        }

        static int TryParseToInt(StringBuilder tempDigits)
        {
            var numberString = tempDigits.ToString();
            int number = 0;

            if (int.TryParse(numberString, out number))
            {
            }
            else
            {
                throw new ArgumentException($"Failed to convert to an integer: {numberString}");
            }
            return number;

        }
        internal static bool IsDigit(char i)
        {
            var temp = i - '0';
            return (temp >= 0 && temp <= 9);
        }

    }
}
