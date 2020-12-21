using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day18.Rules
{
    public record LetterRule(string Value) 
        : Rule
    {
        public static readonly Regex RuleRegex = new("[a-z]+");

        public static LetterRule Parse(string input)
        {
            var value = RuleRegex.Match(input);
            return new LetterRule(value.Value);
        }

        public override string GetString(Dictionary<string, Rule> dictionary) 
            => Value;

        public override List<string> GetPossibleValues(Dictionary<string, Rule> dictionary)
        {
            return new() {Value};
        }

        public override List<string> GetValues() 
            => new();
    }
}