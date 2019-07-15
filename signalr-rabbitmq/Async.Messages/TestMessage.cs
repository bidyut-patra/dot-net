using System;

namespace PES.Async.Messages
{
    [Serializable]
    public class TestMessage : IMessage
    {
        public string ParentId { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
    }
}