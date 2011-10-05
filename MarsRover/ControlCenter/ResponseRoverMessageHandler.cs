using System;
using System.Drawing;
using Messages;
using NServiceBus;

namespace ControlCenter
{
    class ResponseRoverMessageHandler : IHandleMessages<ResponseRoverMessage>
    {
        /// <summary>Alert the user that the rover has sent a reply message</summary>
        /// <param name="message">The (ResponseRoverMessage) received</param>
        public void Handle(ResponseRoverMessage message)
        {
            Point roverPosition = new Point(message.PositionX, message.PositionY);

            Console.WriteLine("Rover reports current position as: {0}, facing: {1}.", roverPosition.ToString(), message.Direction);
        }
    }

    class DontSubscribe : IWantCustomInitialization
    {
        public void Init()
        {
            NServiceBus.Configure.Instance.UnicastBus().DoNotAutoSubscribe();
        }
    }
}
