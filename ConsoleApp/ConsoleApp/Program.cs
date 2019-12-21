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

        private static void OpenWindows()
        {
            //var main = new Riddles.Windows.Main();
            //main.ShowDialog();
        }

        private static void ExecuteOthers()
        {
            //var rx = new ReactiveExample();
            //rx.Test();

            int[] a = { 1, 2, -4, 5, 6, -4, 4, 5, -5, 12, 11, 12, -12 };
            for(var i = 0; i < a.Length; i++)
            {
                var hashCode = a[i].GetHashCode();
                Console.Write("{0} ", hashCode);
            }
            Console.ReadLine();
        }

        private static void ExecuteAlgorithms()
        {
            //ExecuteAction(new Executor<string, string>(new GenerateSecuredGuid(), new StringInput(), new StringOutput()));
            //var generatedNumbers = new Executor<int[], int[]>(new GenerateNumberPairSequence(), new NumbersInput()) { CaptureExecutionTime = false }.Execute();
            //ExecuteAction(new Executor<int[], int>(new FindMaxNumberPairs(), new NumbersInput(generatedNumbers), new NumberOutput()));
            ExecuteAction(new Executor<int[], int>(new FindMaxNumberPairs(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<int[], int>(new FindSocksForLaundering(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<int[], int>(new FindMaxMatchesInTennis(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<int[], int>(new MinimizeBlockLargestSum(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<string, bool>(new CheckEqualBrackets(), new StringInput(), new EqualBracketOutput()));
            var generatedNumbers = new Executor<int[], int[]>(new GenerateNumberSequence(), new NumbersInput()) { CaptureExecutionTime = false }.Execute();
            //ExecuteAction(new Executor<int[], int[]>(new FindDistinctValues(), new NumbersInput(generatedNumbers), new NumbersOutput()));
            ExecuteAction(new Executor<int[], int[]>(new FindDistinctValues(), new NumbersInput(), new NumbersOutput()));
            ExecuteAction(new Executor<int[], int>(new FindNumbersDivisibleByGivenNumber(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<int[], bool>(new CheckPermutationArray(), new NumbersInput(), new BooleanOutput()));
            generatedNumbers = new Executor<int[], int[]>(new GenerateNumberSequence(), new NumbersInput()) { CaptureExecutionTime = false }.Execute();
            ExecuteAction(new Executor<int[], int>(new FindMissingNumberInSequence(), new NumbersInput(generatedNumbers), new NumberOutput()));
            //ExecuteAction(new Executor<int[], int>(new FindMissingNumberInSequence(), new NumbersInput(), new NumberOutput()));
            ExecuteAction(new Executor<int[], string>(new FindUnpairedNumbers(), new NumbersInput(), new StringOutput()));
            ExecuteAction(new Executor<int[], int[]>(new LongestBinaryGap(), new InputNumbersFromFile(), new NumbersOutput()));
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