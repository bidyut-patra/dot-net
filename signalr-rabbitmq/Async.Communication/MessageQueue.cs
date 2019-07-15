using PES.Async.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PES.RabbitMQ.Core
{
    public class MessageQueue : IMessageQueue
    {
        public IConnection Connection { get; set; }
        private Action<IMessage> Subscription { get; set; }

        public MessageQueue()
        {
            this.Connection = new Connection();
        }

        public void Publish(string routingKey, IMessage message)
        {
            this.Connection.Connect(routingKey);
            this.Connection.Publish(routingKey, message);
        }

        public void Subscribe(string routingKey, Action<IMessage> action, string queueName = null)
        {
            this.Subscription = action;
            this.Connection.Connect(routingKey, queueName);
            this.Connection.Subscribe();
            this.Connection.OnDataReceived += this.OnDataReceived;
        }

        /// <summary>
        /// On data received
        /// </summary>
        /// <param name="msg"></param>
        private void OnDataReceived(IMessage msg)
        {
            if(this.Subscription != null)
            {
                this.Subscription(msg);
            }
        }
    }
}
