using ConsoleApp.Riddles;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExecuteOthers();
            ExecuteAlgorithms();
        }

        private static void ExecuteOthers()
        {
            var rx = new ReactiveExample();
            rx.Test();
        }

        private static void ExecuteAlgorithms()
        {
            ExecuteAction(new Executor<string, string>(new ReversePolishNotation(), new StringInput(), new StringOutput()));
            ExecuteAction(new Executor<int[], int[]>(new FindMaxTwoNumbers(), new NumbersInput(), new NumbersOutput()));
            ExecuteAction(new Executor<string[], string>(new WordSeparationBasedOnWordList(), new StringsInput(), new StringOutput()));
            ExecuteAction(new Executor<string, string>(new CapitalizeLetters(), new StringInput(), new StringOutput()));
            ExecuteAction(new Executor<int[], double[]>(new GenerateNumbersWithEqualProbability(), new NumbersInput(), new FloatNumbersOutput()));
            ExecuteAction(new Executor<int, int[]>(new PrintNumbersWithoutLoop(), new NumberInput(), new NumbersOutput()));
            ExecuteAction(new Executor<string, bool>(new UniqueString(), new StringInput(), new BooleanOutput()));
            ExecuteAction(new Executor<int[], int[]>(new SmallestLargestNumbers(), new NumbersInput(), new NumbersOutput()));
            ExecuteAction(new Executor<int[], int>(new AddTwoNumbersWithoutAddOperator(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<int, int[]>(new Fibonacci(true), new FibonacciInput(), new FibonacciOutput(true)));
            ExecuteAction(new Executor<int, int[]>(new Fibonacci(false), new FibonacciInput(), new FibonacciOutput(false)));
        }


        /// <summary>
        /// Scans input/ performs calculation / generates output
        /// </summary>
        private static void ExecuteAction<T1, T2>(Executor<T1, T2> executor)
        {
            do
            {
                executor.Execute();
            }
            while (ConfirmForRepeatStep("Repeat"));
        }      

        /// <summary>
        /// Confirms repeat step
        /// </summary>
        /// <param name="displayText"></param>
        private static bool ConfirmForRepeatStep(string displayText)
        {
            Console.WriteLine();
            Console.Write(displayText + " ? ");
            var yesNo = Console.ReadLine();
            var repeatStep = yesNo.ToString().Equals("Y", StringComparison.CurrentCultureIgnoreCase) ? true : false;
            return repeatStep;
        }        
    }
}