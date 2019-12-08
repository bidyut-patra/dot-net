using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class OutputToFile<T> : IOutput<T>
    {        
        public virtual void Print(T data)
        {
            
        }
    }
}
