using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public interface IAlgorithm<In, Out>
    {
        Out Compute(In input);
    }
}
