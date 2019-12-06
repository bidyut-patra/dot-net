using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class StringInput : KeyboardInput<string>
    {
        public override string GetData()
        {
            Console.WriteLine();
            Console.Write(this.GetDisplayText());
            return Console.ReadLine();
        }

        protected override string GetDisplayText()
        {
            return "Enter text: ";
        }
    }
}
