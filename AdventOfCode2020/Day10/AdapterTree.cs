using System.Collections.Generic;

namespace AdventOfCode2020.Day10
{
    public record AdapterTree
    {
        public Adapter Adapter { get; init; }
        public List<AdapterTree> PossibleAdapetes { get; set; }
    }
}