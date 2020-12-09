using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day9
{
    public static class XmasParser
    {
        public static Xmas Parse(int preambleLength, string input)
        {
            var numberList = input.Split(Environment.NewLine)
                .Select(long.Parse)
                .ToList();
            return new Xmas(numberList, preambleLength);
        }
    }

    public record Xmas
    {
        private readonly int _preambleLength;
        private readonly List<long> _numbers;

        public Xmas(IEnumerable<long> numbers, int preambleLength)
        {
            _preambleLength = preambleLength;
            _numbers = numbers.ToList();
        }

        public long GetNumberAtIndex(int index) 
            => _numbers[index + _preambleLength];

        public bool Check(int index)
        {
            var endOfRange = index + _preambleLength;
            var preAmble = new List<long>();
                
            for (var i = index; i < endOfRange; i++) 
                preAmble.Add(_numbers[i]);

            var numberToMatch = _numbers[index + _preambleLength];

            return preAmble.Where((t3, i) =>
                (from t2 in preAmble.Where((t2, j) => i != j)
                    let t = t3
                    let t1 = t2
                    where t + t1 == numberToMatch
                    select t).Any()).Any();
        }

        public long FindContiguomSumFor(long number)
        {
            for (var i = 0; i < _numbers.Count; i++)
            {
                var numbers = new List<long> {_numbers[i]};
                var result = Add(i, number, numbers);

                if (result != number) 
                    continue;
                
                var min = numbers.Min();
                var max = numbers.Max();
                return min + max;
            }

            return 0;
        }

        private long Add(int startingNumber, long aimingFor, ICollection<long> currentNumbers)
        {
            var index = startingNumber + 1;
            currentNumbers.Add(_numbers[index]);
            
            var sum = currentNumbers.Sum();
            if (sum == aimingFor)
            {
                return aimingFor;
            }

            return sum < aimingFor ? 
                Add(index, aimingFor, currentNumbers) 
                : 0;
        }
    }
}