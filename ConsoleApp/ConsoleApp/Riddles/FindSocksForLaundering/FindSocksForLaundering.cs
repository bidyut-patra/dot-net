using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class FindSocksForLaundering : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            var k = input[0]; // the number of dirty socks taken for washing
            var noOfCleanSocks = input[1]; // Number of clean socks in the drawer1
            var noOfDirtySocks = input[2]; // Number of dirty socks in the drawer2

            var cleanSocksStartIndex = 3;
            var cleanSocksArrayLength = cleanSocksStartIndex + noOfCleanSocks;
            var dirtySocksStartIndex = 3 + noOfCleanSocks;
            var dirtySocksArrayLength = dirtySocksStartIndex + noOfDirtySocks;

            // K = 2, C = [1, 2, 1, 1] and D = [1, 4, 3, 2, 4] --> 3

            int maxPairsCount = 0;
            bool atLeastOneDirtySocksPicked = false;

            for(var i = cleanSocksStartIndex; (i < cleanSocksArrayLength) && (k > 0); i++)
            {
                var colorX = input[i];
                if(colorX != 0) // '0' means color code is already counted
                {
                    var cleanSocksCountOfColorX = this.FindSocksCount(colorX, cleanSocksStartIndex, cleanSocksArrayLength, input);

                    var pairCountOfColorX = cleanSocksCountOfColorX / 2;
                    var hasUnmatchedPairOfColorX = (cleanSocksCountOfColorX % 2) == 1;
                    maxPairsCount += pairCountOfColorX;

                    if (hasUnmatchedPairOfColorX)
                    {
                        var dirtSocksCountOfColorX = this.FindSocksCount(colorX, dirtySocksStartIndex, dirtySocksArrayLength, input);

                        if (dirtSocksCountOfColorX > 0)
                        {
                            maxPairsCount += 1;
                            k--;
                            atLeastOneDirtySocksPicked = true;
                        }
                    }
                }
            }

            if(k > 0) // If specified number of dirty socks not yet picked
            {
                if((k % 2) == 1) // If odd number of dirty socks remaining to be picked then 
                {
                    if(atLeastOneDirtySocksPicked)
                    {
                        maxPairsCount -= 1;
                        k++; // Now dirty socks remaining to be picked is even number
                    }
                }

                var dirtySocksPairToBePicked = k / 2;
                if(dirtySocksPairToBePicked > 0)
                {
                    maxPairsCount += this.FindDirtySocksPairCount(dirtySocksPairToBePicked, dirtySocksStartIndex, dirtySocksArrayLength, input);
                }
            }

            return maxPairsCount;
        }

        private int FindDirtySocksPairCount(int dirtySocksPairToBePicked, int socksStartIndex, int totalSocksCount, int[] a)
        {
            var dirtySocksPairCount = 0;
            for (var i = socksStartIndex; (i < totalSocksCount) && (dirtySocksPairCount < dirtySocksPairToBePicked); i++)
            {
                var dirtySocksCountOfColorY = this.FindSocksCount(a[i], socksStartIndex, totalSocksCount, a);
                var dirtySocksPairOfColorY = dirtySocksCountOfColorY / 2;
                dirtySocksPairCount += dirtySocksPairOfColorY;
            }
            return dirtySocksPairCount;
        }

        private int FindSocksCount(int colorCode, int socksStartIndex, int totalSocksCount, int[] a)
        {
            int socksCount = 0;
            for(var i = socksStartIndex; i < totalSocksCount; i++)
            {
                if(a[i] == colorCode)
                {
                    socksCount += 1;
                    a[i] = 0;
                }
            }
            return socksCount;
        }
    }
}
