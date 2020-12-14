using Xunit;
using System;
using System.Linq;
using AdventOfCode2020.Day14;

namespace AdventOfCode2020.Tests.Day14
{
    public class Day14Tests
    {
        [Fact]
        public void BitMask()
        {
            const string maskString = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X";
            const string value =      "000000000000000000000000000000001011";

            var effectiveMask = "";
            for (var i = 0; i < maskString.Length; i++)
            {
                effectiveMask += maskString[i] switch
                {
                    'X' => value[i],
                    _ => maskString[i]
                };
            }

            var left = Convert.ToInt64(effectiveMask, 2);
            Assert.Equal(73L, left);
        }

        [Fact]
        public void TestParseMaskOperation()
        {
            const string input = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X";
            var computer = BitMaskParser.Parse(input);

            var operation = Assert.Single(computer.Operations);
            var maskOperation = Assert.IsType<MaskOperation>(operation);
            Assert.Equal("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", maskOperation.MaskValue);
        }

        [Fact]
        public void TestParseMemoryOperation()
        {
            const string input = "mem[8] = 11";
            var computer = BitMaskParser.Parse(input);

            var operation = Assert.Single(computer.Operations);
            var (address, value) = Assert.IsType<MemoryOperation>(operation);
            Assert.Equal(11L, value);
            Assert.Equal(8, address);
        }

        [Fact]
        public void Example1()
        {
            const string input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
            
            var computer = BitMaskParser.Parse(input);
            var memory = computer.RunOperations();
            var sum = memory.GetValues().Sum();
            Assert.Equal(165L, sum);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day14.PuzzleInput.txt");
            
            var computer = BitMaskParser.Parse(input);
            var memory = computer.RunOperations();
            var sum = memory.GetValues().Sum();
            Assert.Equal(13865835758282L, sum);
        }
        
        [Fact]
        public void Example2()
        {
            const string input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";
            
            var computer = BitMaskParser.Parse(input);
            var memory = computer.RunOperations2();
            var sum = memory.GetValues().Sum();
            Assert.Equal(208L, sum);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day14.PuzzleInput.txt");
            
            var computer = BitMaskParser.Parse(input);
            var memory = computer.RunOperations2();
            var sum = memory.GetValues().Sum();
            Assert.Equal(4195339838136, sum);
        }
    }
}