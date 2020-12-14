using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day14
{
    public static class BitMaskParser
    {
        private static readonly Dictionary<Regex, Func<GroupCollection, Operation>> Keywords = new()
        {
            {new Regex("mask = ([X|0|1]{36})") , MaskOperation.Parse},
            {new Regex("mem\\[([0-9]+)\\] = ([0-9]+)") , MemoryOperation.Parse},
        };

        public static Computer Parse(string input)
        {
            var computer = new Computer(new Memory());
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                foreach (var keyword in Keywords)
                {
                    var match = keyword.Key.Match(line);
                    if (!match.Success) 
                        continue;
                    
                    computer.Operations.Add(keyword.Value(match.Groups));
                    break;
                }
            }

            return computer;
        }
    }

    public record Computer(Memory Memory)
    {
        public string Mask { get; init; }
        
        public List<Operation> Operations { get; init; }
            = new();

        public Memory RunOperations()
        {
            return Operations.Aggregate(this, (current, operation) => operation.Run(current)).Memory;
        }
        
        public Memory RunOperations2()
        {
            Computer result = this;
            foreach (var operation in Operations) 
                result = operation.Run2(result);
            return result.Memory;
        }
    }

    public record MaskOperation(string MaskValue) : Operation
    {
        public static MaskOperation Parse(GroupCollection groupCollection) 
            => new(groupCollection[1].Value);

        public override Computer Run(Computer computer) 
            => computer with {Mask = MaskValue};

        public override Computer Run2(Computer computer) 
            => Run(computer);
    }

    public record MemoryOperation(int Address, long Value) : Operation
    {
        public static MemoryOperation Parse(GroupCollection groupCollection)
        {
            var address = int.Parse(groupCollection[1].Value);
            return new(address, long.Parse(groupCollection[2].Value));
        }
        
        public override Computer Run(Computer computer)
        {
            var memory = computer.Memory.AddValue(Address, GetValue(computer));
            return computer with {Memory = memory};
        }

        public override Computer Run2(Computer computer)
        {
            var values = GetValues(Address, computer.Mask);
            var memory = computer.Memory;
            foreach (var t in values)
                memory = computer.Memory.AddValue(t, Value);

            return computer with {Memory = memory};
        }
        
        private List<long> GetValues(long value, string mask)
        {
            var effectiveMask = "";
            var valueBinary = ConvertToBit(36, value);

            for (var i = 0; i < mask.Length; i++)
            {
                effectiveMask += mask[i] switch
                {                    
                    '0' => valueBinary[i],
                    _ => mask[i],
                };
            }

            var floatingIndices = effectiveMask
                .Select((v, index) => new { Value = v, Index = index })
                .Where(x => x.Value == 'X')
                .Select(x => x.Index)
                .ToList();

            if (!floatingIndices.Any())
                return new List<long> {Convert.ToInt64(valueBinary, 2)};

            var values = new List<long>();
            for(var i = 0; i < (int)Math.Pow(2, floatingIndices.Count); i++)
            {
                // Convert current variation count into binary
                var updateBits = ConvertToBit(floatingIndices.Count, i);
                var thing = effectiveMask.ToCharArray();

                for(var j = 0; j < floatingIndices.Count; j++)
                {
                    // replace the X's with this variations binary values
                    var floatingIndex = floatingIndices[j];
                    thing[floatingIndex] = updateBits[j];
                }

                values.Add(Convert.ToInt64(new string(thing), 2));
            }
            
            return values;
        }

        private string ConvertToBit(int length, long value)
        {
            var valueBinary = Convert.ToString(value, 2);
            if (valueBinary.Length >= length) 
                return valueBinary;
            
            for (var i = valueBinary.Length; i < length; i++)
            {
                valueBinary = "0" + valueBinary;
            }

            return valueBinary;
        }

        private long GetValue(Computer computer)
        {
            var effectiveMask = "";
            var computerMask = computer.Mask;
            var valueBinary = ConvertToBit(36, Value);
            
            for (var i = 0; i < computerMask.Length; i++)
            {
                effectiveMask += computerMask[i] switch
                {
                    'X' => valueBinary[i],
                    _ => computerMask[i]
                };
            }
            return Convert.ToInt64(effectiveMask, 2);
        }
    }

    public abstract record Operation
    {
        public abstract Computer Run(Computer computer);
        public abstract Computer Run2(Computer computer);
    }

    public record Memory
    {
        private Dictionary<long, long> _addresses = new();

        public Memory AddValue(long address, long value)
        {
            if (_addresses.ContainsKey(address))
            {
                _addresses[address] = value;
            }
            else
            {
                _addresses.Add(address, value);
            }

            return this with {_addresses = _addresses};
        }

        public IEnumerable<long> GetValues()
        {
            return _addresses.Select(address => address.Value);
        }
    }
}