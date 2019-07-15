using PES.Async.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PES.RabbitMQ.Core
{
    public interface IMessageQueue
    {
        void Publish(string queueName, IMessage msg);
        void Subscribe(string routingKey, Action<IMessage> action, string queueName = null);
    }
}
