using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day18
{
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
}