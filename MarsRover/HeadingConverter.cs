using MarsRover.Models;
using System;

namespace MarsRover
{
    public class HeadingConverter
    {
        public const char North = 'N';
        public const char South = 'S';
        public const char East = 'E';
        public const char West = 'W';

        public char ToChar(Vector vector)
        {
            if (vector.Y == 1)
                return North;
            if (vector.Y == -1)
                return South;
            if (vector.X == 1)
                return East;
            if (vector.X == -1)
                return West;

            return '\0';
        }

        public Vector ToVector(char heading)
        {
            switch (Char.ToUpper(heading))
            {
                case North:
                    return new Vector()
                    {
                        X = 0,
                        Y = 1
                    };
                case South:
                    return new Vector()
                    {
                        X = 0,
                        Y = -1
                    };

                case East:
                    return new Vector()
                    {
                        X = 1,
                        Y = 0
                    };
                case West:
                    return new Vector()
                    {
                        X = -1,
                        Y = 0
                    };
            }

            return null;
        }
    }
}
