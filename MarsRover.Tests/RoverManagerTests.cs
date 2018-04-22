using MarsRover.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverManagerTests
    {
        public static IEnumerable<object[]> InvalidPlateauData => new List<object[]> {
            new object[] { -1, 4 },
            new object[] { 4, -1 },
            new object[] { 0, 0 },
        };

        [Theory]
        [MemberData(nameof(InvalidPlateauData))]
        public void LandRover_MustLandOnPlateau(int x, int y)
        {
            var manager = new RoverManager();

            Assert.Throws<InvalidOperationException>(() => manager.DiscoverPlateau(x, y));
        }

        [Fact]
        public void LandRover_MustNotRelandRover()
        {
            var manager = new RoverManager();
            manager.DiscoverPlateau(5, 5);

            manager.LandRover(new Rover()
            {
                Id = 1,
                Position = new RoverPosition()
                {
                    X = 4,
                    Y = 3
                }
            });

            Assert.Throws<InvalidOperationException>(() => manager.LandRover(new Rover()
            {
                Id = 1,
                Position = new RoverPosition()
                {
                    X = 5,
                    Y = 8
                }
            }));
        }


        [Fact]
        public void LandRover_MustNotLandOnAnotherRover()
        {
            var manager = new RoverManager();
            manager.DiscoverPlateau(5, 5);

            manager.LandRover(new Rover()
            {
                Id = 1,
                Position = new RoverPosition()
                {
                    X = 4,
                    Y = 3
                }
            });

            Assert.Throws<InvalidOperationException>(() => manager.LandRover(new Rover()
            {
                Id = 2,
                Position = new RoverPosition()
                {
                    X = 4,
                    Y = 3
                }
            }));
        }

        [Fact]
        public void DirectRover_RequiresRoverToHaveLanded()
        {
            var manager = new RoverManager();
            manager.DiscoverPlateau(5, 5);

            manager.LandRover(new Rover()
            {
                Id = 1,
                Position = new RoverPosition()
                {
                    X = 4,
                    Y = 3
                }
            });

            Assert.Throws<InvalidOperationException>(() => manager.DirectRover(2, RoverOperationType.Move));
        }

        [Fact]
        public void DirectRover_WillNotDriveIntoOtherRovers()
        {
            var manager = new RoverManager();
            manager.DiscoverPlateau(5, 5);

            manager.LandRover(new Rover()
            {
                Id = 1,
                Heading = RoverHeading.North,
                Position = new RoverPosition()
                {
                    X = 4,
                    Y = 3
                }
            });

            manager.LandRover(new Rover()
            {
                Id = 2,
                Heading = RoverHeading.North,
                Position = new RoverPosition()
                {
                    X = 4,
                    Y = 4
                }
            });

            Assert.Throws<InvalidOperationException>(() => manager.DirectRover(1, RoverOperationType.Move));
        }
    }
}
