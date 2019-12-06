using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class FloatNumbersOutput : ConsoleOutput<double[]>
    {
        public FloatNumbersOutput()
        {

        }

        public override void Display(double[] data)
        {
            Console.Write("\r\n" + this.GetDisplayText());
            foreach(var n in data)
            {
                Console.Write("{0} ", n);
            }
        }

        protected override string GetDisplayText()
        {
            return "Percentages: ";
        }
    }
}
