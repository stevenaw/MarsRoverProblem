using MarsRover.Models;
using MarsRover.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarsRover
{
    public class RoverService
    {
        private IRoverParser _parser;
        private IRoverManager _manager;

        public RoverService(IRoverParser parser, IRoverManager manager)
        {
            _parser = parser;
            _manager = manager;
        }

        public void ExecuteInstructions(string[] instructions)
        {
            if (instructions.Any())
            {
                var initialization = _parser.ParseInitialization(instructions[0]);
                _manager.DiscoverPlateau(initialization.PlateauWidth, initialization.PlateauLength);

                for (int i = 1; i < instructions.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(instructions[i]))
                    {
                        var landing = _parser.ParseLanding(instructions[i]);
                        if (landing != null)
                            _manager.LandRover(landing);

                        var movement = _parser.ParseMovement(instructions[i]);
                        if (movement != null)
                            foreach (var operation in movement.Operations)
                                _manager.DirectRover(movement.RoverId, operation);
                    }
                }
            }
        }

        public IEnumerable<Rover> EnumerateRovers()
        {
            return _manager.Rovers.Values;
        }
    }
}
