using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day17
{
    public record Cube(Position Position, bool Active, bool HasGeneratedNeighbours)
    {
        protected readonly HashSet<Position> NeighbourPositions 
            = new();
        
        public static Cube CreateCube(Position p)
        {
            return p switch
            {
                FourDPosition position => new FourDimensionCube(position, false, false),
                _ => new Cube(p, false, false)
            };
        }

        public (Position Min, Position Max) GetRange()
        {
            var minPosition = Position - 1;
            var maxPosition = Position + 1;
            return (minPosition, maxPosition);
        } 

        public virtual IEnumerable<Position> GetNeighbourCoordinates()
        {
            if (NeighbourPositions.Any()) 
                return NeighbourPositions;

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var z = 0; z < 3; z++)
                    {
                        var newX = x == 0 ? -1 : x == 1 ? 0 : 1;
                        var newY = y == 0 ? -1 : y == 1 ? 0 : 1;
                        var newZ = z == 0 ? -1 : z == 1 ? 0 : 1;
                        
                        var position = new Position(Position.X + newX, Position.Y + newY, Position.Z + newZ);
                        
                        // We are not our own neighbour
                        if(position == Position)
                            continue;

                        NeighbourPositions.Add(position);
                    }
                } 
            }
            return NeighbourPositions;
        }

        public string? GetString() => Active ? "#" : ".";
    }
}