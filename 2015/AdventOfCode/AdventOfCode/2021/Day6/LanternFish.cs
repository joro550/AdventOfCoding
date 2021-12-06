using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day6
{
    public static class FishSimulator
    {
        public static long Simulate(string input, int days)
        {
            var originFish = LanternFish2.FromList(input
                .Split(",")
                .Select(int.Parse));

            for (int i = 0; i < days; i++)
            {
                originFish = originFish.AddDay();
            }

            return originFish.Sum();
        }
    }

    public class LanternFish2
    {
        private Dictionary<int, long> _fish;

        private LanternFish2(Dictionary<int, long> dictionary) 
            => _fish = dictionary;

        public LanternFish2 AddDay()
        {
            var dictionary = new Dictionary<int, long>();

            for (var i = 0; i < 8; i++) 
                dictionary.Add(i, _fish[i + 1]);

            dictionary[6] += _fish[0];
            dictionary.Add(8, _fish[0]);
            return new LanternFish2(dictionary);
        }

        public long Sum() 
            => _fish.Select(x => x.Value).Sum();

        public static LanternFish2 FromList(IEnumerable<int> fish)
        {
            var group = fish.GroupBy(x => x)
                .Select(x => new { x.Key, Count = x.Count() })
                .ToDictionary(x => x.Key);
            
            var dictionary = new Dictionary<int, long>();
            for (var i = 0; i < 9; i++) 
                dictionary.Add(i, group.ContainsKey(i) ? group[i].Count : 0);

            return  new LanternFish2(dictionary);;
        }
    }
}