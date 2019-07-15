using System;
using PES.SignalR.Core;

namespace SignalR.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting SignalR client...");
            var signalRClient = new SignalRClient();
            var subscription = signalRClient.ConnectHub<NotificationHub>("http://localhost:5000");
            subscription.Subscribe("notification", data =>
            {
                Console.WriteLine(string.Format("Received data: {0}", data));
            });
            Console.ReadLine();
        }
    }
}
