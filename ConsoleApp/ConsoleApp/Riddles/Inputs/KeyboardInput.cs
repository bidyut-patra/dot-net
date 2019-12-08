using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public abstract class KeyboardInput<T> : IInput<T>
    {
        public virtual T GetData()
        {
            Console.WriteLine();
            Console.Write(this.GetDisplayText());
            var val = Console.ReadLine();
            return (T)Convert.ChangeType(val, typeof(T));
        }

        protected virtual string GetDisplayText()
        {
            return "Enter: ";
        }
    }
}
