using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public abstract class ConsoleOutput<T> : IOutput<T>
    {
        public abstract void Print(T data);

        protected virtual string GetDisplayText()
        {
            return "Output";
        }
    }
}
