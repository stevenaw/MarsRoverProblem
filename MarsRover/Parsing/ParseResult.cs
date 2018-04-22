using MarsRover.Models;
using System.Collections.ObjectModel;

namespace MarsRover.Parsing
{
    public class ParseResult
    {
        public int PlateauWidth { get; set; }
        public int PlateauLength { get; set; }
        public Collection<RoverOperationQueue> RoverOperations { get; set; }
        public Collection<Rover> Rovers { get; set; }

        public ParseResult()
        {
            RoverOperations = new Collection<RoverOperationQueue>();
            Rovers = new Collection<Rover>();
        }
    }
}
