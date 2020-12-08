using System;
using AdventOfCode2020.Day8;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day8
{
    public class Day8Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day8Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GivenExampleInput_ThenAccIsFiveWhenLoopHappens()
        {
            var program = LanguageParser.Parse(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6");

            var context = program.Execute();
            Assert.Equal(5, context.Accumulator);
        }
        
        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day8.PuzzleInput.txt");
            var program = LanguageParser.Parse(puzzleInput);

            var context = program.Execute();
            _testOutputHelper.WriteLine(context.Accumulator.ToString());
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day8.PuzzleInput2.txt");
            var program = LanguageParser.Parse(puzzleInput);

            var context = program.Execute();
            _testOutputHelper.WriteLine(context.CurrentOperation.ToString());
            _testOutputHelper.WriteLine(context.Accumulator.ToString());
        }
    }
}