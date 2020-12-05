using Xunit;
using System;
using System.Linq;
using Xunit.Abstractions;
using AdventOfCode._2015.Day2;

namespace AdventOfCode.Tests._2015.Day2
{
    public class Day2Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day2Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [InlineData("2x3x4", 52, 6)]
        [InlineData("1x1x10", 42, 1)]
        [Theory]
        public void GivenRectangle_SurfaceAreaIsAsExpected(string input, int expectedSurfaceArea, int expectedSlack)
        {
            var record = RecordParser.Parse(input);
            var (exactMeasurement, slack) = record.ConvertToWrappingPaper();

            Assert.Equal(expectedSlack, slack);
            Assert.Equal(expectedSurfaceArea, exactMeasurement);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = FileReader.GetResource("AdventOfCode.Tests._2015.Day2.PuzzleInput.txt");
            var sum = input.Split(Environment.NewLine)
                .Select(RecordParser.Parse)
                .Select(rect => rect.ConvertToWrappingPaper())
                .Select(wp => wp.Total())
                .Sum();
            
            _testOutputHelper.WriteLine(sum.ToString());
        }

        [InlineData("2x3x4", 10, 24)]
        [InlineData("1x1x10", 4, 10)]
        [Theory]
        public void GivenRectangle_RibbonLengthIsAsExpected(string input, int expectedRibbonLength, int expectedBowLength)
        {
            var record = RecordParser.Parse(input);
            var ribbon = record.ConvertToRibbon();

            Assert.Equal(expectedRibbonLength, ribbon.ExactLength);
            Assert.Equal(expectedBowLength, ribbon.BowLength);
            Assert.Equal(expectedRibbonLength + expectedBowLength, ribbon.Total());
        }

        [Fact]
        public void SolvePuzzle2()
        {
            var input = FileReader.GetResource("AdventOfCode.Tests._2015.Day2.PuzzleInput.txt");
            var sum = input.Split(Environment.NewLine);
                
                
                
            var t = sum.Select(RecordParser.Parse);
            var s = t.Select(rect => rect.ConvertToRibbon());
            var u = s.Select(r => r.Total());
            var v = u.Sum();
            
            _testOutputHelper.WriteLine(v.ToString());
        }
    }
}