using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class NumbersInput : KeyboardInput<int[]>
    {
        private int[] numbers;

        public NumbersInput()
        {
            this.numbers = new int[0];
        }

        public NumbersInput(int[] numbers)
        {
            this.numbers = numbers;
        }

        public override int[] GetData()
        {
            if(this.numbers.Length == 0)
            {
                Console.WriteLine();
                Console.Write(this.GetDisplayText());
                var val = Console.ReadLine();
                var numbers = val.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var intNumbers = numbers.Select(n => Convert.ToInt32(n));
                return intNumbers.ToArray();
            }
            else
            {
                return this.numbers;
            }
        }

        protected override string GetDisplayText()
        {
            return "Enter numbers: ";
        }
    }
}
