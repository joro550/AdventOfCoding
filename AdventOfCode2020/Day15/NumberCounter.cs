using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    public class NumberCounter
    {
        private readonly Dictionary<int, int> _numbers = new();
        
        public long GetNumber(string numbers, int index)
        {
            var startingNumbers = numbers.Split(",").Select(int.Parse).ToArray();

            for (var i = 0; i < startingNumbers.Length - 1; i++) 
                _numbers.Add(startingNumbers[i], i + 1);

            var nextNumber = startingNumbers[^1];
            for (var i = startingNumbers.Length; i < index; i++)
            {
                var containsKey = _numbers.ContainsKey(nextNumber);
                if (containsKey)
                {
                    var previousNumber = nextNumber;
                    nextNumber = i - _numbers[previousNumber];
                    _numbers[previousNumber] = i;
                }
                else
                {
                    _numbers.Add(nextNumber, i);
                    nextNumber = 0;
                }
            }
            return nextNumber;
        }
    }
}