using System;
using System.Threading.Tasks;
using PES.Async.Messages;

namespace PES.SignalR.Core
{
    public interface ISignalRClient
    {
        IHubSubscription ConnectHub<T>(string baseUrl) where T : BaseHub;
        bool DisconnectHub<T>() where T : BaseHub;
    }

    public interface IHubSubscription
    {
        Task Subscribe(string channel, Action<IMessage> action);
        Task Unsubscribe(string channel);
    }
}
