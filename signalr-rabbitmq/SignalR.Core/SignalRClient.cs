using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;

namespace PES.SignalR.Core
{
    public class SignalRClient : ISignalRClient
    {
        public Dictionary<Type, HubConnection> HubConnections { get; private set; }

        public SignalRClient()
        {
            this.HubConnections = new Dictionary<Type, HubConnection>();
        }

        /// <summary>
        /// Connect to a hub of specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IHubSubscription ConnectHub<T>(string baseUrl) where T : BaseHub
        {
            var hubType = this.GetHubType<T>();
            var hubUri = HubUrlRegistry.GetUrl(hubType);
            var hubUrl = baseUrl + hubUri;
            HubSubscription hubSubscription = null;
            if(string.IsNullOrEmpty(hubUrl) == false)
            {
                var connection = new HubConnectionBuilder();
                var hubConnection = connection.WithUrl(hubUrl).Build();
                Console.WriteLine(string.Format("Connected to hub '{0}'...", hubType.Name));
                this.HubConnections.Add(hubType, hubConnection);
                hubSubscription = new HubSubscription(hubConnection);
            }
            return hubSubscription;
        }

        /// <summary>
        /// Disconnect hub of specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool DisconnectHub<T>() where T : BaseHub
        {
            var hubType = this.GetHubType<T>();
            bool disconnected = false;
            if(this.HubConnections.ContainsKey(hubType))
            {
                var hubConnection = this.HubConnections[hubType];
                hubConnection.StopAsync();
                disconnected = this.HubConnections.Remove(hubType);
            }
            return disconnected;
        }

        /// <summary>
        /// Gets the hub type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Type GetHubType<T>()
        {
            var hubType = typeof(T);
            return hubType;
        }
    }
}
