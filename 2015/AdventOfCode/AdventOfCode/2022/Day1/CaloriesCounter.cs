using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode._2022.Day1
{
    public class CaloriesCounter
    {
        public static long[] Count(IEnumerable<string> lines)
        {

            return lines.Select(line => line.Split(Environment.NewLine).Sum(long.Parse)).ToArray();
        }
    }
}