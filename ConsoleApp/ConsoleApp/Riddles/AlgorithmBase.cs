using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public abstract class AlgorithmBase<In, Out> : IAlgorithm<In, Out>
    {
        public abstract Out Compute(In input);
    }
}
