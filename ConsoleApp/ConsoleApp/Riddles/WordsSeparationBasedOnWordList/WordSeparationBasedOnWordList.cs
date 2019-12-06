using System;

namespace ConsoleApp.Riddles
{
    public class WordSeparationBasedOnWordList : IAlgorithm<string[], string>
    {
        public string Compute(string[] input)
        {
            var word = input[0];
            var words = input[1];
            var wordList = words.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] identifiedWords = { };
            foreach(var w in wordList)
            {
                if(word.StartsWith(w))
                {
                    var secondWord = word.Substring(w.Length);
                    if(this.Contains(wordList, secondWord))
                    {
                        identifiedWords = new string[2];
                        identifiedWords[0] = w;
                        identifiedWords[1] = secondWord;
                    }
                }

                if (word.EndsWith(w))
                {
                    var firstWord = word.Substring(0, w.Length);
                    if (this.Contains(wordList, firstWord))
                    {
                        identifiedWords = new string[2];
                        identifiedWords[0] = firstWord;
                        identifiedWords[1] = w;
                    }
                }

                if (identifiedWords.Length == 2) break;
            }

            return identifiedWords.Length == 2 ? identifiedWords[0] + "," + identifiedWords[1] : "not possible";
        }


        public bool Contains(string[] words, string word)
        {
            bool wordPresent = false;
            foreach(var w in words)
            {
                if(word.Equals(w))
                {
                    wordPresent = true;
                    break;
                }
            }
            return wordPresent;
        }
    }
}
