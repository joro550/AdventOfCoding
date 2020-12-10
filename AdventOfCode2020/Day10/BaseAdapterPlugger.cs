using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class BaseAdapterPlugger
    {
        protected static Adapter UseAdapter(Adapter source, ICollection<Adapter> adapters, IDictionary<int, int> dictionary)
        {
            var adapter = FindAdapterApplicableFor(source, adapters);
            
            var joltDifference = adapter.Jolt - source.Jolt;
            if (!dictionary.ContainsKey(joltDifference))
            {
                dictionary.Add(joltDifference, 1);
            }
            else
            {
                dictionary[joltDifference]++;
            }

            adapters.Remove(adapter);
            return adapter;
        }
        
        protected static void PlugDeviceIn(Device source, IEnumerable<Adapter> adapters, IDictionary<int, int> dictionary)
        {
            var adapter = adapters.Single(a => a.Jolt == source.Jolt - source.JoltDifference);

            var joltDifference = source.Jolt - adapter.Jolt;
            if (!dictionary.ContainsKey(joltDifference))
            {
                dictionary.Add(joltDifference, 1);
            }
            else
            {
                dictionary[joltDifference]++;
            }
        }

        private static Adapter FindAdapterApplicableFor(Adapter adapter, IEnumerable<Adapter> adapters) =>
            adapters.Where(a => a.Jolt <= adapter.Jolt + 3)
                .Min();

        protected static IEnumerable<Adapter> FindAdaptersApplicableFor(Adapter adapter, IEnumerable<Adapter> adapters) =>
            adapters.Where(a => a.Jolt > adapter.Jolt && a.Jolt <= adapter.Jolt + 3);
    }
}