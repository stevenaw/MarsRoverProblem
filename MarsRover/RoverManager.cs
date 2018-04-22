using MarsRover.Models;
using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class RoverManager : IRoverManager
    {
        public int PlateauWidth { get; private set; }
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

            PlateauWidth = width;
            PlateauLength = length;
        }

        public void LandRover(Rover rover)
        {
            if(Rovers.ContainsKey(rover.Id))
            {
                throw new InvalidOperationException("Rover already exists.");
            }

            if(rover.Position.X < 0 || rover.Position.X > PlateauLength || rover.Position.Z < 0 || rover.Position.Z > PlateauWidth)
            {
                throw new InvalidOperationException("Rover has landed off the plateau.");
            }
            
            foreach (var roverEntry in Rovers.Values)
                if (Vector.AreEqual(roverEntry.Position, rover.Position))
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
                var newPosition = Vector.Add(rover.Position, rover.Heading);
                if (newPosition.X < 0 || newPosition.X > PlateauLength || newPosition.Z < 0 || newPosition.Z > PlateauWidth)
                    throw new InvalidOperationException("Rover has driven off the plateau");

                foreach(var roverEntry in Rovers.Values)
                    if (Vector.AreEqual(roverEntry.Position, newPosition))
                        throw new InvalidOperationException("Collision! Curiousity got the best of this Martian rover");

                rover.Position = newPosition;
            }
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private static Vector GetNewHeading(Vector heading, RoverOperationType operationType)
        {
            if (operationType != RoverOperationType.RotateLeft && operationType != RoverOperationType.RotateRight)
                return heading;

            var theta = DegreeToRadian(operationType == RoverOperationType.RotateRight ? -90 : 90);

            var cs = Math.Cos(theta);
            var sn = Math.Sin(theta);

            var newX = heading.X * cs - heading.Z * sn;
            var newY = heading.X * sn + heading.Z * cs;

            return new Vector()
            {
                X = (int)Math.Round(newX),
                Z = (int)Math.Round(newY)
            };
        }
    }
}
