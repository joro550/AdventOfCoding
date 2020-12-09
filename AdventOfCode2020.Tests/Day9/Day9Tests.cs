using System;
using AdventOfCode2020.Day9;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day9
{
    public class Day9Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day9Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void GivenExampleInput_ThenExpectedNumberIsNotASumOfPreviousNumbers()
        {
            var xmas = XmasParser.Parse(5, @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576");
            var i = 0;
            while (xmas.Check(i)) 
                i++;

            Assert.Equal(127, xmas.GetNumberAtIndex(i));

        }

        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day9.PuzzleInput.txt");
            var xmas = XmasParser.Parse(25, puzzleInput);
            
            var i = 0;
            while (xmas.Check(i)) 
                i++;
            
            Assert.Equal(1212510616,xmas.GetNumberAtIndex(i) );
        }

        [Fact]
        public void Example2()
        {
            var xmas = XmasParser.Parse(5, @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576");
            var sum = xmas.FindContiguomSumFor(127);
            Assert.Equal(62, sum);

        }
        
        
        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day9.PuzzleInput.txt");
            var xmas = XmasParser.Parse(25, puzzleInput);
            
            var sum = xmas.FindContiguomSumFor(1212510616);
            Assert.Equal(171265123, sum);
        }
    }
}