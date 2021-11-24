using System.Collections.Generic;

namespace AdventOfCode._2015.Day7
{
    public class TokenQueue
    {
        private readonly Queue<Token> _tokenQueue;
        private Token _token;

        public TokenQueue(IEnumerable<Token> tokens) 
            => _tokenQueue = new Queue<Token>(tokens);

        public Token GetNext()
        {
            if (!_tokenQueue.TryDequeue(out var token))
                return new Token(TokenType.Unknown, string.Empty);
            
            _token = token;
            return _token;
        }

        public Token GetCurrent() => _token;

        public Token PeekNext() 
            => _tokenQueue.TryPeek(out var token) ? token : new Token(TokenType.Unknown, string.Empty);
    }
}