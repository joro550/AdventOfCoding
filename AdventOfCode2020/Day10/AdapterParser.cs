using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day10
{
    public static class AdapterParser
    {
        public static List<Adapter> GetAdapters(string input) =>
            input.Split(Environment.NewLine)
                .Select(line => new Adapter(int.Parse(line)))
                .ToList();
    }
}