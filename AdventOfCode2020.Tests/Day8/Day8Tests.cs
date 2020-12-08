using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Puzzle2GivenExampleInput_ThenAccIsFiveWhenLoopHappens()
        {
            var context = ExecutionContext(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6");
            Assert.Equal(8, context.Accumulator);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day8.PuzzleInput.txt");
            
            var context = ExecutionContext(puzzleInput);

            _testOutputHelper.WriteLine(context.CurrentOperation.ToString());
            _testOutputHelper.WriteLine(context.Accumulator.ToString());
        }

        private static ExecutionContext ExecutionContext(string puzzleInput)
        {
            ExecutionContext context = null;
            var program = LanguageParser.Parse(puzzleInput);

            for (var i = 0; i < program.Operations.Count; i++)
            {
                // Switch Jmp for no op or no op for jmp
                var originalOp = program.Operations[i];

                program.Operations[i] = originalOp switch
                {
                    JumpOperation => new NoOperation(originalOp.PlusMinus, originalOp.Number),
                    NoOperation => new JumpOperation(originalOp.PlusMinus, originalOp.Number),
                    _ => program.Operations[i]
                };

                // Execute with new operation
                context = program.Execute();

                // Did we get through the program?
                if (context.CurrentOperation == program.Operations.Count)
                    break;

                // Set it back
                program.Operations[i] = originalOp;
            }

            return context;
        }
    }
}