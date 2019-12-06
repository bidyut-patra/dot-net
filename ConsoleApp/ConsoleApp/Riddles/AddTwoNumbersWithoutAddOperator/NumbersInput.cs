using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class NumbersInput : KeyboardInput<int[]>
    {
        public override int[] GetData()
        {
            Console.WriteLine();
            Console.Write(this.GetDisplayText());
            var val = Console.ReadLine();
            var numbers = val.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var intNumbers = numbers.Select(n => Convert.ToInt32(n));
            return intNumbers.ToArray();
        }

        protected override string GetDisplayText()
        {
            return "Enter numbers: ";
        }
    }
}
