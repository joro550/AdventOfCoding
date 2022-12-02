using AdventOfCode._2022.Day2;
using Xunit;

namespace AdventOfCode.Tests._2022.Day2;

public class Day2Tests
{
    [Theory]
    [InlineData("A", Shape.Rock)]
    [InlineData("B", Shape.Paper)]
    [InlineData("C", Shape.Scissors)]
    [InlineData("X", Shape.Rock)]
    [InlineData("Y", Shape.Paper)]
    [InlineData("Z", Shape.Scissors)]
    public void ParserTests(string input, Shape expectedShape)
    {
        var shape = ShapeParser.Parse(input);
        Assert.Equal(expectedShape, shape);
    }

    
    [Theory]
    // Draws
    [InlineData(Shape.Rock, Shape.Rock, 3L)]
    [InlineData(Shape.Paper, Shape.Paper, 3L)]
    [InlineData(Shape.Scissors, Shape.Scissors, 3L)]

    // Wins
    [InlineData(Shape.Paper, Shape.Rock, 6L)]
    [InlineData(Shape.Scissors, Shape.Paper, 6L)]
    [InlineData(Shape.Rock, Shape.Scissors, 6L)]
    
    // loses
    [InlineData(Shape.Rock, Shape.Paper, 0L)]
    [InlineData(Shape.Paper, Shape.Scissors, 0L)]
    [InlineData(Shape.Scissors, Shape.Rock, 0L)]
    public void ScoreTests(Shape shapeOne, Shape shapeTwo, long expectedScore)
    {
        var score = ShapeParser.GetGameScore(shapeOne, shapeTwo);
        Assert.Equal(expectedScore, score);
    }

    [Theory]
    [InlineData(Shape.Rock, Shape.Scissors, 7L)]
    [InlineData(Shape.Paper, Shape.Rock, 8L)]
    [InlineData(Shape.Scissors, Shape.Paper, 9L)]
    public void TestWinningScore(Shape shapeOne, Shape shapeTwo, long expectedScore)
    {
        var score = ShapeParser.GetScoreForWinner(shapeOne, shapeTwo);
        Assert.Equal(expectedScore, score);
    }
    
    [Fact]
    public void Example1()
    {
        const string input = @"A Y
B X
C Z";
        var game = RockPaperScissor.Play(input);
        Assert.Equal(15, game);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "2");
        var game = RockPaperScissor.Play(input);
        Assert.Equal(15422, game);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "2");
        var game = RockPaperScissor.Play2(input);
        Assert.Equal(15442, game);
    }
}