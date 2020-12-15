using AdventOfCode2020.Day15;
using Xunit;

namespace AdventOfCode2020.Tests.Day15
{
    public class Day15Tests
    {
        [Fact]
        public void Example1()
        {
            var counter = new NumberCounter();
            var number = counter.GetNumber("0,3,6", 10);
            Assert.Equal(0, number);
            
            counter = new NumberCounter();
            number = counter.GetNumber("0,3,6", 2020);            
            Assert.Equal(436, number);
        }

        [Theory]
        [InlineData("1,3,2", 1)]
        [InlineData("2,1,3", 10)]
        [InlineData("1,2,3", 27)]
        [InlineData("2,3,1", 78)]
        [InlineData("3,2,1", 438)]
        [InlineData("3,1,2", 1836)]
        public void FirstPuzzleExamples(string input, long expectedOutput)
        {
            var counter = new NumberCounter();
            var number = counter.GetNumber(input, 2020);
            Assert.Equal(expectedOutput, number);
        }
    }
}