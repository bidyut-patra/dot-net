using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class InvalidTokenException : AuthenticationException
    {
        public InvalidTokenException(string token)
        {
            var invalidToken = string.IsNullOrEmpty(token) ? string.Empty : token;
            this.Error = string.Format(AuthenticationResources.InvalidToken, token);
        }
    }
}
