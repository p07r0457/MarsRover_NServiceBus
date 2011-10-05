using System;
using NServiceBus;

namespace Messages
{
    public class ResponseRoverMessage : IMessage
    {
        /// <summary>A globally-unique identifier for the message</summary>
        public virtual Guid MessageID { get; set; }
        /// <summary>The resulting position of the rover, on the x-axis</summary>
        public virtual int PositionX { get; set; }
        /// <summary>The resulting position of the rover, on the y-axis</summary>
        public virtual int PositionY { get; set; }
        /// <summary>The direction the rover is currently facing</summary>
        public virtual String Direction { get; set; }
    }
}
