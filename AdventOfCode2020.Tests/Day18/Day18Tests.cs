using AdventOfCode2020.Day18;
using Xunit;

namespace AdventOfCode2020.Tests.Day18
{
    public class Day18Tests
    {
        [Fact]
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
            var result = Interpreter.Run(tokens);
            Assert.Equal(3, result);
        }
    }
}