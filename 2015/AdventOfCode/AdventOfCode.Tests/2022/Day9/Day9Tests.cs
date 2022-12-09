using System;
using Xunit;
using AdventOfCode._2022.Day9;
using Xunit.Abstractions;

namespace AdventOfCode.Tests._2022.Day9;

public class Day9Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day9Tests(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void Example1()
    {
        
        var instructionString = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

        var grid = Grid.New();
        var instructions = Instruction.Parse(instructionString);

        foreach (var instruction in instructions)
        {
            grid = instruction.ExecuteInstruction(grid);
        }

        var visited = grid.GetVisitedCells();
        Assert.Equal(13, visited.Count);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var instructionString = FileReader.GetResource("2022", "9");

        var grid = Grid.New();
        var instructions = Instruction.Parse(instructionString);

        foreach (var instruction in instructions)
        {
            grid = instruction.ExecuteInstruction(grid);
        }

        var visited = grid.GetVisitedCells();
        Assert.Equal(5902, visited.Count);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var instructionString = FileReader.GetResource("2022", "9");

        var grid = Grid.New();
        var instructions = Instruction.Parse(instructionString);

        foreach (var instruction in instructions)
        {
            grid = instruction.ExecuteInstruction(grid);
        }

        var visited = grid.GetVisitedCells();
        Assert.Equal(27, visited.Count);
    }
}