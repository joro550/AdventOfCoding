using AdventOfCode._2022.Day8;
using Xunit;

namespace AdventOfCode.Tests._2022.Day8;

public class Day8Tests
{
    [Fact]
    public void Example1()
    {
        var input = @"30373
25512
65332
33549
35390";

        var grid = TreeGrid.Parse(input);
        var count = grid.CountVisibleTrees();

        Assert.Equal(21, count);
    }
    
    [Fact]
    public void Example2()
    {
        var input = @"30373
25512
65332
33549
35390";

        var grid = TreeGrid.Parse(input);
        var count = grid.GetBestScenicScore();

        Assert.Equal(8, count);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "8");

        var grid = TreeGrid.Parse(input);
        var count = grid.CountVisibleTrees();

        Assert.Equal(1698, count);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "8");

        var grid = TreeGrid.Parse(input);
        var count = grid.GetBestScenicScore();

        Assert.Equal(672280, count);
    }
}