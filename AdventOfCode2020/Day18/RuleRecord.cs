using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2020.Day18.Rules;

namespace AdventOfCode2020.Day18
{
    public record RuleRecord(Dictionary<string, Rule> Rules)
    {
        public static Rule ParseRule(string name, string rule)
        {
            if (ConditionalRule.RuleRegex.IsMatch(rule))
            {
                return ConditionalRule.Parse(name, rule);
            }

            return LetterRule.RuleRegex.IsMatch(rule) 
                ? LetterRule.Parse(rule) 
                : NumberRule.Parse(name, rule);
        }

        public int CountValidMessages(string ruleName, IEnumerable<string> messages)
        {
            var value = new Regex($"^{CompileRules(ruleName)}$");
            return messages.Count(message => value.IsMatch(message));
        }
        
        public int CountValidMessages2(string ruleName, IEnumerable<string> messages)
        {
            var possibilities = Rules[ruleName].GetPossibleValues(Rules);
            return messages.Count(message => possibilities.Any(possibility => message == possibility));
        }

        private string CompileRules(string ruleName) 
            => Rules[ruleName].GetString(Rules);
    }
}