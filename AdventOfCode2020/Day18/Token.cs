namespace AdventOfCode2020.Day18
{
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