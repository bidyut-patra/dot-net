using System;
using System.Collections.Generic;
using System.Text;

namespace PES.Async.Messages
{
    public interface IMessage
    {
        string ParentId { get; set; }
        string Id { get; set; }
        string Message { get; set; }
    }
}
