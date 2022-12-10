using System;
using AdventOfCode._2022.Day10;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests._2022.Day10;

public class Day10Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day10Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Example2()
    {
        var operations = FileReader.GetResource("AdventOfCode.Tests._2022.Day10.ExampleInput.txt");
        var output = CycleCounter.Parse(operations);
        
        var one = 20L * output[20];
        var two = 60L * output[60];
        var three = 100L * output[100];
        var four = 140L * output[140];
        var five = 180L * output[180];
        var six = 220L * output[220];
        Assert.Equal(13140, one+two+three+four+five+six);
    }
    
    [Fact]
    public void Example3()
    {
        var operations = FileReader.GetResource("AdventOfCode.Tests._2022.Day10.ExampleInput.txt");
        var output = CycleCounter.Parse2(operations);
        
        _testOutputHelper.WriteLine(output);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var operations = FileReader.GetResource("2022", "10");

        var output = CycleCounter.Parse(operations);

        var one = 20L * output[20];
        var two = 60L * output[60];
        var three = 100L * output[100];
        var four = 140L * output[140];
        var five = 180L * output[180];
        var six = 220L * output[220];
        Assert.Equal(14860, one+two+three+four+five+six);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var operations = FileReader.GetResource("2022", "10");
        var output = CycleCounter.Parse2(operations);
        
        _testOutputHelper.WriteLine(output);
    }
}