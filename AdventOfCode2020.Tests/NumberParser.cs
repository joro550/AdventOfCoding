using System.Linq;

namespace AdventOfCode2020.Tests
{
    public class NumberParser
    {
        public int[] Parse(string lines, string seperator) =>
            lines
                .Split(seperator)
                .Select(int.Parse)
                .ToArray();
    }
}