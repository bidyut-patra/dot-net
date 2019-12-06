using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class SmallestLargestNumbers : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            int[] numbers = new int[2];
            int? max = null;
            int? min = null;
            for(var i = 0; i < input.Length; i++)
            {
                if((max == null) && (min == null))
                {
                    max = input[i];
                    min = input[i];
                }
                else
                {
                    if(min > input[i])
                    {
                        min = input[i];
                    }

                    if(max < input[i])
                    {
                        max = input[i];
                    }
                }
            }
            numbers[0] = (int)max;
            numbers[1] = (int)min;
            return numbers;
        }
    }
}
