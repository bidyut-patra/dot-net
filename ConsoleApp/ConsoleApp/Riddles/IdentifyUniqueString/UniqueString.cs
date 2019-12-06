using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class UniqueString : IAlgorithm<string, bool>
    {
        public bool Compute(string input)
        {
            bool uniqueViolated = false;
            for (var i = 0; (i < input.Length) && !uniqueViolated; i++)
            {
                char currentChar = input[i];
                if (currentChar != ' ')
                {
                    var k = 1;
                    var l = i + 1;
                    for (var j = l; (j < l + (input.Length - l) / 2) && !uniqueViolated; j++)
                    {
                        uniqueViolated = (input[j] == currentChar) || (input[input.Length - k++] == currentChar);
                    }
                }
            }
            return uniqueViolated;
        }
    }
}
