using System;

namespace ConsoleApp.Riddles
{
    /*
     We draw N discs on a plane. The discs are numbered from 0 to N − 1. An array A of N non-negative integers, 
     specifying the radiuses of the discs, is given. The J-th disc is drawn with its center at (J, 0) and radius A[J].

     We say that the J-th disc and K-th disc intersect if J ≠ K and the J-th and K-th discs have at least one common 
     point (assuming that the discs contain their borders).

     The figure below shows discs drawn for N = 6 and A as follows:

      A[0] = 1
      A[1] = 5
      A[2] = 2
      A[3] = 1
      A[4] = 4
      A[5] = 0
    */
    public class FindNumberInterections : IAlgorithm<int[], int[]>
    {
        public int[] Compute(int[] input)
        {
            return new int[0];
        }
    }
}
