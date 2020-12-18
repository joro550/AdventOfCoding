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
}