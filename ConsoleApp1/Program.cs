using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            // a list of integers [3,2,1], [-3,2,1], [3,2]
            // a list of operands [ '+', '-']

            //Calculator.Parse("-3+2-1");
            //Console.WriteLine($"IsDigit(0) = {Calculator.IsDigit('0')}");
            //Console.WriteLine($"IsDigit(9) = {Calculator.IsDigit('9')}");
            //Console.WriteLine($"IsDigit(a) = {Calculator.IsDigit('a')}");
            //Console.WriteLine($"IsDigit(-) = {Calculator.IsDigit('-')}");


            Tests.ParsingOutput_Test();
            Tests.Parse_Test();

            Tests.Calculate_Test();



            Console.ReadKey();
        }
    }
}
