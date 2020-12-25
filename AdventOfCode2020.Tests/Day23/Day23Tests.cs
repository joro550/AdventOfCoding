using System;
using System.Collections.Generic;
using System.Linq;
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
                crabCups.PlayRound(3);
            }

            var finalPosition = crabCups.GetState(5);
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

        [Fact]
        public void Speeeeed()
        {
            var cupList = new List<int>();
            for (var i = 0; i < 19000; i++) 
                cupList.Add(i);

            var crabCups = new CrabCups(cupList);

            for (var i = 0; i < 10000; i++)
            {
                crabCups.PlayRound(3);
            }

            var finalPosition = crabCups.GetState(2);
            var s = finalPosition.Aggregate(string.Empty, (current, value) => current + $", {value}");
            // Assert.Equal(", 2, 3, 4, 1, 7, 8, 5, 6, 9", s);
        }
        
        
        [Fact]
        public void Puzzle1()
        {
            var cups = CupParser.Parse("394618527");
            var crabCups = new CrabCups(cups);

            for (var i = 0; i < 100; i++)
            {
                crabCups.PlayRound(3);
            }

            var finalPosition = crabCups.GetState(2);
            var s = finalPosition.Aggregate(string.Empty, (current, value) => current + $", {value}");
            Assert.Equal(", 2, 3, 4, 1, 7, 8, 5, 6, 9", s);
        }

        [Fact]
        public void Puzzle2()
        {
            var cupList = new List<int> {3,9,4,6,1,8,5,2,7};
            for (var i = cupList.Max() + 1; i <= 1000000; i++) 
                cupList.Add(i);

            var crabCups = new CrabCups2(new LinkedCupCollection(cupList));
            for (var i = 0; i < 10000000; i++)
            {
                crabCups.PlayRound(3);
            }

            var finalPosition = crabCups.GetState(1);
            Assert.Equal(new[] {835237, 677192}, finalPosition[1..3]);
        }
    }
}