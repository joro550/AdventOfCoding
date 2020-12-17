using System;
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
        public void GivenInput_ThenDimensionInitialStateIsAsExpected()
        {
            const string input = @".#.|..#|###";
            var dimension = DimensionParser.Parse(input.Replace("|", Environment.NewLine));

            Assert.False(dimension[0, 0, 0].Active);
            Assert.True(dimension[1, 0, 0].Active);
            Assert.False(dimension[2, 0, 0].Active);
            
            Assert.False(dimension[0, 1, 0].Active);
            Assert.False(dimension[1, 1, 0].Active);
            Assert.True(dimension[2, 1, 0].Active);
            
            Assert.True(dimension[0, 2, 0].Active);
            Assert.True(dimension[1, 2, 0].Active);
            Assert.True(dimension[2, 2, 0].Active);
        }

        [Fact]
        public void WhenAccessingCubeThatDoesntExist_ThenOneIsCreated()
        {
            const string input = @".#.|..#|###";
            var dimension = DimensionParser.Parse(input.Replace("|", Environment.NewLine));
            
            Assert.False(dimension[0, 0, 1].Active);
            Assert.Equal(10, dimension.Cubes.Count);
        }

        [Fact]
        public void Example1()
        {
            const string input = @".#.|..#|###";
            var dimension = DimensionParser.Parse(input.Replace("|", Environment.NewLine));

            dimension = dimension.Simulate();
            _testOutputHelper.WriteLine(dimension.PrintToString(new []{ -1, 0, 1}));
        }
    }
}