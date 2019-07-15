using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace PES.SignalR.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting SignalR service....");
            CreateWebHostBuilder(args).Build().Run();
            Console.ReadLine();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        }
    }
}
