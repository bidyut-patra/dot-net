
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthenticationException : ApplicationException
    {
        public string Error { get; set; }
    }
}
