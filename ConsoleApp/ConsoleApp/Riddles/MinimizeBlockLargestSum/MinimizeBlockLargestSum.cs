using System;

namespace ConsoleApp.Riddles
{
    /*
        You are given integers K, M and a non-empty array A consisting of N integers. Every element of the array is not greater than M.

        You should divide this array into K blocks of consecutive elements. The size of the block is any integer between 0 and N. 
        Every element of the array should belong to some block.

        The sum of the block from X to Y equals A[X] + A[X + 1] + ... + A[Y]. The sum of empty block equals 0.

        The large sum is the maximal sum of any block.

        For example, you are given integers K = 3, M = 5 and array A such that:

          A[0] = 2
          A[1] = 1
          A[2] = 5
          A[3] = 1
          A[4] = 2
          A[5] = 2
          A[6] = 2
        The array can be divided, for example, into the following blocks:

        [2, 1, 5, 1, 2, 2, 2], [], [] with a large sum of 15;
        [2], [1, 5, 1, 2], [2, 2] with a large sum of 9;
        [2, 1, 5], [], [1, 2, 2, 2] with a large sum of 8;
        [2, 1], [5, 1], [2, 2, 2] with a large sum of 6.
        The goal is to minimize the large sum. In the above example, 6 is the minimal large sum.

        Write an efficient algorithm for the following assumptions:

        N and K are integers within the range [1..100,000];
        M is an integer within the range [0..10,000];
        each element of array A is an integer within the range [0..M].
     */
    public class MinimizeBlockLargestSum : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            var k = input[0];
            var m = input[1];

            //return largestSum(k, m, input);                 
            return solution(k, m, input);
        }

        public static int largestSum(int k, int m, int[] input)
        {
            var largestSum = 0;
            var j = 2; // Here starts actual data of array
            var blockSum = new int[k];
            var blockLength = (input.Length - 2) / k;
            var blockIndex = 0;
            var blockSumIndex = 0;

            for (; j < input.Length; j++)
            {
                blockSum[blockSumIndex] += input[j];

                if (largestSum < blockSum[blockSumIndex])
                {
                    largestSum = blockSum[blockSumIndex];
                }

                blockIndex++;

                if (blockSumIndex == k - 1)
                {
                    // Add remaining numbers
                }
                else if (blockIndex >= blockLength)
                {
                    blockIndex = 0;
                    blockSumIndex++;
                }
                else
                {
                    // Add next number
                }

                if (j == input.Length - 1)
                {

                }
            }
            return largestSum;
        }

        public static int solution(int K, int M, int[] A)
        {
            int min = 0;
            int max = 0;
            for (int i = 2; i < A.Length; i++)
            {
                //get the sum as max, and the largest number as min
                max += A[i];
                min = Math.Max(min, A[i]);
            }
            int result = max;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (divisionSolvable(mid, K - 1, A))
                {
                    max = mid - 1;
                    result = mid;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return result;
        }

        private static bool divisionSolvable(int mid, int k, int[] a)
        {
            int sum = 0;
            for (int i = 2; i < a.Length; i++)
            {
                sum += a[i];
                if (sum > mid)
                {
                    sum = a[i];
                    k--;
                }
                if (k < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
