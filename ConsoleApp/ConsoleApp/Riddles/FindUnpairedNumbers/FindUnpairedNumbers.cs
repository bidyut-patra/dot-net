using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FindUnpairedNumbers : IAlgorithm<int[], string>
    {
        public string Compute(int[] input)
        {
            var indicesSum = ((input.Length - 1) * input.Length) / 2;
            var matchedCount = 0;
            var matchedIndicesSum = 0;
            var unpairedIndex = -1;
            var matches = new bool[input.Length];
            var result = string.Empty;
            for(var i = 0; i < input.Length - 1; i++)
            {
                for(var j = i + 1; j < input.Length; j++)
                {
                    if (input[i] == input[j])
                    {
                        matchedIndicesSum += (i + j);
                        matchedCount += 2;
                    }
                }
                
                if (matchedCount == input.Length - 1)
                {
                    unpairedIndex = indicesSum - matchedIndicesSum;
                    break;
                }
            }

            result = unpairedIndex > -1 ? input[unpairedIndex].ToString() : "not found";

            return result;
        }
    }
}
