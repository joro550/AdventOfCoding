using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day18
{
    public static class LanguageParser
    {
        public static Token[] Parse(string s)
        {
            var script = new Script(s);
            var tokens = new List<Token>();

            string nextCharacter;
            
            while ((nextCharacter = script.GetNext()) != null)
            {
                if (string.IsNullOrWhiteSpace(nextCharacter))
                    continue;
                
                // Get the full number
                if (IsNumeric(nextCharacter)) 
                    nextCharacter += GetFullNumber(script);

                tokens.Add(HandleToken(nextCharacter));
            }

            tokens.Add(Token.EndOf);
            return tokens.ToArray();
        }

        private static string GetFullNumber(Script script)
        {
            var numberChar = string.Empty;
            while (IsNumeric(script.PeekNext())) 
                numberChar += script.GetNext();
            return numberChar;
        }

        private static Token HandleToken(string character)
        {
            return character switch
            {
                "+" => Token.Plus,
                "*" => Token.Multiply,
                "(" => Token.LeftParen,
                ")" => Token.RightParen,
                _ => Token.Integer(character)
            };
        }

        private static bool IsNumeric(string? character)
        {
            if (string.IsNullOrWhiteSpace(character))
                return false;
            
            var isNumber = int.TryParse(character, out _);
            return isNumber;
        }
    }

    public static class Interpreter
    {
        public static long Run(Token[] tokens)
        {
            var total = int.Parse(tokens[0].Value);

            var currentIndex = 1;

            while (currentIndex < tokens.Length)
            {
                if (tokens[currentIndex] == Token.EndOf)
                    break;
                
                if (tokens[currentIndex] == Token.Plus)
                {
                    currentIndex++;
                    total += int.Parse(tokens[currentIndex].Value);
                    currentIndex++;
                }
                else if(tokens[currentIndex] == Token.Multiply)
                {
                    currentIndex++;
                    total *= int.Parse(tokens[currentIndex].Value);
                    currentIndex++;
                }
            }
            
            return total;
        }
    }
    
    public record Script
    {
        private readonly Queue<string> _queue;
        
        public Script(string input) 
            => _queue = new Queue<string>(input.Select(s => s.ToString()));

        public string? GetNext()
        {
            var canDequeue = _queue.TryDequeue(out var result);
            return canDequeue ? result : null;
        }
        
        public string? PeekNext()
        {
            var canDequeue = _queue.TryPeek(out var result);
            return canDequeue ? result : null;
        }
    }
    
    

    public enum TokenType
    {
        LEFT_PAREN,
        RIGHT_PAREN,
        INTEGER,
        PLUS,
        MULTIPLY,
        EOF
    }

    public record Token(TokenType Type, string Value)
    {
        public static Token Plus => new(TokenType.PLUS, "+");
        public static Token Multiply => new(TokenType.MULTIPLY, "*");
        public static Token LeftParen => new(TokenType.LEFT_PAREN, "(");
        public static Token RightParen => new(TokenType.RIGHT_PAREN, ")");
        public static Token EndOf => new(TokenType.EOF, string.Empty);
        
        public static Token Integer(string character) 
            => new(TokenType.INTEGER, character);
        
        public virtual string GetValue()
        {
            return Value;
        }
    }
}