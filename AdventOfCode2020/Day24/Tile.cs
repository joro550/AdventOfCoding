using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day24
{
    public record Tile(Position Position, Colour Colour = Colour.White)
    {
        public Tile(int x, int y, Colour colour = Colour.White)
            :this(new Position(x, y), colour)
        {
            
        }
        
        public Tile FlipColour()
        {
            if (Colour == Colour.Black)
                return this with {Colour = Colour.White};
            return this with {Colour = Colour.Black};
        }

        public Tile[] GetNeighbours(Dictionary<string, Tile> state)
        {
            var positions = new List<Position>
            {
                // north east
                new(0.5f, 0.5f),

                // north west
                new(0.5f, -0.5f),

                // west
                new(0f, -1f),

                // east
                new(0f, 1f),

                // south east
                new(-0.5f, 0.5f),

                // south west
                new(-0.5f, -0.5f),
            };

            // 2006 called, it wants it's linq back
            return (from position in positions
                select new Position(Position.X + position.X, Position.Y + position.Y)
                into neighbourPosition
                let key = neighbourPosition.GetKey()
                select state.ContainsKey(key) ? state[key] : new Tile(neighbourPosition))
                .ToArray();
        }
        
    }
}