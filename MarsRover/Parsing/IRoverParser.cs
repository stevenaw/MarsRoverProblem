using MarsRover.Models;

namespace MarsRover.Parsing
{
    public interface IRoverParser
    {
        ParseResult ParseInitialization(string firstLine);
        Rover ParseLanding(string lineContents);
        RoverOperationQueue ParseMovement(string lineContents);
    }
}