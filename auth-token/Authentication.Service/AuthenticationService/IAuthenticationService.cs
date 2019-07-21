using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public interface IAuthenticationService
    {
        AuthenticationInfo Login(UserInfo userInfo);
        void Logoff(UserInfo userInfo);
        UserSession GetSession(string token);
    }
}
