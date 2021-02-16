using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    static class Tests
    {
        #region Calculate tests
        public static void Calculate_Test()
        {
            Console.WriteLine("Calculate valid test...");
            Calculate_Single_Valid_Test("3+2", 5);
            Calculate_Single_Valid_Test("30+20", 50);
            Calculate_Single_Valid_Test("30-20", 10);
            Calculate_Single_Valid_Test("-30+20", -10);
            Calculate_Single_Valid_Test("-3-20", -23);

            Console.WriteLine();
            Console.WriteLine("Calculate invalid test...");
            Calculate_Single_InValid_Test("3++2");
            Calculate_Single_InValid_Test("3*2");
            Calculate_Single_InValid_Test("3+a-2");
            Calculate_Single_InValid_Test(" 3+2");// space test

        }

        static void Calculate_Single_Valid_Test(string expression, int expected)
        {
            var result = Calculator.Calculate(expression);
            Console.WriteLine($"{expression} = {result}, {OutputMessage(result == expected)}");
        }
        static void Calculate_Single_InValid_Test(string expression)
        {
            try
            {
                var result = Calculator.Calculate(expression);
                Console.WriteLine($"{expression} = failed");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{expression} is invalid = passed");
            }
        }

        #endregion

        #region Parse tests
        public static void Parse_Test()
        {

            // valid tests
            Console.WriteLine("Parse valid tests...");

            Parse_SingleTest("3+2", new int[] { 3, 2 }, new char[] { '+' });
            Parse_SingleTest("30+20", new int[] { 30, 20 }, new char[] { '+' });
            Parse_SingleTest("-30+20", new int[] { -30, 20 }, new char[] { '+' });

            // invalid tests
            Console.WriteLine("Parse invalid tests...");

            Parse_SingleTest("3", new int[] {3}, new char[0], true);
            Parse_SingleTest("", new int[0], new char[0], true);
            Parse_SingleTest(null, new int[0], new char[0], true);
            Parse_SingleTest("3+2-", new int[] { 3, 2 }, new char[] { '+' }, true);
            Parse_SingleTest("+", new int[0], new char[] { '+' }, true);
            Parse_SingleTest("30+20-", new int[] { 30, 20 }, new char[] { '+' }, true);
            Parse_SingleTest("-30-20-", new int[] { -30, 20 }, new char[] { '-' }, true);
            Parse_SingleTest("3++2", new int[] { 3, 2 }, new char[] { '+' }, true);


        }

        static  ParsingOutput CreateParsingOutput(int[] integersArray, char[] operands)
        {
            var result = new ParsingOutput();
            foreach(var i in integersArray)
            {
                result.IntegersArray.Add(i);
            }
            foreach (var o in operands)
            {
                result.Operands.Add(o);
            }
            return result;
        }

        static void Parse_SingleTest(string expr, 
                                    int[] integersArray, 
                                    char[] operands,
                                    bool expectedFailure=false)
        {
            var expected = CreateParsingOutput(integersArray, operands);
            try
            {
                var result = Calculator.Parse(expr);
                if (expectedFailure)
                {
                    Console.WriteLine($"{OutputMessage(!result.Equals(expected))}");
                }
                else
                {
                    Console.WriteLine($"{OutputMessage(result.Equals(expected))}");
                }
            }
            catch(Exception e)
            {
                if (expectedFailure)
                {
                    Console.WriteLine("passed");
                }
                else
                {
                    Console.WriteLine(e);
                }
            }


        }
        #endregion

        #region ParsingOutput tests
        public static void ParsingOutput_Test()
        {
            var p1 = new ParsingOutput();
            p1.IntegersArray.Add(1);
            p1.IntegersArray.Add(2);
            p1.IntegersArray.Add(3);
            p1.Operands.Add('+');

            var p2 = new ParsingOutput();
            p2.IntegersArray.Add(1);
            p2.IntegersArray.Add(2);
            p2.IntegersArray.Add(3);
            p2.Operands.Add('+');

            var p3 = new ParsingOutput();
            p3.IntegersArray.Add(1);
            p3.IntegersArray.Add(2);
            p3.IntegersArray.Add(3);
            p3.Operands.Add('-');

            var p4 = new ParsingOutput();
            p4.IntegersArray.Add(1);
            p4.IntegersArray.Add(2);

            p3.Operands.Add('-');

            Console.WriteLine("testing ParsingOutput equality...");

            Console.WriteLine(OutputMessage(p1.Equals(p2)));

            Console.WriteLine(OutputMessage(!p1.Equals(p3)));

            Console.WriteLine(OutputMessage(!p2.Equals(p4)));

        }

        #endregion

        static string OutputMessage(bool passed)
        {
            return (passed ? "passed" : "failed");
        }
    }
}
