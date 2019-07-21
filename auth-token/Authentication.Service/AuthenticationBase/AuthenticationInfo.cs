using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthenticationInfo : IIdentity
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("authType")]
        public string AuthenticationType { get; private set; }

        [JsonProperty("isAuthenticated")]
        public bool IsAuthenticated { get; private set; }

        [JsonIgnore]
        public UserInfo UserInfo { get; protected set; }

        public AuthenticationInfo(UserInfo user)
        {
            this.UserInfo = user;
        }

        /// <summary>
        /// Sets the user name
        /// </summary>
        /// <param name="name"></param>
        internal void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Sets the authentication type
        /// </summary>
        /// <param name="authType"></param>
        internal void SetAuthenticationType(string authType)
        {
            this.AuthenticationType = authType;
        }

        /// <summary>
        /// Sets user authenticated
        /// </summary>
        internal void SetAuthenticated()
        {
            this.IsAuthenticated = true;
        }
    }
}
