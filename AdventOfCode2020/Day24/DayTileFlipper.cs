using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day24
{
    public class DayTileFlipper
    {
        public Dictionary<string, Tile> FlipTilesForDay(Dictionary<string, Tile> currentState)
        {
            var blackTiles = currentState.Values.Where(tile => tile.Colour == Colour.Black)
                .ToArray();

            var blackTileRule = new BlackTileRule();
            var newWhiteTiles = blackTileRule.ApplyRule(blackTiles, currentState);
            var newBlackTiles = GetFlippedTiles(currentState, blackTiles);

            var newState = new Dictionary<string, Tile>(currentState);
            foreach (var tile in newWhiteTiles)
            {
                var tileKey = tile.Position.GetKey();
                if (newState.ContainsKey(tileKey))
                {
                    newState[tileKey] = newState[tileKey].FlipColour();
                }
                else
                {
                    newState.Add(tileKey, tile);
                }
            }
            
            foreach (var tile in newBlackTiles)
            {
                var tileKey = tile.Position.GetKey();
                if (newState.ContainsKey(tileKey))
                {
                    newState[tileKey] = newState[tileKey].FlipColour();
                }
                else
                {
                    newState.Add(tileKey, tile);
                }
            }

            return newState;
        }

        private static IEnumerable<Tile> GetFlippedTiles(Dictionary<string, Tile> currentState, Tile[] blackTiles)
        {
            var neighbourDictionary = new Dictionary<string, Tile>();
            var neighbours = blackTiles.Select(x => x.GetNeighbours(currentState));

            foreach (var neighbour in neighbours)
            {
                var whiteNeighbours = neighbour.Where(x => x.Colour == Colour.White);
                foreach (var tile in whiteNeighbours)
                {
                    var tileKey = tile.Position.GetKey();
                    if (!neighbourDictionary.ContainsKey(tileKey))
                    {
                        neighbourDictionary.Add(tileKey, tile);
                    }
                }
            }
            return new WhiteTileRule().ApplyRule(neighbourDictionary.Values, currentState);
        }
    }

    public record BlackTileRule : Rule
    {
        public override Tile[] ApplyRule(IEnumerable<Tile> tiles, Dictionary<string, Tile> currentState)
        {
            var returnTiles = new List<Tile>();
            
            foreach (var tile in tiles)
            {
                var neighbours = tile.GetNeighbours(currentState).Count(x => x.Colour == Colour.Black);
                if (neighbours == 0 || neighbours > 2)
                    returnTiles.Add(tile.FlipColour());
            }

            return returnTiles.ToArray();
        }
    }

    public record WhiteTileRule : Rule
    {
        public override Tile[] ApplyRule(IEnumerable<Tile> tiles, Dictionary<string, Tile> currentState)
        {
            var returnTiles = new List<Tile>();
            
            foreach (var tile in tiles)
            {
                var neighbours = tile.GetNeighbours(currentState).Count(x => x.Colour == Colour.Black);
                if (neighbours == 2)
                    returnTiles.Add(tile.FlipColour());
            }
            return returnTiles.ToArray();
        }
    }

    public abstract record Rule
    {
        public abstract Tile[] ApplyRule(IEnumerable<Tile> tiles, Dictionary<string, Tile> currentState);
    }
}