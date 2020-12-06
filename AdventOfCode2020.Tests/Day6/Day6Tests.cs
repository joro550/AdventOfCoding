using System;
using Xunit;
using System.Linq;
using AdventOfCode2020.Day6;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day6
{
    public class Day6Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day6Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void GivenAGroup_WithThreeUniqueAnswers_ThenCountIsThree()
        {
            const string input = "abc";
            var groups = AnswerParser.Parse(input);

            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            Assert.Equal(3, sum);
        }

        [Fact]
        public void GivenTwoGroupsOfThreeYesAnswers_ThenTotalIsSix()
        {
            const string input = @"abc

a
b
c";
            var groups = AnswerParser.Parse(input);

            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            Assert.Equal(6, sum);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day6.PuzzleInput.txt");
            var groups = AnswerParser.Parse(puzzleInput);
            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            _testOutputHelper.WriteLine(sum.ToString());
        }
        
        [Fact]
        public void Puzzle2_GivenAGroup_WithThreeUniqueAnswers_ThenCountIsThree()
        {
            const string input = "abc";
            var groups = AnswerParser.Parse(input);

            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Puzzle2_GivenTwoGroupsOfThreeYesAnswers_ThenTotalIsThree()
        {
            const string input = @"abc

a
b
c";
            var groups = AnswerParser.ParsePuzzle2(input);

            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            Assert.Equal(3, sum);
        }

        [Fact]
        public void Puzzle2_ExampleInput()
        {
            const string input = @"abc

a
b
c

ab
ac

a
a
a
a

b";
            var groups = AnswerParser.ParsePuzzle2(input);

            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            Assert.Equal(6, sum);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day6.PuzzleInput.txt");
            var groups = AnswerParser.ParsePuzzle2(puzzleInput);
            var sum = groups.Select(g => g.GetGroupAnswerCount())
                .Sum();
            _testOutputHelper.WriteLine(sum.ToString());
        }
    }
}