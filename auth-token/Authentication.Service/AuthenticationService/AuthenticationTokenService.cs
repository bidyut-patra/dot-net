using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthenticationTokenService : AuthenticationService, IAuthenticationTokenService
    {
        public string GetToken(string token)
        {
            return TokenStore.Instance.GetNewToken(token);
        }

        public override AuthenticationInfo Login(UserInfo userInfo)
        {
            return new AuthenticationTokenInfo(userInfo);
        }

        public bool Logoff(string token)
        {
            var userInfo = TokenStore.Instance.RemoveToken(token);
            this.Logoff(userInfo);
            return true;
        }

        public override UserSession GetSession(string token)
        {
            return TokenStore.Instance.GetSession(token);
        }
    }
}
