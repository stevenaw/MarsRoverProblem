using MarsRover.Models;
using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class RoverManager : IRoverManager
    {
        public int PlateauHeight { get; private set; }
        public int PlateauLength { get; private set; }
        public Dictionary<int, Rover> Rovers { get; private set; }

        public RoverManager()
        {
            Rovers = new Dictionary<int, Rover>();
        }

        public void DiscoverPlateau(int width, int length)
        {
            if (width <= 0)
                throw new InvalidOperationException("Plateau must have real width");

            if (length <= 0)
                throw new InvalidOperationException("Plateau must have real length");

            PlateauHeight = width;
            PlateauLength = length;
        }

        public void LandRover(Rover rover)
        {
            if(Rovers.ContainsKey(rover.Id))
            {
                throw new InvalidOperationException("Rover already exists.");
            }

            if(rover.Position.X < 0 || rover.Position.X > PlateauLength || rover.Position.Y < 0 || rover.Position.Y > PlateauHeight)
            {
                throw new InvalidOperationException("Rover has landed off the plateau.");
            }
            
            foreach (var roverEntry in Rovers.Values)
                if (RoverPosition.AreEqual(roverEntry.Position, rover.Position))
                    throw new InvalidOperationException("Rover has crash-landed on another rover.");

            Rovers.Add(rover.Id, rover);
        }

        public void DirectRover(int roverId, RoverOperationType operationType)
        {
            Rover rover;
            if(!Rovers.TryGetValue(roverId, out rover))
            {
                throw new InvalidOperationException("Rover does not exist");
            }
            
            if(operationType == RoverOperationType.RotateLeft || operationType == RoverOperationType.RotateRight)
            {
                rover.Heading = GetNewHeading(rover.Heading, operationType);
            }
            else if (operationType == RoverOperationType.Move)
            {
                var newPosition = GetNewPosition(rover);
                foreach(var roverEntry in Rovers.Values)
                    if (RoverPosition.AreEqual(roverEntry.Position, newPosition))
                        throw new InvalidOperationException("Collision! Curiousity got the best of this Martian rover");

                rover.Position = newPosition;
            }
        }

        private static RoverPosition GetNewPosition(Rover rover)
        {
            var deltaX = 0;
            var deltaY = 0;

            if (rover.Heading == RoverHeading.North)
                deltaY = 1;
            else if (rover.Heading == RoverHeading.South)
                deltaY = -1;
            else if (rover.Heading == RoverHeading.East)
                deltaX = 1;
            else if (rover.Heading == RoverHeading.West)
                deltaX = -1;

            return new RoverPosition()
            {
                X = rover.Position.X + deltaX,
                Y = rover.Position.Y + deltaY
            };
        }

        // Probably better done as a 1-unit vector
        private static RoverHeading GetNewHeading(RoverHeading heading, RoverOperationType operationType)
        {
            if (operationType != RoverOperationType.RotateLeft && operationType != RoverOperationType.RotateRight)
                return heading;

            if(heading == RoverHeading.North)
            {
                return operationType == RoverOperationType.RotateLeft ? RoverHeading.West : RoverHeading.East;
            }
            if (heading == RoverHeading.South)
            {
                return operationType == RoverOperationType.RotateLeft ? RoverHeading.East : RoverHeading.West;
            }
            if (heading == RoverHeading.East)
            {
                return operationType == RoverOperationType.RotateLeft ? RoverHeading.North : RoverHeading.South;
            }
            if (heading == RoverHeading.West)
            {
                return operationType == RoverOperationType.RotateLeft ? RoverHeading.South : RoverHeading.North;
            }

            return heading;
        }
    }
}
