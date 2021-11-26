using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode._2015.Day8;
using Xunit;

namespace AdventOfCode.Tests._2015.Day8
{
    public class StringTests
    {
        [Theory]
        [InlineData("", 2, 0)]
        [InlineData("aaa\"aaa", 10, 7)]
        public void TestLength(string value, int expectedCodeChars, int expectedStringChars)
        {
            var (codeCharacters, stringCharacters) = StringCounter.Count(value);
            Assert.Equal(expectedCodeChars, codeCharacters);
            Assert.Equal(expectedStringChars, stringCharacters);
        }

        [Fact]
        public void Puzzle1()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day8.PuzzleInput.txt");


            var sum = (from line in input.Split(Environment.NewLine)
                let u = Regex.Unescape(line.Substring(1, line.Length - 2))
                select line.Length - u.Length).Sum();

            Assert.Equal(1371, sum);
        }

        [Fact]
        public void Puzzle2()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day8.PuzzleInput.txt");

            var sum = (from line in input.Split('\n')
                let u = "\"" + line.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\""
                select u.Length - line.Length).Sum();
            Assert.Equal(2117, sum);
        }
    }
}