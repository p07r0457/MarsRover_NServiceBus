using System;
using System.Drawing;
using NServiceBus;
using Messages;

namespace ControlCenter
{
    public class ClientEndpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            String[] Input;

            Console.WriteLine("Please specify the size of the Mars Plateu:");
            Console.WriteLine("The first number is the width, and the second number is the height.");

            Input = Console.ReadLine().Split(' ');
            Size plateu = new Size(Convert.ToInt32(Input[0]), Convert.ToInt32(Input[1]));

            Console.WriteLine("Initializing Plateau with size: {0}.", plateu.ToString());

            Bus.Send<RequestSurfaceMessage>(m =>
                {
                    m.MessageID = Guid.NewGuid();
                    m.Width = plateu.Width;
                    m.Height = plateu.Height;
                });


            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Please specify the starting position and direction of the rover:");
                Console.WriteLine("The first number is the horizontal coordinate, the second number is the vertical coordinate");
                Console.WriteLine("The third value is a single character representing a Cardinal Point (N, S, E, or W)");
                Console.WriteLine("Please enter a blank line to indicate you have no more rovers to enter.");

                Input = Console.ReadLine().Split(' ');

                try
                {
                    Point roverStartingPosition = new Point(Convert.ToInt32(Input[0]), Convert.ToInt32(Input[1]));
                    String roverDirection = Input[2].ToUpper();

                    Console.WriteLine("Please provide a string of directions for the rover to follow.  No spaces are requred.");
                    Console.WriteLine("{ L = Turn Left; R = Turn Right; M = Move 1 unit forward }");

                    String roverCommands = Console.ReadLine().Replace(" ", "").ToUpper();

                    Console.WriteLine("Sending message to rover...");

                    Bus.Send<RequestRoverMessage>(m =>
                        {
                            m.MessageID = Guid.NewGuid();
                            m.StartingPositionX = roverStartingPosition.X;
                            m.StartingPositionY = roverStartingPosition.Y;
                            m.StartingDirection = roverDirection;
                            m.Commands = roverCommands;
                        });


                }
                catch
                {
                    break;
                }
            }





        }

        public void Stop()
        {
        }
    }
}
