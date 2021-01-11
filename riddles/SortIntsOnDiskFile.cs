using System;
using System.IO;
using System.Reflection;

namespace Riddles
{
    public class SortIntsOnDiskFile : RiddleBase<string, string>
    {
        public override void GenerateInput()
        {
            Console.Write("Enter min value: ");
            var minValue = int.Parse(Console.ReadLine());

            Console.Write("Enter max value: ");
            var maxValue = int.Parse(Console.ReadLine());

            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name);

            var random = new UniqueRandom(minValue, maxValue);
            var input = new StreamWriter(this.Input);

            int? number = null;
            var counter = 0;

            do
            {
                number = random.Next();
                if(number != null)
                {
                    input.Write("{0} ", (int)number);
                    counter++;

                    if ((counter % 5) == 0)
                    {
                        input.Flush();
                    }
                }
            }
            while ((number != null) && (counter < maxValue));

            input.Close();

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name);
        }

        public override void Process()
        {
            this.MeasureTimeStart(MethodBase.GetCurrentMethod().Name);

            var input = new StreamReader(this.Input);
            input.Close();

            var memory = new int[(1024 * 1024) / 4];

            var output = new StreamWriter(this.Output);
            output.Close();

            this.MeasureTimeEnd(MethodBase.GetCurrentMethod().Name);
        }
    }
}
