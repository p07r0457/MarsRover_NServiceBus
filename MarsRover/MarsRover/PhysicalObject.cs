using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class PhysicalObject
    {
        /// <summary>
        /// Possible directions for our objects to be facing.
        /// Correlates to degrees so it can be extended, in the future.
        /// </summary>
        public enum Heading
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270
        }


        /// <summary>Possible ways that an object can be rotated</summary>
        public enum Rotation
        {
            Left = -90,
            Right = 90
        }


        /// <summary>Define the direction our object is facing</summary>
        public virtual Heading Direction { get; set; }


        /// <summary>Determine what a heading would be after performing a rotation</summary>
        /// <param name="currentDirection">the current (Heading) of the object</param>
        /// <param name="desiredRotation">The (Rotation) that should be applied</param>
        /// <returns>The resulting (Heading)</returns>
        protected Heading Rotate(Heading currentDirection, Rotation desiredRotation)
        {
            int degreeHeading = (int)currentDirection + (int)desiredRotation;
            // Check if we rotated left of North; if so, set West
            if (degreeHeading < 0)
            {
                return Heading.West;
            }
            
            // Check if we rotated right of West; if so, set North
            if (degreeHeading > 270)
            {
                return Heading.North;
            }

            return (Heading)degreeHeading;
        }
    }
}
