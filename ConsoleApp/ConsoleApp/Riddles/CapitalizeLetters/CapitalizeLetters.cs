using System;

namespace ConsoleApp.Riddles
{
    public class CapitalizeLetters : IAlgorithm<string, string>
    {
        public string Compute(string input)
        {
            for(var i = 0; i < input.Length; i++)
            {
                int c = (int)input[i];
                if(c >= 97 && c <= 122)
                {
                    char newChar = (char)(c - 32);
                    input = input.Replace(input[i], newChar);
                }
            }
            return input;
        }
    }
}
