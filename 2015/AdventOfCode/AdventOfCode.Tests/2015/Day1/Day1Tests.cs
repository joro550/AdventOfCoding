using System;
using AdventOfCode._2015.Day1;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests._2015.Day1
{
    public class Day1Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day1Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void WhenGivenAOpenParenthesis_ThenOneIsAddedToFloorCount()
        {
            const string input = "(";
            var floor = FloorParser.Parse(input);
            Assert.Equal(1, floor.FloorNumber);
        }
        
        [Fact]
        public void WhenGivenAClosedParenthesis_ThenOneIsSubtractedToFloorCount()
        {
            const string input = ")";
            var floor = FloorParser.Parse(input);
            Assert.Equal(-1, floor.FloorNumber);
        }

        [InlineData("(())", 0)]
        [InlineData("()()", 0)]
        [InlineData("(((", 3)]
        [InlineData("(()(()(", 3)]
        [InlineData("))(((((", 3)]
        [InlineData("())", -1)]
        [InlineData("))(", -1)]
        [InlineData(")))", -3)]
        [InlineData(")())())", -3)]
        [Theory]
        public void WhenGivenAnInput_ExpectedFloorCountIsReturned(string input, int expectedFloorCount)
        {
            var floor = FloorParser.Parse(input);
            Assert.Equal(expectedFloorCount, floor.FloorNumber);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = FileReader.GetResource("AdventOfCode.Tests._2015.Day1.PuzzleInput.txt");
            var floor = FloorParser.Parse(input);

            _testOutputHelper.WriteLine(floor.ToString());
        }

        [InlineData(")", 1)]
        [InlineData("()())", 5)]
        [Theory]
        public void WhenGivenAnInput_ExpectedBasementPositionIsReturned(string input, int basementPosition)
        {
            var floor = FloorParser.ParsePuzzle2(input);
            Assert.Equal(basementPosition, floor.BasementAtPosition);
        }
        
        
        [Fact]
        public void SolvePuzzle2()
        {
            var input = FileReader.GetResource("AdventOfCode.Tests._2015.Day1.PuzzleInput.txt");
            var floor = FloorParser.ParsePuzzle2(input);

            _testOutputHelper.WriteLine(floor.ToString());
        }
    }
}