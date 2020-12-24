using Xunit;
using System.Linq;
using AdventOfCode2020.Day24;

namespace AdventOfCode2020.Tests.Day24
{
    public class Day24Tests
    {
        [Fact]
        public void FlipStartingTile()
        {
            const string input = "nwwswee";

            var tiles = TileFinder.FindTiles(input);
            
            var expectedPosition = new Position(0,0);
            Assert.True(tiles.ContainsKey(expectedPosition.GetKey()));
            Assert.Equal(1, tiles.Values.Count(tile => tile.Colour == Colour.Black));
        }
        
        [Fact]
        public void Example()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day24.PuzzleExample.txt");

            var tiles = TileFinder.FindTiles(input);
            Assert.Equal(10, tiles.Values.Count(tile => tile.Colour == Colour.Black));
        }
        
        [Fact]
        public void Puzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day24.PuzzleInput.txt");

            var tiles = TileFinder.FindTiles(input);
            Assert.Equal(269, tiles.Values.Count(tile => tile.Colour == Colour.Black));
        }
        
        
        [Fact]
        public void Puzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day24.PuzzleInput.txt");

            var tiles = TileFinder.FindTiles(input);

            var dayFlipper = new DayTileFlipper();
            for (var i = 0; i < 100; i++)
            {
                tiles = dayFlipper.FlipTilesForDay(tiles);
            }
            
            
            Assert.Equal(3667, tiles.Values.Count(tile => tile.Colour == Colour.Black));
        }
    }
}