using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FloatNumbersInput : KeyboardInput<double[]>
    {
        public override double[] GetData()
        {
            Console.WriteLine();
            Console.Write(this.GetDisplayText());
            var val = Console.ReadLine();
            var numbers = val.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var intNumbers = numbers.Select(n => Convert.ToDouble(n));
            return intNumbers.ToArray();
        }

        protected override string GetDisplayText()
        {
            return "Enter numbers: ";
        }
    }
}
