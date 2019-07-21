using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class LogRequestHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Client request: {0}", request.RequestUri.OriginalString);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
