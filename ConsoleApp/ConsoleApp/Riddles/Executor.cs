using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class Executor<In, Out>
    {
        public IAlgorithm<In, Out> Algorithm { get; private set; }
        public IInput<In> Input { get; private set; }
        public IOutput<Out> Output { get; private set; }
        public bool CaptureExecutionTime { get; set; } = true;

        public Executor(IAlgorithm<In, Out> algorithm, IInput<In> input)
        {
            this.Algorithm = algorithm;
            this.Input = input;
        }

        public Executor(IAlgorithm<In, Out> algorithm, IInput<In> input, IOutput<Out> output)
        {
            this.Algorithm = algorithm;
            this.Input = input;
            this.Output = output;
        }

        public Out Execute()
        {
            var inputData = this.Input.GetData();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var outputData = this.Algorithm.Compute(inputData);
            stopwatch.Stop();
            if(this.CaptureExecutionTime)
            {
                var execTimeText = stopwatch.Elapsed.Minutes + "M" + stopwatch.Elapsed.Seconds + "S" + stopwatch.Elapsed.Milliseconds + "MS";
                Console.WriteLine("Time taken to compute: " + execTimeText);
            }
            if(this.Output != null)
            {
                this.Output.Print(outputData);
            }
            return outputData;
        }
    }
}
