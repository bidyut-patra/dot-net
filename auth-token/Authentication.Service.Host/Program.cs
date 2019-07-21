using System;
using Autofac;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Autofac.Integration.WebApi;
using System.Reflection;
using Microsoft.Owin.Hosting;

namespace Authentication.Service.Host
{
    public class Program
    {
        private static readonly string AUTH_URL_PARAM = "AuthenticationUrl";
        private static readonly string AUTH_URL_DEFAULT_VALUE = "http://localhost:8087";

        public static void Main(string[] args)
        {
            try
            {
                var authUrl = AuthenticationHelper.GetConfigurationValue(AUTH_URL_PARAM, AUTH_URL_DEFAULT_VALUE);

                #region Web Api Server Code For Self Hosting

                bool owinSelfHost = (args.Length == 1) && (args[0].Equals("OSH")) ? true : false;
                Console.WriteLine("Starting Web API Server {0}", owinSelfHost ? "using OWIN..." : "...");
                if (owinSelfHost)
                {
                    OwinSelfHostWebApi(authUrl);
                }
                else
                {
                    SelfHostWebApi(authUrl);
                }

                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("Web API Server could not be started because {0} {1}", e.Message, e.InnerException?.Message);
            }
        }

        /// <summary>
        /// OWIN Self Host
        /// </summary>
        /// <param name="authUrl"></param>
        private static void OwinSelfHostWebApi(string authUrl)
        {
            using (WebApp.Start<Startup>(authUrl))
            {
                Console.WriteLine("Web API Server has started at {0}.", authUrl);
                Console.WriteLine("Hit Enter to shutdown Web API Server.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Self Host
        /// </summary>
        /// <param name="authUrl"></param>
        private static void SelfHostWebApi(string authUrl)
        {
            var config = new HttpSelfHostConfiguration(authUrl);

            Startup.Configure(config);

            var server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();

            Console.WriteLine("Web API Server has started at {0}.", authUrl);
            Console.WriteLine("Hit Enter to shutdown Web API Server.");
            Console.ReadLine();
        }
    }
}
