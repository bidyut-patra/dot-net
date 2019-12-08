using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class InputNumbersFromFile : InputFromFile<int[]>
    {
        public override int[] GetData()
        {
            var words = this.Read();
            int[] numbers = new int[words.Length];

            for(var i = 0; i < words.Length; i++)
            {
                Int32.TryParse(words[i], out numbers[i]);
            }

            return numbers;
        }
    }
}
