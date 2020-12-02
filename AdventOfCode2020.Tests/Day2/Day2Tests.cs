using System;
using Xunit;
using Xunit.Abstractions;
using AdventOfCode2020.Day2;

namespace AdventOfCode2020.Tests.Day2
{
    public class Day2Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day2Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;
        
        [Fact]
        public void GivenPolicyLineThenPolicyGetPopulatedAsExpected()
        {
            const string policyString = "1-3 a";
            var (min, max, character) = PolicyParser.ParsePolicy(policyString);
            
            Assert.Equal(1, min);
            Assert.Equal(3, max);
            Assert.Equal("a", character);
        }

        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        [Theory]
        public void GivenPolicy_ThenPasswordValidIsAsExpected(string policyString, bool expectedValidValue)
        {
            var (policy, password) = LineParser.ParseLine(policyString);

            var isValid = policy.IsPasswordValid(password);
            Assert.Equal(expectedValidValue, isValid);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day2.PuzzleInput.txt");

            var count = 0;
            var policyLines = exampleInput.Split(Environment.NewLine);
            
            foreach (var line in policyLines)
            {
                var (policy, password) = LineParser.ParseLine(line);
                var isValid = policy.IsPasswordValid(password);
                if (isValid)
                    count++;
            }

            _testOutputHelper.WriteLine(count.ToString());
        }
        
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        [Theory]
        public void GivenTobogganPolicy_ThenPasswordValidIsAsExpected(string policyString, bool expectedValidValue)
        {
            var (policy, password) = LineParser.ParseTobogganLine(policyString);

            var isValid = policy.IsPasswordValid(password);
            Assert.Equal(expectedValidValue, isValid);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day2.PuzzleInput.txt");

            var count = 0;
            var policyLines = exampleInput.Split(Environment.NewLine);
            
            foreach (var line in policyLines)
            {
                var (policy, password) = LineParser.ParseTobogganLine(line);
                var isValid = policy.IsPasswordValid(password);
                if (isValid)
                    count++;
            }

            _testOutputHelper.WriteLine(count.ToString());
        }
    }
}