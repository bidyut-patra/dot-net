using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public interface IAuthenticationTokenService
    {
        string GetToken(string token);
        bool Logoff(string token);
    }
}
