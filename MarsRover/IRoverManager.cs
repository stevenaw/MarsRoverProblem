using MarsRover.Models;
using System.Collections.Generic;

namespace MarsRover
{
    public interface IRoverManager
    {
        void DirectRover(int roverId, RoverOperationType operationType);
        void DiscoverPlateau(int width, int length);
        void LandRover(Rover rover);
        Dictionary<int, Rover> Rovers { get; }
    }
}