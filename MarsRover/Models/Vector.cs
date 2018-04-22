namespace MarsRover.Models
{
    public class Vector
    {
        public int X { get; set; }
        public int Z { get; set; }
        
        public static bool AreEqual(Vector a, Vector b)
        {
            return a.X == b.X && a.Z == b.Z;
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector()
            {
                X = a.X + b.X,
                Z = a.Z + b.Z
            };
        }
    }
}
