using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class Executor<In, Out>
    {
        public IAlgorithm<In, Out> Algorithm { get; private set; }
        public IKeyboardInput<In> Input { get; private set; }
        public IConsoleOutput<Out> Output { get; private set; }

        public Executor(IAlgorithm<In, Out> algorithm, IKeyboardInput<In> input, IConsoleOutput<Out> output)
        {
            this.Algorithm = algorithm;
            this.Input = input;
            this.Output = output;
        }

        public void Execute()
        {
            var inputData = this.Input.GetData();
            var outputData = this.Algorithm.Compute(inputData);
            this.Output.Display(outputData);
        }
    }
}
