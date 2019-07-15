using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PES.Async.Messages;

namespace PES.SignalR.Core
{
    public class HubSubscription : IHubSubscription
    {
        public HubConnection HubConnection { get; set; }

        public HubSubscription(HubConnection connection)
        {
            this.HubConnection = connection;
        }

        public async Task Subscribe(string channel, Action<IMessage> action)
        {
            this.HubConnection.On(ClientCallbacks.ReceiveMessage, action);
            await this.HubConnection.StartAsync();
            await this.HubConnection.InvokeAsync(ServerCallbacks.Connect, channel);
        }

        public async Task Unsubscribe(string channel)
        {
            await this.HubConnection.InvokeAsync(ServerCallbacks.Disconnect, channel);
            this.HubConnection.Remove(ClientCallbacks.ReceiveMessage);
        }
    }
}
