using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.Linq;
using AdventOfCode2020.Day18;
=======
using AdventOfCode2020.Day18;
using AdventOfCode2020.Day18.Rules;
>>>>>>> Stashed changes
using Xunit;

namespace AdventOfCode2020.Tests.Day18
{
    public class Day18Tests
    {
        [Fact]
<<<<<<< Updated upstream
        public void ParserTests()
        {
            var tokens = LanguageParser.Parse("1+2");

            Assert.Equal(4, tokens.Length);
            Assert.Equal(Token.Integer("1"), tokens[0]);
            Assert.Equal(Token.Plus, tokens[1]);
            Assert.Equal(Token.Integer("2"), tokens[2]);
            Assert.Equal(Token.EndOf, tokens[3]);
        }
        
        [Fact]
        public void ParserTests_WithDoubleDigitNumbers()
        {
            var tokens = LanguageParser.Parse("10+200");

            Assert.Equal(4, tokens.Length);
            Assert.Equal(Token.Integer("10"), tokens[0]);
            Assert.Equal(Token.Plus, tokens[1]);
            Assert.Equal(Token.Integer("200"), tokens[2]);
            Assert.Equal(Token.EndOf, tokens[3]);
        }
        
        [Fact]
        public void ParserTests_WithSpaces()
        {
            var tokens = LanguageParser.Parse("1 + 2");

            Assert.Equal(4, tokens.Length);
            Assert.Equal(Token.Integer("1"), tokens[0]);
            Assert.Equal(Token.Plus, tokens[1]);
            Assert.Equal(Token.Integer("2"), tokens[2]);
            Assert.Equal(Token.EndOf, tokens[3]);
        }
        
        [Fact]
        public void ParserTests_WithMultiply()
        {
            var tokens = LanguageParser.Parse("1 * 2");

            Assert.Equal(4, tokens.Length);
            Assert.Equal(Token.Integer("1"), tokens[0]);
            Assert.Equal(Token.Multiply, tokens[1]);
            Assert.Equal(Token.Integer("2"), tokens[2]);
            Assert.Equal(Token.EndOf, tokens[3]);
        }
        
        [Fact]
        public void ParserTests_WithParens()
        {
            var tokens = LanguageParser.Parse("(1 * 2)");

            Assert.Equal(6, tokens.Length);
            Assert.Equal(Token.LeftParen, tokens[0]);
            Assert.Equal(Token.Integer("1"), tokens[1]);
            Assert.Equal(Token.Multiply, tokens[2]);
            Assert.Equal(Token.Integer("2"), tokens[3]);
            Assert.Equal(Token.RightParen, tokens[4]);
            Assert.Equal(Token.EndOf, tokens[5]);
        }
        
        [Fact]
        public void RunTests()
        {
            var tokens = LanguageParser.Parse("1+2");
            var result = new Interpreter(tokens).Run();
            Assert.Equal(3, result);
        }
        
        [InlineData("(1+2)", 3)]
        [InlineData("1+2+(2*3)", 9)]
        [InlineData("2 * 3 + (4 * 5)", 26)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        [Theory]
        public void RunTests_WithBraces(string input, int expectedOutput)
        {
            var tokens = LanguageParser.Parse(input);
            var result = new Interpreter(tokens).Run();
            Assert.Equal(expectedOutput, result);
=======
        public void LetterRuleTest()
        {
            var record = RulesParser.ParseRules("5: \"b\"");
            var rules = Assert.Single(record.Rules.Values);
            var letterRule = Assert.IsType<LetterRule>(rules);
            Assert.Equal("b", letterRule.Value);
        }
        
        [Fact]
        public void NumberRuleTest()
        {
            var record = RulesParser.ParseRules("5: 1 2 3");
            
            var first = Assert.IsType<NumberRule>(record.Rules["5"]);

            Assert.Equal("1", first.Values[0]);
            Assert.Equal("2", first.Values[1]);
            Assert.Equal("3", first.Values[2]);
        }
        
        [Fact]
        public void ConditionalRuleTest()
        {
            var record = RulesParser.ParseRules("5: 1 2 3 | 4 5 6");
            
            var conditional = Assert.IsType<ConditionalRule>(record.Rules["5"]);

            var first = Assert.IsType<NumberRule>(conditional.Values[0]);
            
            Assert.Equal("1", first.Values[0]);
            Assert.Equal("2", first.Values[1]);
            Assert.Equal("3", first.Values[2]);
            
            first = Assert.IsType<NumberRule>(conditional.Values[1]);
            
            Assert.Equal("4", first.Values[0]);
            Assert.Equal("5", first.Values[1]);
            Assert.Equal("6", first.Values[2]);
        }

        [Fact]
        public void SimpleMessageTest()
        {
            var record = RulesParser.ParseRules("5: \"b\"");
            int messageCount = record.CountValidMessages("5", new List<string> {"b"});
        }

        [Fact]
        public void Puzzle1Example()
        {
            var input = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""";
            var record = RulesParser.ParseRules(input);
            var messages = @"ababbb|bababa|abbbab|aaabbb|aaaabbb".Split("|");
            var count = record.CountValidMessages("0", messages);
            Assert.Equal(2, count);
>>>>>>> Stashed changes
        }

        [Fact]
        public void Puzzle1()
        {
<<<<<<< Updated upstream
            var results = new List<long>();
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day18.PuzzleInput.txt");
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                var tokens = LanguageParser.Parse(line);
                results.Add(new Interpreter(tokens).Run());
            }

            Assert.Equal(2743012121210, results.Sum());
        }
        
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
        [InlineData("2 * 3 + (4 * 5)", 46)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        [Theory]
        public void RunPrecedenceTests_WithBraces(string input, int expectedOutput)
        {
            var tokens = LanguageParser.Parse(input);
            var result = new InterpreterWithPrecedence(tokens)
                .Run();
            
            Assert.Equal(expectedOutput, result);
=======
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day18.PuzzleInput.txt");
            var pieces = input.Split(Environment.NewLine + Environment.NewLine);
            var record = RulesParser.ParseRules(pieces[0]);
            var count = record.CountValidMessages("0", pieces[1].Split(Environment.NewLine));
            Assert.Equal(279, count);
        }
        
        [Fact]
        public void Puzzle2Example()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day18.Puzzle2Example.txt");
            var pieces = input.Split(Environment.NewLine + Environment.NewLine);
            var record = RulesParser.ParseRules(pieces[0]);
            var count = record.CountValidMessages("0", pieces[1].Split(Environment.NewLine));
            Assert.Equal(12, count);
>>>>>>> Stashed changes
        }
        
        [Fact]
        public void Puzzle2()
        {
<<<<<<< Updated upstream
            var results = new List<long>();
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day18.PuzzleInput.txt");
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                var tokens = LanguageParser.Parse(line);
                results.Add(new InterpreterWithPrecedence(tokens)
                        .Run());
            }

            Assert.Equal(65658760783597, results.Sum());
=======
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day18.PuzzleInput2.txt");
            var pieces = input.Split(Environment.NewLine + Environment.NewLine);
            var record = RulesParser.ParseRules(pieces[0]);
            var count = record.CountValidMessages("0", pieces[1].Split(Environment.NewLine));
            Assert.Equal(384, count);
>>>>>>> Stashed changes
        }
    }
}