using Xunit;
using Xunit.Abstractions;
using AdventOfCode2020.Day23;
using System.Collections.Generic;

namespace AdventOfCode2020.Tests.Day23
{
    public class CapCupCollectionTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CapCupCollectionTests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Theory]
        [InlineData(new []{3, 8, 9, 1, 2, 5, 4, 6, 7}, new []{3, 2, 8, 9, 1, 5, 4, 6, 7})]
        [InlineData(new []{2, 8, 9, 1, 5, 4, 6, 7, 3}, new []{3, 2, 5, 4, 6, 7, 8, 9, 1})]
        [InlineData(new []{5, 4, 6, 7, 8, 9, 1, 3, 2}, new []{3, 4, 6, 7, 2, 5, 8, 9, 1})]
        public void GivenInput_ThenOutPutIsAsExpected(int[] cups, int [] expectedOutput)
        {
            var cupCollection = new LinkedCupCollection(cups);
            var crabCups = new CrabCups2(cupCollection);

            crabCups.PlayRound(3);

            var state = crabCups.GetState(expectedOutput[0]);
            Assert.Equal(expectedOutput, state);
        }

        [Fact]
        public void Example1()
        {
            var cupCollection = new LinkedCupCollection(new [] {3, 8, 9, 1, 2, 5, 4, 6, 7});
            var crabCups = new CrabCups2(cupCollection);
            for (var i = 0; i < 10; i++)
            {
                _testOutputHelper.WriteLine($"Iteration: {i}");
                crabCups.PlayRound(3);
            }

            var state = crabCups.GetState(5);
            Assert.Equal(new [] {5, 8, 3, 7, 4, 1, 9, 2, 6}, state);
        }
        
        [Fact]
        public void Puzzle1()
        {
            var cupCollection = new LinkedCupCollection(new [] {3, 9, 4, 6, 1, 8, 5, 2, 7});
            var crabCups = new CrabCups2(cupCollection);
            for (var i = 0; i < 100; i++)
            {
                _testOutputHelper.WriteLine($"Iteration: {i}");
                crabCups.PlayRound(3);
            }

            var state = crabCups.GetState(1);
            Assert.Equal(new [] {1, 7, 8, 5, 6, 9, 2, 3, 4}, state);
        }
        
        [Fact]
        public void Speeeeed()
        {
            var cupList = new List<int>();
            for (var i = 0; i < 1000000; i++) 
                cupList.Add(i);

            var crabCups = new CrabCups2(new LinkedCupCollection(cupList));

            for (var i = 0; i < 1000000; i++)
            {
                crabCups.PlayRound(3);
            }

            var finalPosition = crabCups.GetState(1);
        }
    }
}