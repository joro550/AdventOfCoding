using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day8
{
    public static class LanguageParser
    {
        private static readonly Dictionary<Regex, Func<string, int, Operation>> Keywords =
            new()
            {
                {new Regex("nop (-|\\+)([0-9]+)"), (pm, n) => new NoOperation(pm, n)},
                {new Regex("jmp (-|\\+)([0-9]+)"), (pm, n) => new JumpOperation(pm, n)},
                {new Regex("acc (-|\\+)([0-9]+)"), (pm, n) => new AccumulateOperation(pm, n)}
            };
        
        public static DebugProgram Parse(string input)
        {
            var operations = (from line in input.Split(Environment.NewLine)
                from keyword in Keywords
                let match = keyword.Key.Match(line)
                where match.Success
                let plusMinus = match.Groups[1].Value
                let number = int.Parse(match.Groups[2].Value)
                select keyword.Value(plusMinus, number)).ToList();

            return new DebugProgram(operations);
        }
    }
}