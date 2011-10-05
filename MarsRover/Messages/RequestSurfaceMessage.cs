using System;
using NServiceBus;

namespace Messages
{
    public class RequestSurfaceMessage : IMessage
    {
        /// <summary>A globally-unique identifier for the message</summary>
        public Guid MessageID { get; set; }
        /// <summary>The desired width of the plateau</summary>
        public int Width { get; set; }
        /// <summary>The desired height of the plateu</summary>
        public int Height { get; set; }
    }
}
