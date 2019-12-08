using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FindNumbersDivisibleByGivenNumber : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            Int64 k = input[0];
            Int64 a = input[1];
            Int64 b = input[2];

            Int64 count = 0;
            Int64 start = k >= a ? k : k * 2;

            for(var i = start; i <= b; i += k)
            {
                count++;
            }

            return (int)count;
        }
    }
}
