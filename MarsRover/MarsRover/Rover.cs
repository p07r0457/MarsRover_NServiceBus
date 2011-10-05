using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MarsRover
{
    public class Rover: PhysicalObject
    {
        /// <summary>Where is our rover?</summary>
        private Surface parent;
        /// <summary>Did our rover crash?</summary>
        private Boolean crashLanded;


        /// <summary>[READONLY] Did the rover crash?</summary>
        public virtual Boolean CrashLanded
        {
            get { return this.crashLanded; }
        }
        /// <summary>[READONLY] The surface this rover is on.</summary>
        public virtual Surface Parent
        {
            get { return this.parent; }
        }


        /// <summary>Create a new rover</summary>
        /// <param name="mySurface">What (Surface) should the rover land on?</param>
        /// <param name="startingPosition">What (Point) does the rover originate at?</param>
        /// <param name="directionFacing">What (Heading) is our rover point in?</param>
        public Rover(Surface mySurface, Point startingPosition, Heading directionFacing)
        {
            this.parent = mySurface;
            this.Direction = directionFacing;
            this.crashLanded = !this.parent.AddObject(this, startingPosition);
        }


        /// <summary>Rotate our rover 90 degrees</summary>
        /// <param name="rotateToThe">The (Rotation) direction to rotate.</param>
        public void Rotate(Rotation rotateToThe)
        {
            this.Direction = this.Rotate(this.Direction, rotateToThe);
        }


        /// <summary>Move the rover 1 unit</summary>
        /// <returns>(Boolean) was the move successful?</returns>
        public Boolean Move()
        {
            if (this.CrashLanded)
            {
                return false;
            }

            Point newPosition = this.Parent.GetPosition(this);
            if (newPosition == null)
            {
                return false;
            }

            switch (this.Direction)
            {
                case Heading.North:
                    newPosition.Y += 1;
                    break;
                case Heading.East:
                    newPosition.X += 1;
                    break;
                case Heading.South:
                    newPosition.Y -= 1;
                    break;
                case Heading.West:
                    newPosition.X -= 1;
                    break;
            }

            return this.Parent.MoveObject(this, newPosition);
        }
    }
}
