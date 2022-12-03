using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2022.Day3;
using Xunit;

namespace AdventOfCode.Tests._2022.Day3;

public class Day3Tests
{
    [Fact]
    public void Things()
    {
        var sum = ItemPriorityConverter
            .GetSumOfSharedItems("vJrwpWtwJgWrhcsFMMfFFhFp");
        Assert.Equal(16, sum);
    }

    [Fact]
    public void GetOfMultipleBags()
    {
        var bags = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".SplitByNewLine();

        var sum = 0L;
        foreach (var bag in bags)
        {
            var sumOfSharedItems = ItemPriorityConverter.GetSumOfSharedItems(bag);
            sum += sumOfSharedItems;
        }
        Assert.Equal(157, sum);
    }

    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "3")
            .SplitByNewLine();
        
        var sum = input.Sum(ItemPriorityConverter.GetSumOfSharedItems);
        Assert.Equal(8349, sum);
    }

    [Fact]
    public void Example2()
    {
        var bags = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".SplitByNewLine();
        var sum = ItemPriorityConverter.Thing(bags);
        Assert.Equal(70, sum);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "3")
            .SplitByNewLine();
        
        var sum = ItemPriorityConverter.Thing(input);
        Assert.Equal(2681, sum);
    }
}