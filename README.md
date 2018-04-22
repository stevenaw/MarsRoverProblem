### Mars Rover Problem

A little app for playing around with .NET Core and xUnit. Run from the command-line, takes one argument (a file name).
The file contains instructions to land and operate one or more mars rovers.

Example:

```
Plateau:5 5
Rover1 Landing:1 2 N
Rover1 Instructions:LMLMLMLMM
Rover2 Landing:3 3 E
Rover2 Instructions:MMRMMRMRRM
```

Plateau measurements are the length/width of the rectangular plateau, in units.
Rover landing  instructions are the coordinates to land at, and the heading to start at. A heading can be:
- N = Noth
- E = East
- S = South
- W = West

Rover instructions are a sequence of commands in the form:
- M = Move forward 1 unit
- R = Turn right 90 degrees.
- L = Turn left 90 degrees.