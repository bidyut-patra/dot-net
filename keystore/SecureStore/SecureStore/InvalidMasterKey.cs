using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStore
{
    internal class InvalidMasterKey : ApplicationException
    {
        internal InvalidMasterKey(string message): base(message)
        {

        }
    }
}
