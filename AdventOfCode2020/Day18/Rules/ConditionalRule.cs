using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day18.Rules
{
    public record ConditionalRule(List<NumberRule> Values)
        : Rule
    {
        public static readonly Regex RuleRegex = new("\\|");

        public static ConditionalRule Parse(string name, string input)
        {
            var conditions = input.Split("|");
            var left = NumberRule.Parse(name, conditions[0]);
            var right = NumberRule.Parse(name, conditions[1]);
            return new ConditionalRule(new List<NumberRule> {left, right });
        }

        public override string GetString(Dictionary<string, Rule> dictionary)
        {
            return "(" + string.Join("|", Values.Select(v => v.GetString(dictionary))) + ")";
        }

        public override List<string> GetPossibleValues(Dictionary<string, Rule> dictionary)
        {
            var possibilities = new List<string>();

            foreach (var v in Values)
            {
                possibilities.AddRange(v.GetPossibleValues(dictionary));
            }

            return possibilities;
        }

        public override List<string> GetValues()
        {
            var list = new List<string>();
            foreach (var v in Values)
            {
                list.AddRange(v.GetValues());
            }

            return list;
        }
    }
}