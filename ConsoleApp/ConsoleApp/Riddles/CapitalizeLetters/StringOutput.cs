using System;
using System.Linq;

namespace ConsoleApp.Riddles
{
    class StringOutput : ConsoleOutput<string>
    {
        public StringOutput()
        {
        }

        public override void Display(string data)
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
