using System;
using AdventOfCode2020.Day23;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day23
{
    public class Day23Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day23Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CrabCupsTest()
        {
            var cups = CupParser.Parse("389125467");
            var crabCups = new CrabCups(cups);

            for (var i = 0; i < 10; i++)
            {
                _testOutputHelper.WriteLine($"Iteration: {i}");
                crabCups.PlayRound();
            }

            var finalPosition = crabCups.GetState();
            Assert.Equal(5, finalPosition[0]);
            Assert.Equal(8, finalPosition[1]);
            Assert.Equal(3, finalPosition[2]);
            Assert.Equal(7, finalPosition[3]);
            Assert.Equal(4, finalPosition[4]);
            Assert.Equal(1, finalPosition[5]);
            Assert.Equal(9, finalPosition[6]);
            Assert.Equal(2, finalPosition[7]);
            Assert.Equal(6, finalPosition[8]);
        }
    }
}