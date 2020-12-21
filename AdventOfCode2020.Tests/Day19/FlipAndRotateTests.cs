using System;
using System.Linq;
using AdventOfCode2020.Day19;
using Xunit;

namespace AdventOfCode2020.Tests.Day19
{
    public class FlipAndRotateTests
    {
        [Fact]
        public void FlipHorizontalTest()
        {
            var input = @"Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###".Replace("|", Environment.NewLine);
            var expectedOutput = @"Tile 2311:|.#..#.##..|.....#..##|.#..##...#|#...#.####|.###.##.##|###.#...##|##..#.#.#.|..#....#..|.#.#...###|###..###..".Replace("|", Environment.NewLine);

            var tiles = TileParser.Parse(input);
            var expectedOutputTiles = TileParser.Parse(expectedOutput);

            var output = tiles[0].FlipHorizontally();
            
            var maxX = output.Spaces.Values.Max(s => s.Position.X);
            var maxY = output.Spaces.Values.Max(s => s.Position.Y);
            
            for (var x = 0; x <= maxX; x++, x++)
            {
                for (var y = 0; y <= maxY; y++ )
                {
                    var position = new Position(x, y);
                    var expected = expectedOutputTiles[0].GetSpaceAtPosition(position);
                    var actual = output.GetSpaceAtPosition(position);

                    Assert.Equal(expected, actual);
                }
            }
        }
        
        [Fact]
        public void FlipVerticallyTest()
        {
            var input = @"Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###".Replace("|", Environment.NewLine);
            var expectedOutput = @"Tile 2311:|..###..###|###...#.#.|..#....#..|.#.#.#..##|##...#.###|##.##.###.|####.#...#|#...##..#.|##..#.....|..##.#..#.".Replace("|", Environment.NewLine);

            var tiles = TileParser.Parse(input);
            var expectedOutputTiles = TileParser.Parse(expectedOutput);

            var output = tiles[0].FlipVertically();
            
            var maxX = output.Spaces.Values.Max(s => s.Position.X);
            var maxY = output.Spaces.Values.Max(s => s.Position.Y);
            
            for (var x = 0; x <= maxX; x++, x++)
            {
                for (var y = 0; y <= maxY; y++ )
                {
                    var position = new Position(x, y);
                    var expected = expectedOutputTiles[0].GetSpaceAtPosition(position);
                    var actual = output.GetSpaceAtPosition(position);

                    Assert.Equal(expected, actual);
                }
            }
        }
        
        [Fact]
        public void RotateLeftTest()
        {
            var input = @"Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###".Replace("|", Environment.NewLine);
            var expectedOutput = @"Tile 2311:|...#.##..#|#.#.###.##|....##.#.#|....#...#.|#.##.##...|.##.#....#|#..##.#..#|#..#...###|.#.####.#.|.#####..#.".Replace("|", Environment.NewLine);

            var tiles = TileParser.Parse(input);
            var expectedOutputTiles = TileParser.Parse(expectedOutput);

            var output = tiles[0].RotateLeft();
            
            var maxX = output.Spaces.Values.Max(s => s.Position.X);
            var maxY = output.Spaces.Values.Max(s => s.Position.Y);
            
            for (var x = 0; x <= maxX; x++, x++)
            {
                for (var y = 0; y <= maxY; y++ )
                {
                    var position = new Position(x, y);
                    var expected = expectedOutputTiles[0].GetSpaceAtPosition(position);
                    var actual = output.GetSpaceAtPosition(position);

                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}