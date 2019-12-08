using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class EqualBracketOutput : ConsoleOutput<bool>
    {
        public EqualBracketOutput()
        {

        }

        public override void Print(bool data)
        {
            Console.Write("\r\n" + this.GetDisplayText());
            Console.Write("{0} ", data ? "properly nested" : "incorrect nesting");
        }

        protected override string GetDisplayText()
        {
            return "Result: ";
        }
    }
}
