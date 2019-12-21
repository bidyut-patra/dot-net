using System;

namespace ConsoleApp.Riddles
{
    public class GenerateNumberPairSequence : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            int length = input[0];
            int offset = input[1];
            int[] result = new int[length];
            int j = 1;
            for(var i = 1; i <= length; i++)
            {
                if(i > length - offset)
                {
                    result[i - 1] = (j - length - offset);
                    j++;
                }
                else
                {
                    result[i - 1] = i;
                    j++;
                }
            }
            return result;
        }
    }
}
