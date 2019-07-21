using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Service.Host
{
    public class PreflightModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication application)
        {
            if(application.Context.Request.HttpMethod.Equals("OPTIONS"))
            {
                application.CompleteRequest();
            }
        }
    }
}