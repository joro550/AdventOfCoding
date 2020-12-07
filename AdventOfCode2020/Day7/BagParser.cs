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
                
                var bagRule = FindInBagRule(modifier, colour, rule.BagRule.Select(r => r.bag));

                
                var canHoldBag = rule.BagRule.Any(r => r.bag.Modifier == modifier && r.bag.Colour == colour);

                if (canHoldBag)
                {
                    hashSet.Add(rule.GetName());
                }
                else
                {
                    if(bagRule.Any())
                        hashSet.Add(bag.GetName());
                }
            }

            return hashSet;
        }
    }
    
    public class BagCounter
    {
        private readonly List<Bag> _bags;

        public BagCounter(List<Bag> bags) 
            => _bags = bags;

        public int FindThing(string modifier, string colour)
        {
            var rule = _bags.SingleOrDefault(b => b.Modifier == modifier && b.Colour == colour);
            return GetBagCount(rule);
        }

        private int GetBagCount(Bag bag) =>
            _bags.SingleOrDefault(b => b.Modifier == bag.Modifier && b.Colour == bag.Colour)?
                .BagRule.Sum(rule => rule.Number + rule.Number * GetBagCount(rule.bag)) ?? 0;
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