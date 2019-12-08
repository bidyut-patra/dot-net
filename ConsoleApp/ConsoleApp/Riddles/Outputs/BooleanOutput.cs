using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class BooleanOutput : ConsoleOutput<bool>
    {
        public BooleanOutput()
        {

        }

        public override void Print(bool data)
        {
            Console.Write("\r\n" + this.GetDisplayText());
            Console.Write("{0} ", data ? "not unique" : "unique");
        }

        protected override string GetDisplayText()
        {
            return "Result: ";
        }
    }
}
