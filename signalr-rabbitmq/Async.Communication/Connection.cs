using System;
using System.IO;
using PES.Async.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Runtime.Serialization.Formatters.Binary;

namespace PES.RabbitMQ.Core
{
    internal class Connection : IConnection
    {
        public DataReceive OnDataReceived { get; set; }
        private string QueueName { get; set; }
        private IModel Channel { get; set; }

        private string EXCHANGE_SUFFIX = "_exchange";
        private string QUEUE_SUFFIX = "_queue";

        public void Connect(string routingKey, string queueName = null)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                var connection = factory.CreateConnection();
                var exchange = this.GetExchangeName(routingKey);
                if(string.IsNullOrEmpty(queueName))
                {
                    this.QueueName = this.GetQueueName(routingKey);
                }
                else
                {
                    this.QueueName = this.GetQueueName(queueName);
                }
                this.Channel = connection.CreateModel();
                this.Channel.ExchangeDeclare(exchange, ExchangeType.Direct);
                this.Channel.QueueDeclare(queue: this.QueueName,
                                          durable: false,
                                          exclusive: false,
                                          autoDelete: false,
                                          arguments: null);
                this.Channel.QueueBind(this.QueueName, exchange, routingKey);
            }
            catch(BrokerUnreachableException e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
            catch(Exception e) 
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }
        }

        /// <summary>
        /// Gets the exchange name
        /// </summary>
        /// <param name="routingKey"></param>
        /// <returns></returns>
        private string GetExchangeName(string routingKey)
        {
            return routingKey + this.EXCHANGE_SUFFIX;
        }

        /// <summary>
        /// Gets the routing key
        /// </summary>
        /// <param name="routingKey"></param>
        /// <returns></returns>
        private string GetQueueName(string routingKey)
        {
            return routingKey + this.QUEUE_SUFFIX;
        }

        /// <summary>
        /// Initializes the subscription
        /// </summary>
        public void Subscribe()
        {
            if(this.Channel != null)
            {
                var consumer = new EventingBasicConsumer(this.Channel);
                consumer.Received += OnReceived;
                this.Channel.BasicConsume(queue: this.QueueName,
                                          autoAck: true,
                                          consumer: consumer);
            }
        }

        /// <summary>
        /// Publish the data to the exchange
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="message"></param>
        public void Publish(string routingKey, IMessage message)
        {
            if(this.Channel != null)
            {
                var exchange = this.GetExchangeName(routingKey);
                var bytes = this.ToByteArray<IMessage>(message);
                this.Channel.BasicPublish(exchange: exchange,
                                          routingKey: routingKey,
                                          basicProperties: null,
                                          body: bytes);
            }
        }

        /// <summary>
        /// On data received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            IMessage msg = this.FromByteArray<IMessage>(e.Body);
            this.OnDataReceived?.Invoke(msg);
        }

        /// <summary>
        /// Get object from byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        /// <summary>
        /// Convert the object to byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private byte[] ToByteArray<T>(T obj)
        {
            if (obj == null) return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
