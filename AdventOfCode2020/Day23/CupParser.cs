using System.Linq;

namespace AdventOfCode2020.Day23
{
    public static class CupParser
    {
        public static CupCollection Parse(string input) 
            => new(input.Select(c => long.Parse(c.ToString())));
    }
}