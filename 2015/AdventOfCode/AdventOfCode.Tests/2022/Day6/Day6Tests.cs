using System.Linq;
using Xunit;

namespace AdventOfCode.Tests._2022.Day6;

public class Day6Tests
{
    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void Example1(string input, int expectedMarkerLocation)
    {
        var marker = CountCharactersUntilMarker(input);
        Assert.Equal(expectedMarkerLocation, marker);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "6");

        var marker = CountCharactersUntilMarker(input);
        Assert.Equal(1198, marker);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "6");

        var marker = CountCharactersUntilMarker(input, 14);
        Assert.Equal(3120, marker);
    }

    private static int CountCharactersUntilMarker(string input, int size = 4)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if(i < size)
                continue;

            var start = i - size;
            var possibleMarker = input[start..i];
            
            var distinctLength = possibleMarker.Distinct().Count();
            if (distinctLength == size)
                return i;
        }

        return -1;
    }
}