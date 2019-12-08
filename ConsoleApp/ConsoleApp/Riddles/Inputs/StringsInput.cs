using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class StringsInput : KeyboardInput<string[]>
    {
        public override string[] GetData()
        {
            Console.WriteLine();
            Console.Write(this.GetDisplayText());
            var words = Console.ReadLine();
            return words.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected override string GetDisplayText()
        {
            return "Enter words: ";
        }
    }
}
