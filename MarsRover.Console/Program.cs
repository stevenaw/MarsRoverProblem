using MarsRover.Parsing;
using System;
using System.IO;
using System.Linq;

namespace MarsRover.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var roverService = new RoverService(new RoverParser(), new RoverManager());

            string fileName = args.FirstOrDefault();
            var fileContents = File.ReadAllLines(fileName);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                System.Console.Error.WriteLine("Please enter a file name.");
            }
            else
            {
                try
                {
                    roverService.ExecuteInstructions(fileContents);

                    foreach (var rover in roverService.EnumerateRovers())
                        System.Console.WriteLine(string.Format("Rover{0}:{1} {2} {3}", rover.Id, rover.Position.X, rover.Position.Y, (char)rover.Heading));
                }
                catch (Exception ex)
                {
                    System.Console.Error.WriteLine(ex.Message);
                }
            }

            System.Console.Write("Please press any key...");
            System.Console.ReadLine();
        }
    }
}
