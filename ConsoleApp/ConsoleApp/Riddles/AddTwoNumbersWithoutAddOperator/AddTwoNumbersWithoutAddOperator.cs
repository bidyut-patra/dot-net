using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class AddTwoNumbersWithoutAddOperator : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            var max = Math.Max(input[0], input[1]);
            var min = Math.Min(input[0], input[1]);
            var diff = max - min;
            var result = max * 2 - diff;
            return result;
        }
    }
}
