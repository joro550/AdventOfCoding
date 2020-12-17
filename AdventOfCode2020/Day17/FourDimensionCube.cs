using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public record FourDimensionCube(Position Position, bool Active)
        :Cube(Position, Active)
    {
        public override IEnumerable<Position> GetNeighbourCoordinates()
        {
            if (NeighbourPositions.Any()) 
                return NeighbourPositions;
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var z = 0; z < 3; z++)
                    {
                        for (var w = 0; w < 3; w++)
                        {
                            var newX = x == 0 ? -1 : x == 1 ? 0 : 1;
                            var newY = y == 0 ? -1 : y == 1 ? 0 : 1;
                            var newZ = z == 0 ? -1 : z == 1 ? 0 : 1;
                            var newW = w == 0 ? -1 : w == 1 ? 0 : 1;

                            var position = Position as FourDPosition ?? throw new Exception("not 4d");
                            var fourDPosition = new FourDPosition(position.X + newX, position.Y + newY, position.Z + newZ, position.W + newW);
                            if(fourDPosition == Position)
                                continue;
                            NeighbourPositions.Add(fourDPosition);
                        }
                    }
                } 
            }
            return NeighbourPositions;
        } 
    }
}