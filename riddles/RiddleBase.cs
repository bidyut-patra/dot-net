﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Riddles
{
    public abstract class RiddleBase<T1, T2>
    {
        public T1 Input { get; set; }
        public T2 Output { get; set; }

        private Stopwatch Stopwatch = new Stopwatch();

        public abstract void Process();
        public abstract void GenerateInput();

        protected void MeasureTimeStart(string methodName, string message)
        {
            this.Stopwatch.Start();
            var className = this.GetType().Name;
            var methodFullName = className + "." + methodName;
            Console.WriteLine("{0} : {1}", methodFullName, message);
        }

        protected void MeasureTimeEnd(string methodName, string message)
        {
            this.Stopwatch.Stop();
            var elapsed = this.Stopwatch.Elapsed;
            var duration = String.Format("{0}H {1}M {2}S {3}MS", elapsed.Hours, elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);
            var className = this.GetType().Name;
            var methodFullName = className + "." + methodName;
            Console.WriteLine("{0} : Time taken for {1} = {2}", methodFullName, message, duration);
            this.Stopwatch.Reset();
        }
    }
}
