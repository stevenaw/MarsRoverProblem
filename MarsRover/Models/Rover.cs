namespace MarsRover.Models
{
    public class Rover
    {
        public int Id { get; set; }
        public RoverPosition Position { get; set; }
        public RoverHeading Heading { get; set; }
    }
}
