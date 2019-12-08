using System;

namespace ConsoleApp.Riddles
{
    /*
     *  A string S consisting of N characters is considered to be properly nested if any of the following conditions is true:

        S is empty;
        S has the form "(U)" or "[U]" or "{U}" where U is a properly nested string;
        S has the form "VW" where V and W are properly nested strings.
        For example, the string "{[()()]}" is properly nested but "([)()]" is not.

        Write an efficient algorithm for the following assumptions:

        N is an integer within the range [0..200,000];
        string S consists only of the following characters: "(", "{", "[", "]", "}" and/or ")".
     */
    public class CheckEqualBrackets : IAlgorithm<string, bool>
    {
        public bool Compute(string input)
        {
            int[] bracketsCount = new int[3];
            bool nestingBroken = false;
            int lastOpeningBracketPosition = -1;
            for(var i = 0; (i < input.Length) && !nestingBroken; i++)
            {
                char bracketType;
                var currentBracketPosition = this.getBracket(input[i], out bracketType);
                if(currentBracketPosition > -1)
                {
                    if(bracketType == 'o') // 'o' : opening bracket
                    {
                        bracketsCount[currentBracketPosition] += 1;
                        lastOpeningBracketPosition = currentBracketPosition;
                    }
                    else // 'c': closing bracket
                    {
                        if(bracketsCount[currentBracketPosition] > 0)
                        {
                            if(currentBracketPosition == lastOpeningBracketPosition)
                            {
                                bracketsCount[currentBracketPosition] -= 1;
                                lastOpeningBracketPosition = -1;
                            }
                            else if(lastOpeningBracketPosition == -1)
                            {
                                bracketsCount[currentBracketPosition] -= 1;
                            }
                            else
                            {
                                nestingBroken = true;
                            }
                        }
                        else // closing bracket must be preceeded by opening bracket
                        {
                            nestingBroken = true;
                        }
                    }
                }
                else if(this.isAlphabet(input[i]))
                {
                    // Alphabets can appear between brackets
                }
                else
                {
                    // any other characters
                }
            }
            return nestingBroken ? false : (bracketsCount[0] == 0) && (bracketsCount[1] == 0) && (bracketsCount[2] == 0);
        }

        private bool isAlphabet(char c)
        {
            var ascii = (int)c;
            return (ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 132);
        }

        private int getBracket(char c, out char bracketType)
        {
            char[] openingBrackets = { '(', '[', '{' };
            char[] closingBrackets = { ')', ']', '}' };
            for (var i = 0; i < 3; i++)
            {
                if(openingBrackets[i] == c)
                {
                    bracketType = 'o';
                    return i;
                }
                if (closingBrackets[i] == c)
                {
                    bracketType = 'c';
                    return i;
                }
            }
            bracketType = '\0';
            return -1;
        }
    }
}
