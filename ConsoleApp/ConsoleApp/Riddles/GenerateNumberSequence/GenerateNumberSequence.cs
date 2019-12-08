using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class GenerateNumberSequence : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            int maxNumber = input[0];
            int excludedNumber = input[1];
            int[] result = new int[input[0] - 1];
            for(var i = 1; i <= result.Length; i++)
            {
                var number = this.Generate(ref maxNumber, excludedNumber);
                result[i - 1] = number;
            }
            return result;
        }

        /// <summary>
        /// Generates a number within max number
        /// </summary>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
        private int Generate(ref int maxNumber, int excludedNumber)
        {
            if(maxNumber == excludedNumber)
            {
                maxNumber -= 1;
            }
            return maxNumber--;
        }
    }
}
