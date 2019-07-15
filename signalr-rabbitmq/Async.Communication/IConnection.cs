using PES.Async.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PES.RabbitMQ.Core
{
    public delegate void DataReceive(IMessage message);
    public interface IConnection
    {
        DataReceive OnDataReceived { get; set; }
        void Connect(string routingKey, string queueName = null);
        void Subscribe();
        void Publish(string routingKey, IMessage message);
    }
}
