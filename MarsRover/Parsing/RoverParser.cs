using MarsRover.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover.Parsing
{
    public class RoverParser : IRoverParser
    {
        public RoverOperationQueue ParseMovement(string lineContents)
        {
            var result = Regex.Match(lineContents, @"^Rover(\d+)\sInstructions:\s*([LRM]+)\s*$", RegexOptions.IgnoreCase);
            if (result.Success)
            {
                var movements = result.Groups[2].Value.Select(o => (RoverOperationType)Char.ToUpper(o)).ToArray();
                return new RoverOperationQueue()
                {
                    RoverId = Int32.Parse(result.Groups[1].Value),
                    Operations = new Collection<RoverOperationType>(movements)
                };
            }
            else
            {
                return null;
            }
        }

        public Rover ParseLanding(string lineContents)
        {
            var result = Regex.Match(lineContents, @"^Rover(\d+)\sLanding:\s*(\d+)\s*(\d+)\s*([NSEW])\s*$", RegexOptions.IgnoreCase);

            if (result.Success)
            {
                return new Rover()
                {
                    Id = Int32.Parse(result.Groups[1].Value),
                    Position = new RoverPosition()
                    {
                        X = Int32.Parse(result.Groups[2].Value),
                        Y = Int32.Parse(result.Groups[3].Value)
                    },
                    Heading = (RoverHeading)Char.ToUpper(result.Groups[4].Value[0])
                };
            }
            else
            {
                return null;
            }
        }

        public ParseResult ParseInitialization(string firstLine)
        {
            var result = Regex.Match(firstLine, @"^Plateau:\s*(\d+)\s+(\d+)\s*$", RegexOptions.IgnoreCase);

            if (!result.Success)
            {
                return null;
            }
            else
            {
                var width = Int32.Parse(result.Groups[1].Value);
                var length = Int32.Parse(result.Groups[2].Value);

                if (width <= 0 || length <= 0)
                    return null;

                return new ParseResult()
                {
                    PlateauWidth = width,
                    PlateauLength = length
                };
            }
        }
    }
}
