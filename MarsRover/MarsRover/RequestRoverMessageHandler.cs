using System;
using System.Drawing;
using Messages;
using NServiceBus;

namespace MarsRover
{
    public class RequestRoverMessageHandler : IHandleMessages<RequestRoverMessage>
    {
        /// <summary>NServiceBus object</summary>
        public IBus Bus { get; set; }

        /// <summary>Initiate a new rover</summary>
        /// <param name="message">The (RequestRoverMessage) received</param>
        public void Handle(RequestRoverMessage message)
        {
            Point roverStartingPositon = new Point(message.StartingPositionX, message.StartingPositionY);

            PhysicalObject.Heading roverDirection = PhysicalObject.Heading.North;
            switch (message.StartingDirection)
            {
                case "N":
                    roverDirection = PhysicalObject.Heading.North;
                    break;
                case "E":
                    roverDirection = PhysicalObject.Heading.East;
                    break;
                case "S":
                    roverDirection = PhysicalObject.Heading.South;
                    break;
                case "W":
                    roverDirection = PhysicalObject.Heading.West;
                    break;
            }

            Console.WriteLine("Received request to dispatch rover to: {0}, facing: {1}.", roverStartingPositon.ToString(), roverDirection.ToString());
            Console.WriteLine("Rover will execute commands: [{0}].", message.Commands);

            Rover rover = new Rover(Mars.Plateu, roverStartingPositon, roverDirection);

            foreach (Char character in message.Commands)
            {
                switch (character)
                {
                    case 'L':
                        rover.Rotate(PhysicalObject.Rotation.Left);
                        break;
                    case 'R':
                        rover.Rotate(PhysicalObject.Rotation.Right);
                        break;
                    case 'M':
                        rover.Move();
                        break;
                }
            }

            // Send a message back to the client to notify them we did something:
            var response = Bus.CreateInstance<ResponseRoverMessage>(m =>
            {
                m.MessageID = message.MessageID;
                m.PositionX = Mars.Plateu.GetPosition(rover).X;
                m.PositionY = Mars.Plateu.GetPosition(rover).Y;
                m.Direction = rover.Direction.ToString();
            });

            Bus.Reply(response);
        }
    }
}
