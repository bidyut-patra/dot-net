using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddles
{
    class Program
    {
        static void Main(string[] args)
        {
            var guid = Guid.NewGuid();
            var guidStr = guid.ToString().ToUpper();

            var riddle1 = new SortIntsOnDiskFile()
            {
                Input = @"C:\WORK@SE\Test\unsorted_numbers.txt",
                Output = @"C:\WORK@SE\Test\sorted_numbers.txt"
            };
            riddle1.GenerateInput();
            riddle1.Process();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
