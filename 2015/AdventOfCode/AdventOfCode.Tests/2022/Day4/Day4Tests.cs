using AdventOfCode._2022.Day4;
using Xunit;

namespace AdventOfCode.Tests._2022.Day4;

public class Day4Tests
{
    [Fact]
    public void WhenOneSectorCoversTheOtherTrueIsReturned()
    {
        const string pair = "2-8,3-7";
        var result = SectorParser.OneCoversTheOther(pair);
        Assert.True(result);
    }
    
    
    [Fact]
    public void WhenOneSectorDoesntCoversTheOtherFalseIsReturned()
    {
        const string pair = "2-4,6-8";
        var result = SectorParser.OneCoversTheOther(pair);
        Assert.False(result);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var pair = FileReader.GetResource("2022", "4")
            .SplitByNewLine();
        var result = SectorParser.Puzzle1Solver(pair);
        Assert.Equal(462, result);
    }
    
    [Fact]
    public void Puzzle2Example()
    {
        var pair = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"
            .SplitByNewLine();
        var result = SectorParser.Puzzle2Solver(pair);
        Assert.Equal(4, result);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var pair = FileReader.GetResource("2022", "4")
            .SplitByNewLine();
        var result = SectorParser.Puzzle2Solver(pair);
        Assert.Equal(835, result);
    }
}