using System;
using AdventOfCode._2015.Day3;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests._2015.Day3
{
    public class Day3Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day3Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void GivenOneRightDirection_ThenTwoPresentsAreGiven()
        {
            var houses = PresentParser.Parse(">");
            Assert.Equal(2, houses.Count);
        }
        
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v^v^v", 2)]
        [Theory]
        public void GivenInput_ExpectedAmountOfHousesAreVisited(string input, int expectedVisitedHouses)
        {
            var houses = PresentParser.Parse(input);
            Assert.Equal(expectedVisitedHouses, houses.Count);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = FileReader.GetResource("AdventOfCode.Tests._2015.Day3.PuzzleInput.txt");
            var houses = PresentParser.Parse(input);
            _testOutputHelper.WriteLine(houses.Count.ToString());
        }
        
        [InlineData("^v", 3)]
        [InlineData("^>v<", 3)]
        [InlineData("^v^v^v^v^v", 11)]
        [Theory]
        public void Puzzle2GivenInput_ExpectedAmountOfHousesAreVisited(string input, int expectedVisitedHouses)
        {
            var houses = PresentParser.ParsePuzzle2(input);
            Assert.Equal(expectedVisitedHouses, houses.Count);
        }

        [Fact]
        public void SolvePuzzle2()
        {
            var input = FileReader.GetResource("AdventOfCode.Tests._2015.Day3.PuzzleInput.txt");
            var houses = PresentParser.ParsePuzzle2(input);
            _testOutputHelper.WriteLine(houses.Count.ToString());
            
        }
    }
}