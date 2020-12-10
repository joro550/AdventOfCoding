using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class Device : Adapter
    {
        public int JoltDifference => 3;
        
        public Device(IEnumerable<Adapter> adapters)
            : base(0)
        {
            Jolt = adapters.Select(a => a.Jolt).Max() + JoltDifference;
        }
    }
}