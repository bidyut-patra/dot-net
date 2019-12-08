using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class NumberOutput : ConsoleOutput<int>
    {
        public NumberOutput()
        {

        }

        public override void Print(int data)
        {
            Console.Write("\r\n" + this.GetDisplayText());
            Console.Write("{0} ", data);
        }

        protected override string GetDisplayText()
        {
            return "Result: ";
        }
    }
}
