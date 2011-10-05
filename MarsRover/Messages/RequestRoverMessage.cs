using System;
using NServiceBus;

namespace Messages
{
    public class RequestRoverMessage : IMessage
    {
        /// <summary>A globally-unique identifier for the message</summary>
        public Guid MessageID { get; set; }
        /// <summary>The starting position for the rover, on the x-axis</summary>
        public int StartingPositionX { get; set; }
        /// <summary>The starting position for the rover, on the y-xaxis</summary>
        public int StartingPositionY { get; set; }
        /// <summary>The initial direction that the rover should face</summary>
        public String StartingDirection { get; set; }
        /// <summary>A string of commands that the rover should execute</summary>
        public String Commands { get; set; }
    }
}
