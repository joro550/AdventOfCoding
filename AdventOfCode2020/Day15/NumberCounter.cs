using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    public class NumberCounter
    {
        public readonly Dictionary<int, int> _numbers = new();
        
        
        private readonly HashSet<(int Number, int Index)> _hashSet
            = new();

        public long GetNumber(string numbers, int index)
        {
            var startingNumbers = numbers.Split(",").Select(int.Parse).ToArray();

            for (var i = 0; i < startingNumbers.Length - 1; i++) 
                _numbers.Add(startingNumbers[i], i + 1);            

            var lastNumberSpoken = startingNumbers[^1];
            for (var i = startingNumbers.Length; i < index; i++)
            {
                var containsKey = _numbers.ContainsKey(lastNumberSpoken);
                if (containsKey)
                {
                    lastNumberSpoken = i - _numbers[lastNumberSpoken];
                    _numbers[lastNumberSpoken] = i + 1;
                }
                else
                {
                    _numbers.Add(lastNumberSpoken, i);
                    lastNumberSpoken = 0;
                }
            }

            return lastNumberSpoken;
        }
    }
}