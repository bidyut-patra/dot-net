using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public interface IConsoleOutput<T>
    {
        void Display(T data);
    }
}
