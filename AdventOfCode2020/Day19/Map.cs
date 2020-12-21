using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day19
{
    public record Map(Dictionary<string, Space> Tiles)
    {
        public Map(List<Space> spaces)
            :this(spaces.ToDictionary(space => space.GetUniqueKey()))
        {
        }
        
    }
}