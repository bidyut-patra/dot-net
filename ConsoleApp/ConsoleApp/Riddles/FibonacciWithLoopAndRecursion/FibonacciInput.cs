using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FibonacciInput : KeyboardInput<int>
    {
        public override int GetData()
        {
            Console.WriteLine();
            Console.Write(this.GetDisplayText());
            var val = Console.ReadLine();
            return Convert.ToInt32(val);
        }

        protected override string GetDisplayText()
        {
            return "Enter fibonacci count: ";
        }
    }
}
