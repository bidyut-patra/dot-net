using System;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using PES.Async.Messages;

namespace PES.SignalR.Core
{
    /// <summary>
    /// Base hub class
    /// </summary>
    public class BaseHub : Hub
    {
        public Dictionary<string, HubChannelManager> Connections { get; set; }

        public BaseHub()
        {
            this.Connections = new Dictionary<string, HubChannelManager>();
        }

        /// <summary>
        /// Send data to the clients registered to the specified channel / all channels
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        public async Task Send(IMessage message, string channel = null)
        {
            if(string.IsNullOrEmpty(channel))
            {
                await this.Clients?.All?.SendAsync(ClientCallbacks.ReceiveMessage, message);
            }
            else
            {
                await this.Clients?.Group(channel)?.SendAsync(ClientCallbacks.ReceiveMessage, message);
            }
        }

        /// <summary>
        /// Register the channel for this connection context
        /// </summary>
        /// <param name="channel"></param>
        public async Task Connect(string channel)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, channel);
            this.SaveChannel(this.Context.ConnectionId, channel);
        }

        /// <summary>
        /// Deletes the channel registered for this connection context
        /// </summary>
        /// <param name="channel"></param>
        public async Task Disconnect(string channel)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, channel);
            this.DeleteChannel(this.Context.ConnectionId, channel);
        }

        /// <summary>
        /// Saves the channel registered for this connection context
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="channel"></param>
        private void SaveChannel(string connectionId, string channel)
        {
            if(this.Connections.ContainsKey(connectionId))
            {
                var hubChannelManager = this.Connections[connectionId];
                hubChannelManager.AddChannel(channel, this.Context.User.Identity);
            }
            else
            {
                var hubChannelManager = new HubChannelManager();
                hubChannelManager.AddChannel(channel, this.Context.User.Identity);
                this.AddConnection(connectionId, hubChannelManager);
            }
        }

        /// <summary>
        /// Deletes the channel registered for this connection context
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="channel"></param>
        private void DeleteChannel(string connectionId, string channel)
        {
            if (this.Connections.ContainsKey(connectionId))
            {
                var hubChannelManager = this.Connections[connectionId];
                hubChannelManager.RemoveChannel(channel);
            }
        }

        /// <summary>
        /// Creates a connection for this connection context
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="hubChannelManager"></param>
        private void AddConnection(string connectionId, HubChannelManager hubChannelManager = null)
        {
            if(this.Connections.ContainsKey(connectionId) == false)
            {
                var channelManager = hubChannelManager ?? new HubChannelManager();
                this.Connections.Add(connectionId, channelManager);
            }
        }

        /// <summary>
        /// Deletes a connection for this connection context
        /// </summary>
        /// <param name="connectionId"></param>
        private void DeleteConnection(string connectionId)
        {
            if(this.Connections.ContainsKey(connectionId))
            {
                this.Connections.Remove(connectionId);
            }
        }

        /// <summary>
        /// On connection estabilished
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            this.AddConnection(this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// On disconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            this.DeleteConnection(this.Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Dispose all channels & connections
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
