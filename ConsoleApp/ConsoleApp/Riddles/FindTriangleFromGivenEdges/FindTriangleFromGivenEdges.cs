using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FindTriangleFromGivenEdges : IAlgorithm<int[], bool>
    {
        public bool Compute(int[] input)
        {
            // 0 <= P < Q < R < N
            // A[P] + A[Q] > A[R]
            // A[Q] + A[R] > A[P]
            // A[R] + A[P] > A[Q]

            return true;
        }
    }
}
