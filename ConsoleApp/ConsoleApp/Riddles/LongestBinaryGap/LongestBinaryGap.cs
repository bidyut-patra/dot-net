using System;

namespace ConsoleApp.Riddles
{
    public class LongestBinaryGap : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            int[] results = new int[input.Length];
            for(var i = 0; i < input.Length; i++)
            {
                var ip = input[i];
                results[i] = this.ComputeLoopBased(ip);
            }
            return results;
        }

        private int ComputeLoopBased(int input)
        {
            string binaryString = Convert.ToString(input, 2);
            Console.WriteLine("Binary Form Of {0}: {1}", input, binaryString);
            int longestBinGap = 0;
            int currentBinGap = 0;
            bool bitOneFound = false;
            bool binaryGapStarted = false;
            for (var i = 0; i < binaryString.Length; i++)
            {
                if(!bitOneFound && (binaryString[i] == '1'))
                {
                    bitOneFound = true;
                }
                else if (binaryGapStarted && (binaryString[i] == '1'))
                {
                    binaryGapStarted = false;
                    if(longestBinGap < currentBinGap)
                    {
                        longestBinGap = currentBinGap;
                    }
                    currentBinGap = 0;
                }
                else if (bitOneFound && (binaryString[i] == '0'))
                {
                    binaryGapStarted = true;
                    currentBinGap++;
                }
                else
                {
                    // Should not consider '0' until it is enclosed within two 1's
                }
            }
            return longestBinGap;
        }
    }
}
