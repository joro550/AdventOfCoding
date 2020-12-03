using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AdventOfCode2020.Day3;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day3
{
    public class Day3Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day3Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void LoadingTests()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day3.ExampleInput.txt");

            var map = Map.LoadMap(exampleInput);

            Assert.IsType<Space>(map.GetCoords(0, 0));
            Assert.IsType<Space>(map.GetCoords(0, 1));
            Assert.IsType<Tree>(map.GetCoords(0, 2));
        }
        
        [Theory]
        [InlineData("..#")]
        public void WhenYCoordinateOverflows_ThenSpacesRepeat(string mapString)
        {
            var map = Map.LoadMap(mapString);

            Assert.IsType<Space>(map.GetCoords(0, 0));
            Assert.IsType<Space>(map.GetCoords(0, 1));
            Assert.IsType<Tree>(map.GetCoords(0, 2));
            
            // overflow
            Assert.IsType<Space>(map.GetCoords(0, 3));
            Assert.IsType<Space>(map.GetCoords(0, 4));
            Assert.IsType<Tree>(map.GetCoords(0, 5));
        }
        
        [Theory]
        [InlineData("..#")]
        public void WhenXCoordinateOverflows_ThenFreedomSpaceIsReturned(string mapString)
        {
            var map = Map.LoadMap(mapString);

            Assert.IsType<Space>(map.GetCoords(0, 0));
            Assert.IsType<Freedom>(map.GetCoords(1, 0));
        }
        
        
        [Fact]
        public void LoadingExample()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day3.ExampleInput.txt");

            var map = Map.LoadMap(exampleInput);
            var navigation = new Navigation(new List<Move>
            {
                new XMove(), 
                new XMove(), 
                new XMove(), 
                new YMove()
            });
            
            var navigator = new Navigator(0, 0, map, navigation);

            while (navigator.GetSpaceOnMap() is not Freedom) 
                navigator.Navigate();
            
            var visitedSpaces = navigator.GetVisitedSpaces();
            var treeCount = visitedSpaces.Count(space => space is Tree);
            Assert.Equal(7, treeCount);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day3.PuzzleInput.txt");
            var map = Map.LoadMap(puzzleInput);

            var navigation = new Navigation(new List<Move>
            {
                new XMove(), 
                new XMove(), 
                new XMove(), 
                new YMove()
            });

            var navigator = new Navigator(0, 0, map, navigation);
            while (navigator.GetSpaceOnMap() is not Freedom) 
                navigator.Navigate();

            var visitedSpaces = navigator.GetVisitedSpaces();
            var treeCount = visitedSpaces.Count(space => space is Tree);
            _testOutputHelper.WriteLine(treeCount.ToString());
            Assert.Equal(178, treeCount);
        }

        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day3.PuzzleInput.txt");
            var map = Map.LoadMap(puzzleInput);

            var treeCounts = new[]
            {
                GetTreeCount(map, new XMove(), new YMove()),
                GetTreeCount(map, new XMove(), new XMove(), new XMove(), new YMove()),
                GetTreeCount(map, new XMove(), new XMove(), new XMove(), new XMove(), new XMove(), new YMove()),
                GetTreeCount(map, new XMove(), new XMove(), new XMove(), new XMove(), new XMove(), new XMove(),
                    new XMove(), new YMove()),
                GetTreeCount(map, new XMove(), new YMove(), new YMove()),
            };
            
            var treeCount = treeCounts.Aggregate(1L, (prod, next) => prod * next);
            _testOutputHelper.WriteLine(treeCount.ToString());
            Assert.Equal(3492520200, treeCount);
        }

        private static int GetTreeCount(Map map, params Move[] moves)
        {
            var navigation = new Navigation(new List<Move>(moves));
            var navigator = new Navigator(0, 0, map, navigation);
            while (navigator.GetSpaceOnMap() is not Freedom) 
                navigator.Navigate();
            
            var visitedSpaces = navigator.GetVisitedSpaces();
            return visitedSpaces.Count(space => space is Tree);
        }
    }
}