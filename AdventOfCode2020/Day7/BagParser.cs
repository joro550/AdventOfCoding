using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day7
{
    public static class BagParser
    {
        public static List<Bag> Parse(string input)
        {
            var bagList = new List<Bag>();

            foreach (var line in input.Split(Environment.NewLine))
            {
                var parts = line.Split("contain");
                var bag = Bag.FromString(parts[0].Split(" "));

                foreach (var rules in parts[1].Split(","))
                {
                    var words = rules.Split(" ").Where(s => !string.IsNullOrEmpty(s))
                        .ToArray();

                    var didParse = int.TryParse(words[0], out var num);
                    var number = didParse ? num : 0;
                    var bagRule = Bag.FromString(words[1..]);

                    bag.BagRule.Add(new(number, bagRule));
                }

                bagList.Add(bag);
            }

            return bagList;
        }
    }

    public class Traverser
    {
        private readonly List<Bag> _bags;

        public Traverser(List<Bag> bags) 
            => _bags = bags;

        public int FindThing(string modifier, string colour) 
            => FindInBagRule(modifier, colour, _bags).Count;

        private HashSet<string> FindInBagRule(string modifier, string colour, IEnumerable<Bag> bags)
        {
            var hashSet = new HashSet<string>();
            foreach (var bag in bags)
            {
                var rule = _bags.SingleOrDefault(b => b.Modifier == bag.Modifier && b.Colour == bag.Colour);
                if(rule == null)
                    continue;
                
                var canHoldBag = rule.BagRule.Any(r => r.bag.Modifier == modifier && r.bag.Colour == colour);

                if (canHoldBag)
                {
                    hashSet.Add(rule.GetName());
                }
                else
                {
                    var bagRule = FindInBagRule(modifier, colour, bag.BagRule.Select(r => r.bag));
                    if(bagRule.Any())
                        hashSet.Add(bag.GetName());
                }
            }

            return hashSet;
        }
    }
    
    public record Bag(string Modifier, string Colour)
    {
        public readonly List<(int Number, Bag bag)> BagRule =
            new();

        public static Bag FromString(string[] input) 
            => new(input[0], input[1]);

        public string GetName()
            => $"{Modifier} {Colour}";
    }
}