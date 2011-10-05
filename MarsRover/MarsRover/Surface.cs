using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MarsRover
{
    public class Surface
    {
        private Size dimensions;


        /// <summary>The (Size) of the surface</summary>
        public virtual Size Dimensions
        {
            get
            {
                return this.dimensions;
            }
        }


        /// <summary>A collection of (PhysicalObject) that represents objects on the surface.</summary>
        public virtual Dictionary<PhysicalObject, Point> Objects { get; set; }

        /// <summary>Create a new surface</summary>
        /// <param name="desiredSize">(Size) How big should the surface be?</param>
        public Surface(Size desiredSize)
        {
            this.Objects = new Dictionary<PhysicalObject,Point>();
            this.dimensions = desiredSize;
        }


        /// <summary>Check if a location is on the surface</summary>
        /// <param name="desiredLocation">The (Point) to test</param>
        /// <returns>(Boolean)</returns>
        private Boolean SolidGround(Point desiredLocation)
        {
            Point resultingLocation = Point.Subtract(desiredLocation, this.Dimensions);
            if (desiredLocation.X < 0 || desiredLocation.Y < 0 || resultingLocation.X > 0 || resultingLocation.Y > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Check if a location is valid to occupy.
        /// * Ensure that another object does not occupy the location.
        /// * Ensure that the location is on the surface.
        /// </summary>
        /// <param name="desiredLocation">The (Point) to test</param>
        /// <returns>(Boolean)</returns>
        public Boolean LocationAvailable(Point desiredLocation)
        {
            if (!this.SolidGround(desiredLocation))
            {
                return false;
            }

            if (this.Objects != null)
            {
                return !this.Objects.ContainsValue(desiredLocation);
            }
            else
            {
                return true;
            }
        }


        /// <summary>Add an object to our surface</summary>
        /// <param name="newObject">The (PhysicalObject) that we want to add</param>
        /// <param name="startingPosition">The (Point) where the object should originate</param>
        /// <returns>(Boolean) Could the object be added?</returns>
        public Boolean AddObject(PhysicalObject newObject, Point startingPosition)
        {
            if (!this.LocationAvailable(startingPosition))
            {
                return false;
            }

            this.Objects.Add(newObject, startingPosition);
            return true;
        }


        /// <summary>Get the position of an object on the surface</summary>
        /// <param name="testObject">The (PhysicalObject) that we want to find.</param>
        /// <returns>The (Point) where the object is currently located.</returns>
        public Point GetPosition(PhysicalObject testObject)
        {
            if (this.Objects != null && this.Objects.ContainsKey(testObject))
            {
                return this.Objects[testObject];
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("testObject", "The specified object is not on this surface.");
            }
        }


        /// <summary>Attempt to move an object to a new position on this surface</summary>
        /// <param name="testObject">The (PhysicalObject) that we want to move.</param>
        /// <param name="newPosition">The (Point) that we want to move it to.</param>
        /// <returns>(Boolean) Was the move successful?</returns>
        public Boolean MoveObject(PhysicalObject testObject, Point newPosition)
        {
            if (!this.LocationAvailable(newPosition))
            {
                return false;
            }

            if (!this.Objects.ContainsKey(testObject))
            {
                throw new System.ArgumentOutOfRangeException("testObject", "The specified object is not on this surface.");
            }
            else
            {
                this.Objects[testObject] = newPosition;
                return true;
            }
        }
    }
}
