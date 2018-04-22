using MarsRover.Models;
using MarsRover.Parsing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace MarsRover.Tests
{
    public class RoverParserTests
    {
        public static IEnumerable<object[]> ValidInitializationData => new List<object[]> {
            new object[] { "Plateau:5 5", 5, 5 },
            new object[] { "Plateau:   6    4", 6, 4 },
            new object[] { "Plateau:   16    14", 16, 14 },
            new object[] { "Plateau:\t16\t14", 16, 14 },
            new object[] { "plateau:\t16\t14 \t", 16, 14 },
        };

        [Theory]
        [MemberData(nameof(ValidInitializationData))]
        public void ParseInitialization_ShouldParseValidCoordinates(string input, int width, int height)
        {
            var parser = new RoverParser();
            var results = parser.ParseInitialization(input);

            Assert.NotNull(results);
            Assert.Equal(width, results.PlateauWidth);
            Assert.Equal(height, results.PlateauLength);
        }


        public static IEnumerable<object[]> InvalidInitializationData => new List<object[]> {
            new object[] { "Plateau:-5 5" },
            new object[] { "Plateau:   6    -4" },
            new object[] { "Plateau:   k    -4" },
            new object[] { "Plateau:   8    k" },
            new object[] { "Plateau:   0    7" },
            new object[] { "Plateau:   7    0" },
            new object[] { "Plateau:   7 " },
            new object[] { "Plateau:   " },
            new object[] { "hohoho" },
            new object[] { "Rover1 Landing:1 2 N" },
            new object[] { "Rover1 Instructions:LLRM" },
        };

        [Theory]
        [MemberData(nameof(InvalidInitializationData))]
        public void ParseInitialization_ShouldReturnNullWhenInvalidCoordinates(string input)
        {
            var parser = new RoverParser();
            var results = parser.ParseInitialization(input);

            Assert.Null(results);
        }




        public static IEnumerable<object[]> ValidLandingData => new List<object[]> {
            new object[] { "Rover1 Landing:1 2 N", 1, 1, 2, RoverHeading.North },
            new object[] { "Rover10 Landing:\t11 14 E", 10, 11, 14, RoverHeading.East },
            new object[] { "Rover4 Landing: 0 0 W", 4, 0, 0, RoverHeading.West },
            new object[] { "rover10 landing: \t11 4 s \t", 10, 11, 4, RoverHeading.South },
        };

        [Theory]
        [MemberData(nameof(ValidLandingData))]
        public void ParseLanding_ShouldParseValidCoordinates(string input, int id, int x, int y, RoverHeading heading)
        {
            var parser = new RoverParser();
            var results = parser.ParseLanding(input);

            Assert.NotNull(results);
            Assert.NotNull(results.Position);

            Assert.Equal(id, results.Id);
            Assert.Equal(x, results.Position.X);
            Assert.Equal(y, results.Position.Y);
            Assert.Equal(heading, results.Heading);
        }


        public static IEnumerable<object[]> InvalidLandingData => new List<object[]> {
            new object[] { "Rover-1 Landing:1 2 N" },
            new object[] { "Rover1 Landing:-1 2 N" },
            new object[] { "Rover1 Landing:1 -2 N" },
            new object[] { "Rover1 Landing:1 -2 Z" },
            new object[] { "RoverX Landing:1 2 N" },
            new object[] { "Rover1 Instructions:LLRM" },
            new object[] { "Plateau:5 5" },
        };

        [Theory]
        [MemberData(nameof(InvalidLandingData))]
        public void ParseLanding_ShouldReturnNullWhenInvalidCoordinates(string input)
        {
            var parser = new RoverParser();
            var results = parser.ParseLanding(input);

            Assert.Null(results);
        }




        public static IEnumerable<object[]> ValidMovementData => new List<object[]> {
            new object[] {
                "Rover1 Instructions:LLRM",
                1,
                new Collection<RoverOperationType>() {
                    RoverOperationType.RotateLeft,
                    RoverOperationType.RotateLeft,
                    RoverOperationType.RotateRight,
                    RoverOperationType.Move
                }
            },
            new object[] {
                "rover10 instructions: \tmrl \t",
                10,
                new Collection<RoverOperationType>() {
                    RoverOperationType.Move,
                    RoverOperationType.RotateRight,
                    RoverOperationType.RotateLeft,
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidMovementData))]
        public void ParseMovement_ShouldParseValidCoordinates(string input, int id, Collection<RoverOperationType> operations)
        {
            var parser = new RoverParser();
            var results = parser.ParseMovement(input);

            Assert.NotNull(results);
            Assert.Equal(results.RoverId, id);
            Assert.Equal(results.Operations, operations);
        }


        public static IEnumerable<object[]> InvalidMovementData => new List<object[]> {
            new object[] { "Rover-1 Instructions:MMMM" },
            new object[] { "RoverX Instructions:MMMM" },
            new object[] { "Rover1 Instructions:QQQQ" },
            new object[] { "Rover1 Instructions:" },
            new object[] { "Plateau:5 5" },
            new object[] { "Rover1 Landing:1 2 N" },
        };

        [Theory]
        [MemberData(nameof(InvalidMovementData))]
        public void ParseMovement_ShouldReturnNullWhenInvalidCoordinates(string input)
        {
            var parser = new RoverParser();
            var results = parser.ParseMovement(input);

            Assert.Null(results);
        }
    }
}
