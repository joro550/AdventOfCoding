using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2015.Day7
{
    public static class Tokenizer
    {
        public static IEnumerable<Token> Tokenize(string script)
        {
            var tokens = new List<Token>();
            foreach (var line in script.Split(Environment.NewLine))
            {
                tokens.AddRange(line.Split(" ").Select(ToToken));
                tokens.Add(new Token(TokenType.EndOfLine, string.Empty));
            }

            tokens.Add(new Token(TokenType.EndOfScript, string.Empty));
            return tokens;
        }

        private static Token ToToken(string word) =>
            word switch
            {
                "->" => Token.Assign(),
                "AND" => new Token(TokenType.And, string.Empty),
                "OR" => new Token(TokenType.Or, string.Empty),
                "LSHIFT" => new Token(TokenType.LShift, string.Empty),
                "RSHIFT" => new Token(TokenType.RShift, string.Empty),
                "NOT" => new Token(TokenType.Not, string.Empty),
                _ => int.TryParse(word, out var value)
                    ? new Token(TokenType.Number, value.ToString())
                    : new Token(TokenType.WireName, word)
            };
    }
}