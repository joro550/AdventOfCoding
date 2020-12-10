using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class AdapterPlugger : BaseAdapterPlugger
    {
        public static Dictionary<int, int> PlugAdaptersIn(Adapter source, Device device, List<Adapter> adapters)
        {
            var dictionary = new Dictionary<int, int>();

            var adapter = UseAdapter(source, adapters, dictionary);
            PlugDeviceIn(device, adapters, dictionary);

            while (adapters.Any())
            {
                adapter = UseAdapter(adapter, adapters, dictionary);
            }
            
            return dictionary;
        }

        private readonly Dictionary<int, AdapterTree> _cache = new();
        
        public AdapterTree CreateAdapterTree(Adapter source, Device device, IEnumerable<Adapter> adapters)
        {
            var enumerable1 = adapters as Adapter[] ?? adapters.ToArray();
            
            var possibleAdapters = FindAdaptersApplicableFor(source, enumerable1);
            var treeList = new List<AdapterTree>();

            if (_cache.ContainsKey(source.Jolt))
                return _cache[source.Jolt];

            var enumerable = possibleAdapters as Adapter[] ?? possibleAdapters.ToArray();
            if (!enumerable.Any())
            {
                treeList.Add(new AdapterTree
                {
                    Adapter = device,
                    PossibleAdapetes = null
                });
            }
            else
            {
                treeList = enumerable
                    .Select(adapter => CreateAdapterTree(adapter, device, enumerable1))
                    .ToList();
                
            }

            var adapterTree = new AdapterTree
            {
                Adapter = source, 
                PossibleAdapetes = treeList
            };
            
            _cache.Add(source.Jolt, adapterTree);

            return adapterTree;
        }
    }
}