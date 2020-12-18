using System.Collections.Generic;

namespace AdventOfCode2020.Day18
{
    public record TokenQueue
    {
        private readonly Queue<Token> _queue;
        
        public TokenQueue(Token[] input) 
            => _queue = new Queue<Token>(input);

        public Token? GetNext()
        {
            var canDequeue = _queue.TryDequeue(out var result);
            return canDequeue ? result : null;
        }
        
        public Token? PeekNext()
        {
            var canDequeue = _queue.TryPeek(out var result);
            return canDequeue ? result : null;
        }
    }
}