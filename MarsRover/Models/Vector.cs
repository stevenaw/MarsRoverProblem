namespace MarsRover.Models
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public static bool AreEqual(Vector a, Vector b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }
    }
}
