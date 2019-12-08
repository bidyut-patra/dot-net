using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class CheckPermutationArray : IAlgorithm<int[], bool>
    {
        public bool Compute(int[] input)
        {
            Int64 iterativeSum = 0;
            Int64 directSum = (((Int64)input.Length) * (input.Length + 1)) / 2;
            for (var i = 0; i < input.Length / 2; i++)
            {
                iterativeSum += (input[i] + input[input.Length - i - 1]);
            }

            if ((input.Length % 2) == 1)
            {
                iterativeSum += input[input.Length / 2];
            }

            return (directSum == iterativeSum) ? true : false;
        }
    }
}
