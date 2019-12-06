using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class PrintNumbersWithoutLoop : IAlgorithm<int, int[]>
    {
        public int[] Compute(int input)
        {
            return GenerateNumbers(input);
        }

        private int[] GenerateNumbers(int n, int index = 0, int[] numbers = null)
        {
            if (numbers == null)
            {
                numbers = new int[n];
            }
            if(index < n)
            {
                numbers[index] = index + 1;
                GenerateNumbers(n, ++index, numbers);
            }
            return numbers;
        }
    }
}
