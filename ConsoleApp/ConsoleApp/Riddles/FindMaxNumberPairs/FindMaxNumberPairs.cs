using System;

namespace ConsoleApp.Riddles
{
    public class FindMaxNumberPairs : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            var result = 0;
            for(var i = 0; i < input.Length; i++)
            {
                if(input[i] != 0)
                {
                    for(var j = i + 1; j < input.Length; j++)
                    {
                        var pairFound = (input[i] + input[j] == 0);
                        var equalFound = (input[i] == input[j]);

                        if (pairFound || equalFound)
                        {
                            if(pairFound)
                            {
                                var positiveValue = input[i] > input[j] ? input[i] : input[j];
                                result = positiveValue > result ? positiveValue : result;
                            }

                            if(i != j)
                            {
                                this.Swap(ref input[i + 1], ref input[j]);
                                i = i + 1;
                            }
                        }
                    }

                    this.Display(input, i);
                }
            }
            return result;
        }

        private void Display(int[] arr, int index)
        {
            Console.WriteLine();
            foreach(var e in arr)
            {
                Console.Write("{0} ", e);
            }
            Console.WriteLine();
            Console.Write("{0}", index);
        }

        private void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
