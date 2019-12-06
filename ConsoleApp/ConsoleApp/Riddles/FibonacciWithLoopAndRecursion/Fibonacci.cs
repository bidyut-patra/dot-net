using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class Fibonacci : AlgorithmBase<int, int[]>
    {
        private bool _recursive;
        public int[] Numbers { get; private set; }

        public Fibonacci(bool recursive)
        {
            this._recursive = recursive;
        }

        /// <summary>
        /// Computes the fibonacci series
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override int[] Compute(int input)
        {
            int[] result = { };
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (this._recursive)
            {
                result = this.FibonacciRecursive(input);
            }
            else
            {
                result = this.FibonacciLoop(input);
            }
            stopwatch.Stop();
            var duration = stopwatch.Elapsed.Minutes + "M" + stopwatch.Elapsed.Seconds + "S" + stopwatch.Elapsed.Milliseconds + "MS";
            Console.WriteLine("Time taken: {0}", duration);
            return result;
        }

        /// <summary>
        /// Recursive fibonacci
        /// </summary>
        /// <param name="count"></param>
        /// <param name="r"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int[] FibonacciRecursive(int count, int[] r = null, int index = 2)
        {
            if (r == null)
            {
                r = new int[count];
                r[0] = 0;
                r[1] = 1;
            }

            if (index < count)
            {
                r[index] = r[index - 1] + r[index - 2];
                FibonacciRecursive(count, r, ++index);
            }

            return r;
        }

        /// <summary>
        /// Loop fibonacci
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private int[] FibonacciLoop(int count)
        {
            var result = new int[count];
            result[0] = 0;
            result[1] = 1;
            for (var i = 2; i < count; i++)
            {
                result[i] = result[i - 1] + result[i - 2];
            }
            return result;
        }
    }
}
