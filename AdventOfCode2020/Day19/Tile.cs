using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day19
{
    public record Tile(long Id, Dictionary<string, Space> Spaces)
    {
        public Tile(long id, List<Space> spaces)
            :this(id, spaces.ToDictionary(space => space.GetUniqueKey()))
        {
        }

        public Space GetSpaceAtPosition(Position position)
        {
            var key = position.GetUniqueKey();
            return Spaces[key];
        }

        public Tile FlipHorizontally()
        {
            var maxX = Spaces.Values.Max(s => s.Position.X);
            var maxY = Spaces.Values.Max(s => s.Position.Y);

            var spaces = new List<Space>();
            for (int x = maxX, xIndex = 0; x >= 0; xIndex++, x--)
            {
                for (var y = 0; y <= maxY; y ++)
                {
                    var (_, isActivated) = GetSpaceAtPosition(new Position(x, y));
                    spaces.Add(new Space(xIndex, y, isActivated));
                }
            }
            return new Tile(Id, spaces);
        }
        
        public Tile FlipVertically()
        {
            var maxX = Spaces.Values.Max(s => s.Position.X);
            var maxY = Spaces.Values.Max(s => s.Position.Y);

            var spaces = new List<Space>();
            for (var x = 0; x <= maxX; x++)
            {
                for (int y = maxY, yIndex = 0; y >= 0; y --, yIndex++)
                {
                    var (_, isActivated) = GetSpaceAtPosition(new Position(x, yIndex));
                    spaces.Add(new Space(x, y, isActivated));
                }
            }
            return new Tile(Id, spaces);
        }

        public Tile RotateLeft()
        {
            var maxX = Spaces.Values.Max(s => s.Position.X);
            var maxY = Spaces.Values.Max(s => s.Position.Y);

            var spaces = new List<Space>();
            for (int y = 0, newX = 0; y <= maxY; y++, newX ++)
            {
                for (int x = maxX, newY = 0; x >= 0; x--, newY ++)
                {
                    var (_, isActivated) = GetSpaceAtPosition(new Position(x, y));
                    spaces.Add(new Space(newX, newY, isActivated));
                }
            } 
            return new Tile(Id, spaces);
        }

        public OuterEdges GetOuterEdges()
        {
            var maxX = Spaces.Values.Max(s => s.Position.X);
            var maxY = Spaces.Values.Max(s => s.Position.Y);

            var top = GetSide(maxX, x => new Position(x, 0));
            var bottom = GetSide(maxX, x => new Position(x, maxY));
            var left = GetSide(maxY, y => new Position(0, y));
            var right = GetSide(maxY, y => new Position(maxX, y));

            return new OuterEdges(top, bottom, left, right);
        }

        private List<Space> GetSide(int maxDirection, Func<int, Position> position)
        {
            var top = new List<Space>();
            for (var x = 0; x <= maxDirection; x++) 
                top.Add(GetSpaceAtPosition(position(x)));
            return top;
        }
    }
}