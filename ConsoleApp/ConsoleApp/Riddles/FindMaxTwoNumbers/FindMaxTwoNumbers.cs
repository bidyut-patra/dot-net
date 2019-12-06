using System;

namespace ConsoleApp.Riddles
{
    public class FindMaxTwoNumbers : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            int? max1 = null;
            int? max2 = null;

            for(var i = 0; i < input.Length; i++)
            {
                if((max1 == null) && (max2 == null))
                {
                    max1 = input[i];
                    max2 = input[input.Length - i - 1];
                }
                else
                {
                    if(max1 < input[i])
                    {
                        if(input[i] != max2)
                        {
                            max1 = input[i];
                        }
                    }

                    if((max2 < input[input.Length - i - 1]))
                    {
                        if (max1 != input[input.Length - i - 1])
                        {
                            max2 = input[input.Length - i - 1];
                        }
                    }
                }
            }

            int[] maxNumbers = new int[2];
            maxNumbers[0] = (int)max1;
            maxNumbers[1] = (int)max2;

            return maxNumbers;
        }
    }
}
