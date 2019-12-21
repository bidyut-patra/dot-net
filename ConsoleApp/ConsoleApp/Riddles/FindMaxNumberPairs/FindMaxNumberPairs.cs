using System;
using System.Collections.Generic;

namespace ConsoleApp.Riddles
{
    public class FindMaxNumberPairs : IAlgorithm<int[], int>
    {
        public int Compute(int[] input)
        {
            return this.ComputeWithCustomDictionary(input);
            //return this.ComputeAsNSquare(input);
        }

        private int ComputeWithDictionary(int[] input)
        {
            var pair = new Dictionary<int, bool>();
            for(var i = 0; i < input.Length; i++)
            {
                if(input[i] > 0)
                {
                    pair.Add(input[i], false);
                }
                if(input[i] < 0)
                {
                    if(pair.ContainsKey(-input[i]))
                    {
                        pair[-input[i]] = true;
                    }
                }
            }
            var result = 0;
            foreach(var number in pair.Keys)
            {
                if(pair[number] && result < number)
                {
                    result = number;
                }
            } 
            return result;
        }

        private int ComputeWithCustomDictionary(int[] input)
        {
            var pair = new CustomDictionary<int, int>(input.Length);
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] > 0)
                {
                    if(pair[input[i]] < 3)
                    {
                        pair[input[i]] += 1;
                    }
                }
                if (input[i] < 0)
                {
                    if (pair[-input[i]] < 2)
                    {
                        pair[-input[i]] += 2;
                    }
                }
            }
            var result = 0;
            foreach (var number in pair.Keys)
            {
                if ((pair[number] == 3)  && (result < number))
                {
                    result = number;
                }
            }
            return result;
        }

        private int ComputeAsN(int[] input)
        {
            int[] positiveNumbers = new int[input.Length];
            int[] negativeNumbers = new int[input.Length];

            int j = 0, k = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if(input[i] > 0)
                {
                    if(input[i] > positiveNumbers[j])
                    {
                        positiveNumbers[j++] = input[i];
                    }
                    else
                    {
                        positiveNumbers[j++] = positiveNumbers[j - 1];
                        positiveNumbers[j - 1] = input[i];
                    }
                }

                if(input[i] < 0)
                {
                    if(input[i] < negativeNumbers[k])
                    {
                        negativeNumbers[k++] = input[i];
                    }
                    else
                    {
                        negativeNumbers[k++] = negativeNumbers[k - 1];
                        negativeNumbers[k - 1] = input[i]; 
                    }
                }
            }

            var result = 0;
            if ((j == 0) || (k == 0))
            {
                result = 0;
            }
            else
            {
                var sourceNumbers = j > k ? negativeNumbers : positiveNumbers;
                var searchList = j > k ? positiveNumbers : negativeNumbers;

                for(var m = 0; m < sourceNumbers.Length; m++)
                {
                    for(var n = 0; n < searchList.Length; n++)
                    {
                        var n1 = sourceNumbers[m];
                        var n2 = searchList[n];

                        if(n1 + n2 == 0)
                        {
                            var positiveNumber = n1 > 0 ? n1 : n2;
                            result = result < positiveNumber ? positiveNumber : result;
                            break;
                        }
                    }
                }
            }
            
            return result;
        }

        private int ComputeAsNSquare(int[] input)
        {
            var result = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] != 0)
                {
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        var pairFoundInForwardSearch = (input[i] + input[j] == 0);
                        var equalFoundInForwardSearch = (input[i] == input[j]);

                        if (pairFoundInForwardSearch || equalFoundInForwardSearch)
                        {
                            if (pairFoundInForwardSearch)
                            {
                                var positiveValue = input[i] > input[j] ? input[i] : input[j];
                                result = positiveValue > result ? positiveValue : result;
                            }

                            if (i != j)
                            {
                                this.Swap(ref input[i + 1], ref input[j]);
                                i = i + 1;
                            }
                        }
                    }

                    //this.Display(input, i);
                }
            }

            Console.WriteLine();

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
