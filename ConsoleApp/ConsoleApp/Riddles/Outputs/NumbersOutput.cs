using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class NumbersOutput : ConsoleOutput<int[]>
    {
        public NumbersOutput()
        {

        }

        public override void Print(int[] data)
        {
            Console.Write("\r\n" + this.GetDisplayText());
            foreach(var n in data)
            {
                Console.Write("{0} ", n);
            }
        }

        protected override string GetDisplayText()
        {
            return "Result: ";
        }
    }
}
