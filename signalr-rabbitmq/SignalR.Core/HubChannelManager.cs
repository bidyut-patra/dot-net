using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PES.SignalR.Core
{
    public class HubChannelManager
    {
        public Dictionary<string, HubChannel> Channels { get; private set; }

        public HubChannelManager()
        {
            this.Channels = new Dictionary<string, HubChannel>();
        }

        internal void AddChannel(string channel, IIdentity identity)
        {
            if(this.Channels.ContainsKey(channel))
            {
                // already the channel is registered
            }
            else
            {
                var hubChannel = new HubChannel(channel, identity);
                this.Channels.Add(channel, hubChannel);
            }
        }

        internal void RemoveChannel(string channel)
        {
            if(this.Channels.ContainsKey(channel))
            {
                this.Channels.Remove(channel);
            }
        }
    }
}
