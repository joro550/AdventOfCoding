using System.Linq;
using AdventOfCode._2022.Day5;
using Xunit;

namespace AdventOfCode.Tests._2022.Day5;

public class Day5Tests
{
    [Fact]
    public void Thing()
    {
        var things = StackParser.Parse(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 ");
        
        Assert.Equal(3, things.Count);
        Assert.Equal(2, things[0].Names.Count);
        Assert.Equal(3, things[1].Names.Count);
        Assert.Equal(1, things[2].Names.Count);
    }
    
    
    [Fact]
    public void InstructionParser()
    {
        var things = StackParser.ParseInstructions(@"move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2");
        
        Assert.Equal(4, things.Count);
        var moveInstruction = Assert.IsType<MoveInstruction>(things[0]);
        
        Assert.Equal(1, moveInstruction.Amount);
        Assert.Equal(2, moveInstruction.FromIndex);
        Assert.Equal(1, moveInstruction.ToIndex);
    }

    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "5")
            .SplitByDoubleNewLine();

        var things = StackParser.Parse(input[0]);
        var instructions = StackParser.ParseInstructions(input[1]);
        var result = Executor.Execute(instructions, things);

        var letters = result
            .Where(r => r.Value.Any)
            .Aggregate("", (current, r) => current + r.Value.Pop());
        
        Assert.Equal("HNSNMTLHQ", letters);
    }

    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "5")
            .SplitByDoubleNewLine();

        var things = StackParser.Parse(input[0]);
        var instructions = StackParser.ParseInstructions(input[1]);
        var result = Executor.Execute2(instructions, things);

        var letters = result
            .Where(r => r.Value.Any)
            .Aggregate("", (current, r) => current + r.Value.Pop());
        
        Assert.Equal("RNLFDJMCT", letters);
    }

    [Fact]
    public void Example1()
    {
        var input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"
            .SplitByDoubleNewLine();

        var things = StackParser.Parse(input[0]);
        var instructions = StackParser.ParseInstructions(input[1]);
        var result = Executor.Execute(instructions, things);

        var letters = result
            .Where(r => r.Value.Any)
            .Aggregate("", (current, r) => current + r.Value.Pop());
        
        Assert.Equal("CMZ", letters);
    }

    [Fact]
    public void Example2()
    {
        var input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"
            .SplitByDoubleNewLine();

        var things = StackParser.Parse(input[0]);
        var instructions = StackParser.ParseInstructions(input[1]);
        var result = Executor.Execute2(instructions, things);

        var letters = result
            .Where(r => r.Value.Any)
            .Aggregate("", (current, r) => current + r.Value.Pop());
        
        Assert.Equal("MCD", letters);
    }
}