using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day19
{
    public static class MapBuilder
    {
        public static List<TileMatch> PlaceMap(IEnumerable<TileMatch> tileMatches)
        {
            var tileMatch = tileMatches.ToDictionary(match => match.Tile.Id);
            return TileMatch.Handle(tileMatch).Select(x => x.Value).ToList();
        }

        public static Map BuildMap(List<TileMatch> tileMatches)
        {
            var tileMatch = tileMatches.ToDictionary(match => match.Tile.Id);
            var topLeft = tileMatches.First(c => c.Matches.Count == 2 && !c.Matches.ContainsKey(Side.Left) && !c.Matches.ContainsKey(Side.Top));

            var spaces = new List<Space>(); 
           
            var tile = tileMatch[topLeft.Tile.Id];
            
            var maxX = tile.Tile.Spaces.Max(c => c.Value.Position.X) + 1;
            var maxY = tile.Tile.Spaces.Max(c => c.Value.Position.Y) + 1;

            var x = 0;
            var y = 0;

            do
            {
                var cacheTile = tile;
                
                do
                {
                    foreach (var ((spaceX, spaceY), isActivated) in tile.Tile.Spaces.Values)
                    {
                        spaces.Add(new Space(spaceX + x, spaceY + y, isActivated));
                    }
                    
                    x += maxX;
                    tile = tile.Matches.ContainsKey(Side.Right) ? tileMatch[tile.Matches[Side.Right].Tile.Id] :null ;

                } while (tile != null);
                
                y += maxY;
                tile = cacheTile.Matches.ContainsKey(Side.Bottom) ?  tileMatch[cacheTile.Matches[Side.Bottom].Tile.Id] : null;

            } while (tile != null);


            return new Map(spaces);
        }
    }
}