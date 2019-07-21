using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        public virtual AuthenticationInfo Login(UserInfo userInfo)
        {
            return new AuthenticationInfo(userInfo);
        }

        public virtual void Logoff(UserInfo userInfo)
        {
            
        }

        public virtual UserSession GetSession(string token)
        {
            return null;
        }
    }
}
