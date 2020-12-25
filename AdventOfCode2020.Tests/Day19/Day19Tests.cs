using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day19;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day19
{
    public class Day19Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day19Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TileParserTests()
        {
            var input = @"Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###".Replace("|", Environment.NewLine);
            var tiles = TileParser.Parse(input);
            var tile = Assert.Single(tiles);

            var space = tile.GetSpaceAtPosition(new Position(0, 0));
            Assert.False(space.IsActivated);
            
            space = tile.GetSpaceAtPosition(new Position(0, 1));
            Assert.True(space.IsActivated);
        }

        [Fact]
        public void FindMatches()
        {
            var input = @"Tile 2311:|..##.#..#.|##..#.....|#...##..#.|####.#...#|##.##.###.|##...#.###|.#.#.#..##|..#....#..|###...#.#.|..###..###".Replace("|", Environment.NewLine);
            var tiles = TileParser.Parse(input);

            var outerEdges = tiles[0].GetOuterEdges();

            var top = new List<bool> {false, false, true, true, false, true, false, false, true, false };
            var left = new List<bool> {false, true, true, true, true, true, false, false, true, false };
            var right = new List<bool> {false, false, false, true, false, true, true, false, false, true };
            var bottom = new List<bool> {false, false, true, true, true, false, false, true, true, true };

            Assert.Equal(top, outerEdges.Edges[Side.Top].Select(x=> x.IsActivated).ToList());
            Assert.Equal(left, outerEdges.Edges[Side.Left].Select(x=> x.IsActivated).ToList());
            Assert.Equal(right, outerEdges.Edges[Side.Right].Select(x=> x.IsActivated).ToList());
            Assert.Equal(bottom, outerEdges.Edges[Side.Bottom].Select(x=> x.IsActivated).ToList());
        }

        [Fact]
        public void Example1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day19.Puzzle1Example.txt");
            var tiles = TileParser.Parse(input);
            var matches = EdgeMatchFinder.FindMatchingEdges(tiles);

            var corners = matches.Where(x => x.Matches.Count == 2);
            Assert.Equal(20899048083289, corners.Aggregate(1L, (current, match) => current * match.Tile.Id));
        }

        [Fact]
        public void Puzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day19.PuzzleInput.txt");
            var tiles = TileParser.Parse(input);
            var matches = EdgeMatchFinder.FindMatchingEdges(tiles);

            var corners = matches.Where(x => x.Matches.Count == 2);
            Assert.Equal(12519494280967, corners.Aggregate(1L, (current, match) => current * match.Tile.Id));
        }

        [Fact]
        public void Puzzle2Example()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day19.Puzzle1Example.txt");
            var tiles = TileParser.Parse(input);
            var matches = EdgeMatchFinder.FindMatchingEdges(tiles);
            var map = MapBuilder.BuildMap(MapBuilder.PlaceMap(matches));

            var maxX = map.Tiles.Values.Max(x => x.Position.X);
            var maxY = map.Tiles.Values.Max(x => x.Position.Y);

            for (var y = 0; y <= maxY; y++)
            {
                var spaces = "";
                for (var x = 0; x <= maxX; x++)
                {
                    var position = new Position(x, y);

                    spaces += map.Tiles[position.GetUniqueKey()].GetString();

                }

                _testOutputHelper.WriteLine(spaces);
            }
        }

        [Fact]
        public void Puzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day19.PuzzleInput.txt");
            var tiles = TileParser.Parse(input);
            var matches = EdgeMatchFinder.FindMatchingEdges(tiles);
            var map = MapBuilder.BuildMap(matches);
            

            var corners = matches.Where(x => x.Matches.Count == 2);
            Assert.Equal(12519494280967, corners.Aggregate(1L, (current, match) => current * match.Tile.Id));
        }
    }
}