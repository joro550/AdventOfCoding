using System;
using System.Linq;
using AdventOfCode._2015.Day5;
using Xunit;

namespace AdventOfCode.Tests._2015.Day5
{
    public class LetterEvalRuleTests
    {
        [Fact]
        public void WhenWordDoesntMatchRules_FalseIsReturned()
        {
            var rule = new LetterEvalRule(new[] { 'a', 'e', 'i', 'o', 'u' }, 3);
            var isValid = rule.Evaluate("bad");
            Assert.False(isValid);
        } 
        
        [Fact]
        public void WhenWordMatchesRules_TrueIsReturned()
        {
            var rule = new LetterEvalRule(new[] { 'a', 'e', 'i', 'o', 'u' }, 3);
            var isValid = rule.Evaluate("aei");
            Assert.True(isValid);
        } 
    }
    
    public class LetterDuplicationRuleTests
    {
        [Fact]
        public void WhenWordDoesntMatchRules_FalseIsReturned()
        {
            var rule = new LetterDuplicationRule(2, 1);
            var isValid = rule.Evaluate("bad");
            Assert.False(isValid);
        }
        
        [Theory]
        [InlineData("baad")]
        [InlineData("badd")]
        [InlineData("bbad")]
        public void WhenWordDoesMatchRules_TrueIsReturned(string word)
        {
            var rule = new LetterDuplicationRule(2, 1);
            var isValid = rule.Evaluate(word);
            Assert.True(isValid);
        } 
    }

    public class WordExclusionTests
    {
        [Fact]
        public void WhenWordDoesntMatchRules_FalseIsReturned()
        {
            var rule = new WordExclusion("aa");
            var isValid = rule.Evaluate("baad");
            Assert.False(isValid);
        }
        
        [Fact]
        public void WhenWordDoesMatchRules_TrueIsReturned()
        {
            var rule = new WordExclusion("aa");
            var isValid = rule.Evaluate("bad");
            Assert.True(isValid);
        }
    }

    public class RepeatingLetterRuleTests
    {
        [Theory]
        [InlineData("xyx")]
        [InlineData("abcdefeghi")]
        [InlineData("efe")]
        public void WhenWordDoesMatchRule_TrueIsReturned(string word)
        {
            var rule = new RepeatingLetterRule();
            var isValid = rule.Evaluate(word);
            Assert.True(isValid);
        }
        
    }

    public class Puzzle1Tests
    {
        [Theory]
        [InlineData("ugknbfddgicrmopn", true)]
        [InlineData("aaa", true)]
        [InlineData("jchzalrnumimnmhp", false)]
        [InlineData("haegwjzuvuyypxyu", false)]
        [InlineData("dvszwmarrgswjxmb", false)]
        public void Example1(string word, bool expectedResult)
        {
            var evaluator = new WordEvaluator()
            {
                Rules =
                {
                    new LetterEvalRule(new[] { 'a', 'e', 'i', 'o', 'u' }, 3),
                    new LetterDuplicationRule(2, 1),
                    new WordExclusion("ab"),
                    new WordExclusion("cd"),
                    new WordExclusion("pq"),
                    new WordExclusion("xy"),
                }
            };

            var isNice = evaluator.IsNiceWord(word);
            Assert.Equal(expectedResult, isNice);
        }

        [Fact]
        public void Puzzle1()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day5.PuzzleInput.txt");

            var evaluator = new WordEvaluator
            {
                Rules =
                {
                    new LetterEvalRule(new[] { 'a', 'e', 'i', 'o', 'u' }, 3),
                    new LetterDuplicationRule(2, 1),
                    new WordExclusion("ab"),
                    new WordExclusion("cd"),
                    new WordExclusion("pq"),
                    new WordExclusion("xy"),
                }
            };

            var count = input.Split(Environment.NewLine)
                .Count(word => evaluator.IsNiceWord(word));
            Assert.Equal(258, count);
        }
        
        
        [Theory]
        [InlineData("qjhvhtzxzqqjkmpb", true)]
        [InlineData("xxyxx", true)]
        [InlineData("uurcxstgmygtbstg", false)]
        [InlineData("dvszwmarrgswjxmb", false)]
        public void Example2(string word, bool expectedResult)
        {
            var evaluator = new WordEvaluator()
            {
                Rules =
                {
                    new LetterDuplicationRule(2, 1),
                    new RepeatingLetterRule()
                }
            };

            var isNice = evaluator.IsNiceWord(word);
            Assert.Equal(expectedResult, isNice);
        }

        [Fact]
        public void Puzzle2()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day5.PuzzleInput.txt");

            var evaluator = new WordEvaluator
            {
                Rules =
                {
                    new LetterDuplicationRule(2, 1),
                    new RepeatingLetterRule()
                }
            };

            var count = input.Split(Environment.NewLine)
                .Count(word => evaluator.IsNiceWord(word));
            Assert.Equal(258, count);
        }
    }
}