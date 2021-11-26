using AdventOfCode._2015.Day9;
using Xunit;

namespace AdventOfCode.Tests._2015.Day9
{
    public class DistanceTests
    {
        [Fact]
        public void Thing()
        {
            const string input = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

            var distanceCal = new DistanceCalculator();
            var result = distanceCal.GetShortestDistance(input);
            Assert.Equal(605, result);
        }
    }
}