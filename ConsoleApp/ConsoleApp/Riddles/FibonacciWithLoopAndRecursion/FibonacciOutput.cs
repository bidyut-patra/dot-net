using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class FibonacciOutput : ConsoleOutput<int[]>
    {
        private bool _recursive;

        public FibonacciOutput(bool recursive)
        {
            this._recursive = recursive;
        }

        public override void Display(int[] data)
        {
            Console.WriteLine("\r\n" + this.GetDisplayText());
            foreach (var fn in data)
            {
                Console.Write("{0} ", fn.ToString());
            }
        }

        protected override string GetDisplayText()
        {
            if(this._recursive)
            {
                return "Fibonacci recursive";
            }
            else
            {
                return "Fibonacci loop";
            }
        }
    }
}
