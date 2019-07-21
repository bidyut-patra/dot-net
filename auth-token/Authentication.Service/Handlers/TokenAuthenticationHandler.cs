using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Authentication.Service
{
    public class TokenAuthenticationHandler : DelegatingHandler
    {
        private string AuthUrl { get; set; }

        public TokenAuthenticationHandler(string authUrl)
        {
            this.AuthUrl = authUrl;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method.Method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase))
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                var headers = request.Headers;
                if (headers.Contains("fs-token"))
                {
                    string oldToken = headers.GetValues("fs-token").First();
                    var sessionData = this.GetUserSession(oldToken);
                    request.GetRequestContext().Principal = sessionData.UserInfo;
                    var response = base.SendAsync(request, cancellationToken);
                    response.Result.Headers.Add("fs-token", sessionData.Token);
                    return response;
                }
                else
                {
                    return Task.FromResult(request.CreateResponse(HttpStatusCode.Unauthorized));
                }
            }
        }

        private UserSession GetUserSession(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, this.AuthUrl + "/api/session?token=" + token);
            var response = client.SendAsync(request);
            var content = response.Result.Content.ReadAsAsync(typeof(UserSession));
            return (UserSession)content.Result;
        }
    }
}
