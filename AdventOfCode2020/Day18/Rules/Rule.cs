using System.Collections.Generic;

namespace AdventOfCode2020.Day18.Rules
{
    public abstract record Rule
    {
        public abstract string GetString(Dictionary<string, Rule> dictionary);
        public abstract List<string> GetPossibleValues(Dictionary<string, Rule> dictionary);
        public abstract List<string> GetValues();
    }
}