using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PES.SignalR.Core
{
    public class HubChannel
    {
        public IIdentity User { get; set; }
        public string Name { get; set; }

        public HubChannel(string channelName, IIdentity identity)
        {
            this.Name = channelName;
            this.User = identity;            
        }
    }
}
