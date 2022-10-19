using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace securewebapi
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8080/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpClient and make a request to api/values 
                var client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}
