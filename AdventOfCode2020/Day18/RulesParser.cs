using System;
using System.Collections.Generic;
using AdventOfCode2020.Day18.Rules;

namespace AdventOfCode2020.Day18
{
    public static class RulesParser
    {
        public static RuleRecord ParseRules(string input)
        {
            var rules = new Dictionary<string, Rule>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var split = line.Split(":");
                rules.Add(split[0], RuleRecord.ParseRule(split[0], split[1]));
            }

            return new RuleRecord(rules);
        }
    }
}