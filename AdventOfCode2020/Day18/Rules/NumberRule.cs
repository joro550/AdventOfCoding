using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day18.Rules
{
    public record NumberRule(List<string> Values, bool WillLoop)
        : Rule
    {
        public static NumberRule Parse(string name, string input)
        {
            var willLoop = false;
            var list = new List<string>();
            foreach (var number in input.Split(" ").Where(c => !string.IsNullOrEmpty(c)))
            {
                if (name == number)
                {
                    willLoop = true;
                    continue;
                }

                list.Add(new (number.Trim()));
            }

            return new NumberRule(list, willLoop);
        }
        
        public override string GetString(Dictionary<string, Rule> dictionary)
        {
            return "(" + string.Join("", Values.Select(v => dictionary[v].GetString(dictionary))) + ")";
        }

        public override List<string> GetPossibleValues(Dictionary<string, Rule> dictionary)
        {
            var possibleValues = new List<string>{string.Empty};

            foreach (var v in Values)
            {
                var possibleRuleValues = dictionary[v].GetPossibleValues(dictionary);
                if (possibleRuleValues.Count == 1)
                {
                    for (var valueIndex = 0; valueIndex < possibleValues.Count; valueIndex++)
                    {
                        possibleValues[valueIndex] += possibleRuleValues[0];
                    }
                }
                else
                {
                    var tempList = new List<string>();
                    foreach (var ruleValue in possibleRuleValues)
                    {
                        tempList.AddRange(possibleValues.Select(possibleValue => ruleValue + possibleValue));
                    }

                    possibleValues = tempList;
                }
            }

            return possibleValues;
        }

        public override List<string> GetValues() 
            => Values;
    }
}