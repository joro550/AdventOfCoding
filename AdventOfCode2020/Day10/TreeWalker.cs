using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class TreeWalker
    {
        private readonly Dictionary<int, long> _cache
            = new();
        
        public long CountType<T>(AdapterTree tree)
        {
            var adapter = tree.Adapter;
            if (_cache.ContainsKey(adapter.Jolt))
                return _cache[adapter.Jolt];

            var count = 0L;
            if (tree.PossibleAdapetes != null)
                count += tree.PossibleAdapetes.Sum(CountType<T>);
            else
                count += 1;

            _cache[adapter.Jolt] = count;
            return count;
        }
    }
}