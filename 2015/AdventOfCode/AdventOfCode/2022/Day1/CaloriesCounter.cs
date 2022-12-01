using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode._2022.Day1
{
    public static class CaloriesCounter
    {
        public static IEnumerable<long> Count(IEnumerable<string> lines)
        {
            return lines.Select(line => line.Split(Environment.NewLine).Sum(long.Parse));
        }
    }
}