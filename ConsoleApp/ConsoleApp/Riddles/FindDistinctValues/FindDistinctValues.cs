using System;

namespace ConsoleApp.Riddles
{
    public class FindDistinctValues : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            var uniqueCount = 0;
            var uniqueIndices = new char[input.Length];
            for(var i = 0; i < input.Length; i++)
            {
                if (uniqueIndices[i] == '\0')
                {
                    uniqueIndices[i] = 'u';
                    uniqueCount++;
                }

                for (var j = i + 1; j < input.Length; j++)
                {
                    if((uniqueIndices[j] == '\0') && (input[i] == input[j]))
                    {
                        uniqueIndices[j] = 'd';
                    }
                }
            }

            var y = 0;
            var uniqueNumbers = new int[uniqueCount];
            for(var x = 0; x < uniqueIndices.Length; x++)
            {
                if(uniqueIndices[x] == 'u')
                {
                    uniqueNumbers[y++] = input[x];
                }
            }

            return uniqueNumbers;
        }
    }
}
