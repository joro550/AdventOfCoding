using System;
using Xunit;
using Xunit.Abstractions;
using AdventOfCode2020.Day8;

namespace AdventOfCode2020.Tests.Day8
{
    public class Day8Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day8Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void GivenMultipleInstructions_ThenMultipleOperationsAreAddedToProgram()
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
            Assert.Equal(9, program.Operations.Count);
            Assert.IsType<NoOperation>(program.Operations[0]);
            Assert.IsType<AccumulateOperation>(program.Operations[1]);
            Assert.IsType<JumpOperation>(program.Operations[2]);
            Assert.IsType<AccumulateOperation>(program.Operations[3]);
            Assert.IsType<JumpOperation>(program.Operations[4]);
            Assert.IsType<AccumulateOperation>(program.Operations[5]);
            Assert.IsType<AccumulateOperation>(program.Operations[6]);
            Assert.IsType<JumpOperation>(program.Operations[7]);
            Assert.IsType<AccumulateOperation>(program.Operations[8]);
        }
        
        [InlineData("nop +0", typeof(NoOperation))]
        [InlineData("acc +1", typeof(AccumulateOperation))]
        [InlineData("jmp -3", typeof(JumpOperation))]
        [Theory]
        public void GivenOperation_ThenAppropriateOperationIsAddedToProgram(string input, Type operationType)
        {
            var program = LanguageParser.Parse(input);

            Assert.Single(program.Operations);
            Assert.IsType(operationType, program.Operations[0]);
        }
        
        [InlineData("nop +0", "+", 0)]
        [InlineData("nop -0", "-", 0)]
        [InlineData("acc +1", "+", 1)]
        [InlineData("acc -1", "-", 1)]
        [InlineData("jmp -3", "-", 3)]
        [InlineData("jmp +3", "+", 3)]
        [Theory]
        public void GivenOperationWithParameters_ThenAppropriateParametersAreAddedToOperation(string input, string plusMinus, int number)
        {
            var program = LanguageParser.Parse(input);

            var op = Assert.Single(program.Operations);
            Assert.Equal(plusMinus, op!.PlusMinus);
            Assert.Equal(number, op!.Number);
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