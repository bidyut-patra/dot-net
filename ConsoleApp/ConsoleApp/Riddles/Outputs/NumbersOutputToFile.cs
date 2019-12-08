using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    class NumbersOutputToFile : OutputToFile<int[]>
    {
        public NumbersOutputToFile()
        {

        }

        public override void Print(int[] data)
        {
            foreach(var n in data)
            {
                Console.Write("{0} ", n);
            }
        }
    }
}
