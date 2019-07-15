using PES.RabbitMQ.Core;
using PES.Async.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Producer service started...");
            var queue = new MessageQueue();
            var validTextEntered = false;
            do
            {
                Console.WriteLine("Enter text: ");
                var text = Console.ReadLine();
                var trimmedText = text.Trim();
                validTextEntered = string.IsNullOrEmpty(trimmedText) == false;
                if(validTextEntered)
                {
                    queue.Publish("notification", new TestMessage() 
                    {
                        ParentId = null,
                        Id = "TEST",
                        Message = trimmedText
                    });
                    Console.WriteLine("Message published at {0}", DateTime.Now);
                }
            }
            while (validTextEntered);
        }
    }
}
