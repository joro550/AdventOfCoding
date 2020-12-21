using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day19
{
    public static class EdgeMatchFinder
    {
        public static List<TileMatch> FindMatchingEdges(List<Tile> tiles)
        {
            var tileDictionary =
                tiles.ToDictionary(tile => tile.Id, tile => new TileMatch(tile, new Dictionary<Side, (Tile, bool)>()));

            foreach (var tile in tiles)
            {
                var outerEdges = tile.GetOuterEdges();

                foreach (var otherTile in tiles.Where(t => t != tile))
                {
                    var (left, right, wasFlippedMatch) = outerEdges.Match(otherTile.GetOuterEdges());

                    if (left == Side.None || right == Side.None)
                        continue;

                    tileDictionary[tile.Id].AddSideMatch(left, otherTile, wasFlippedMatch);
                    tileDictionary[otherTile.Id].AddSideMatch(right, tile, wasFlippedMatch);
                }
            }
            return tileDictionary.Select(x => x.Value).ToList();
        }
    }
}