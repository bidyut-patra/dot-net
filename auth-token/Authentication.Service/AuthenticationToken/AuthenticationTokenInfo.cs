using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthenticationTokenInfo : AuthenticationInfo
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        public AuthenticationTokenInfo(UserInfo user) : base(user)
        {
            this.Token = TokenStore.Instance.GetNewToken(user);
            this.SetAuthenticated();
            this.SetAuthenticationType("token");
        }
    }
}
