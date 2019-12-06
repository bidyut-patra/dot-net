using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class ReversePolishNotation : IAlgorithm<string, string>
    {
        public string Compute(string input)
        {
            var result = this.ComputePolishNotation(input);
            return result == null ? "Input string not in correct form" : result.ToString();
        }

        private char[] operators = { '+', '-', '*', '/' };

        private int? ComputePolishNotation(string n)
        {
            var input = n;
            int i = 0, j = 0, k = 0;
            int[] numbers = new int[2];
            char? op = null;
            do
            {
                var character = input[i];
                if (character != ' ')
                {
                    if (operators.Contains(character))
                    {
                        if(j > 0)
                        {
                            op = character;
                        }
                    }
                    else
                    {
                        var asciiCode = (int)character;
                        string number = string.Empty;
                        while(asciiCode >= 48 && asciiCode <= 57) // 0...9
                        {
                            number += character.ToString();
                            character = input[++i];
                            asciiCode = (int)character;
                        }
                        numbers[j++] = Int32.Parse(number.ToString());
                    }

                    if((j == 2) && (op != null))
                    {
                        var opResult = this.ComputeResult(numbers, (char)op);
                        var opResultStr = opResult.ToString();
                        if(k == 0)
                        {
                            input = opResultStr.Trim() + " " + input.Substring(i + 1).Trim();
                            k = opResultStr.Length + 1;
                        }
                        else
                        {
                            input = input.Substring(0, k).Trim() + " " + opResultStr.Trim() + " " + input.Substring(i + 1).Trim();
                            k = k + opResultStr.Length + 2;
                        }
                        j = 0;
                        if (i >= input.Length - 2)
                        {
                            i = 0;
                            k = 0;
                        }
                        else
                        {
                            i = k;
                        }
                        op = null;
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    i++;
                }
            }
            while (isExprPartiallyEvaluated(input));

            return Convert.ToInt32(input);
        }

        private bool isExprPartiallyEvaluated(string input)
        {
            var foundOperator = false;
            for(var i = 0; i < operators.Length && !foundOperator; i++)
            {
                foundOperator = input.Contains(operators[i]);
            }
            return foundOperator;
        }

        private int ComputeResult(int[] numbers, char op)
        {
            int result = 0;
            switch(op)
            {
                case '+':
                    result = numbers[0] + numbers[1];
                    break;
                case '-':
                    result = numbers[0] - numbers[1];
                    break;
                case '*':
                    result = numbers[0] * numbers[1];
                    break;
                case '/':
                    result = numbers[0] / numbers[1];
                    break;
            }
            return result;
        }
    }
}
