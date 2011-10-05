using System;
using System.Drawing;
using Messages;
using NServiceBus;

namespace MarsRover
{
    public class RequestSurfaceMessageHandler : IHandleMessages<RequestSurfaceMessage>
    {
        /// <summary>Initiate a new plateu on Mars.</summary>
        /// <param name="message">The (RequestSurfaceMessage) received</param>
        public void Handle(RequestSurfaceMessage message)
        {
            Mars.Plateu = new Surface(new Size(message.Width, message.Height));
            Console.WriteLine("Initialized Plateau of size: {0}.", Mars.Plateu.Dimensions.ToString());
        }
    }
}
