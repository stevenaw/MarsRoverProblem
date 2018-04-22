using System.Collections.ObjectModel;

namespace MarsRover.Models
{
    public class RoverOperationQueue
    {
        public int RoverId { get; set; }
        public Collection<RoverOperationType> Operations { get; set; }

        public RoverOperationQueue()
        {
            Operations = new Collection<RoverOperationType>();
        }
    }
}
