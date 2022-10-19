using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureStore
{
    internal class DecryptionError : ApplicationException
    {
        internal DecryptionError(string message): base(message)
        {

        }
    }
}
