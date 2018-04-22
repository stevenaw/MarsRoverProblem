namespace MarsRover.Models
{
    public class RoverPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public static bool AreEqual(RoverPosition a, RoverPosition b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
    }
}
