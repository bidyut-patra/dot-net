using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class UserInfo : IPrincipal
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonIgnore]
        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
