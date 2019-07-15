using PES.Async.Messages;
using PES.RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var queue = new MessageQueue();
            queue.Subscribe("notification", OnReceive);
            Console.WriteLine("Consumer service started...");
            Console.ReadLine();
        }

        private static void OnReceive(IMessage message)
        {
            var testMessage = (TestMessage)message;
            Console.WriteLine("Message received at [{0}]: {1}", DateTime.Now, message.Message);
        }
    }
}
