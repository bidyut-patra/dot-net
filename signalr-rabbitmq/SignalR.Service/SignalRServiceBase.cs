using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PES.Async.Messages;
using PES.RabbitMQ.Core;
using PES.SignalR.Core;

namespace PES.SignalR.Service
{
    public class SignalRServiceBase : BackgroundService
    {
        public IHubContext<NotificationHub> HubContext { get; private set; }

        public SignalRServiceBase(IHubContext<NotificationHub> hubContext)
        {
            this.HubContext = hubContext;
        }

        /// <summary>
        /// Perform startup task while starting the service 
        /// like subscribes to notification queue.
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            await Task.Run(() =>
            {
                var queue = new MessageQueue();
                queue.Subscribe("notification", this.ProcessMessage, "websocket");
            });
        }

        /// <summary>
        /// Processes incoming messages
        /// </summary>
        /// <param name="message"></param>
        private void ProcessMessage(IMessage message)
        {
            Console.WriteLine("Sent message: {0}", message.Message);
            // Get the hub context and push the message
            //((NotificationHub)this.HubContext).Send(message);
            var notificationHub = this.HubContext as NotificationHub;
            this.HubContext.Clients.All.SendAsync(ClientCallbacks.ReceiveMessage, message);
        }

        /// <summary>
        /// Perform tasks while stopping background service
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            await Task.Run(() =>
            {
            });
        }

        /// <summary>
        /// Executes the service async
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return this.Start();
        }

        /// <summary>
        /// Stop background service async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return this.Stop();
        }
    }
}
