using System;
using System.Linq;
using AdventOfCode2020.Day17;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day17
{
    public class Day17Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day17Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void WhenAccessingCubeThatDoesntExist_ThenOneIsCreated()
        {
            const string input = @".#.|..#|###";
            var dimension = DimensionParser.Parse3D(input.Replace("|", Environment.NewLine));
            
            Assert.Equal(9, dimension.Cubes.Count);
        }

        [Fact]
        public void Example1()
        {
            const string input = @".#.|..#|###";
            var dimension = DimensionParser.Parse3D(input.Replace("|", Environment.NewLine));

            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            
            Assert.Equal(112, dimension.Cubes.Count(c => c.Active));
        }

        [Fact]
        public void Puzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day17.PuzzleInput.txt");

            var dimension = DimensionParser.Parse3D(input);
            for (int i = 0; i < 6; i++)
            {
                dimension = dimension.Simulate();
            }
            
            Assert.Equal(207, dimension.Cubes.Count(c => c.Active));
        }

        [Fact]
        public void RecordTest()
        {
            Position left = new FourDPosition(1, 1, 1, 0);
            Position right = new FourDPosition(1, 1, 1, 1);

            Assert.False(left == right);

        }

        [Fact]
        public void Example2()
        {
            const string input = @".#.|..#|###";
            var dimension = DimensionParser.Parse4D(input.Replace("|", Environment.NewLine));

            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            dimension = dimension.Simulate();
            
            Assert.Equal(848, dimension.Cubes.Count(c => c.Active));
        }
        
        [Fact]
        public void Puzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day17.PuzzleInput.txt");

            var dimension = DimensionParser.Parse4D(input);
            for (int i = 0; i < 6; i++)
            {
                dimension = dimension.Simulate();
            }
            
            Assert.Equal(207, dimension.Cubes.Count(c => c.Active));
        }
    }
}