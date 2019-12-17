using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FindMaxMatchesInTennis : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            var noOfPlayers = input[0]; // 5
            var noOfCourts = input[1]; // 3

            var maxNoOfMatchesAtTheSameTime = noOfPlayers / 2;

            return maxNoOfMatchesAtTheSameTime < noOfCourts ? maxNoOfMatchesAtTheSameTime : noOfCourts;
        }
    }
}
