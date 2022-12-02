using System;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day2;

public static class ShapeParser
{
    public static Shape Parse(string input)
    {
        return input switch
        {
            "A" => Shape.Rock,
            "B" => Shape.Paper,
            "C" => Shape.Scissors,
            "X" => Shape.Rock,
            "Y" => Shape.Paper,
            "Z" => Shape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
    }
    
    public static Shape Parse(Shape shape, string input)
    {
        return input switch
        {
            // lose
            "X" => shape switch
            {
                Shape.Rock => Shape.Scissors,
                Shape.Paper => Shape.Rock,
                Shape.Scissors => Shape.Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
            },
            // win
            "Z" => shape switch
            {
                Shape.Rock => Shape.Paper,
                Shape.Paper => Shape.Scissors,
                Shape.Scissors => Shape.Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
            },
            //draw
            _ => shape
        };
    }

    public static long GetGameScore(Shape playerOneShape, Shape playerTwoShape)
    {
        // If it's a draw then return 3
        if (playerOneShape == playerTwoShape)
            return 3;

        return playerOneShape switch
        {
            Shape.Rock => playerTwoShape == Shape.Paper ? 0 : 6,
            Shape.Paper => playerTwoShape == Shape.Scissors ? 0 : 6,
            Shape.Scissors => playerTwoShape == Shape.Rock ? 0 : 6,
            _ => throw new ArgumentOutOfRangeException(nameof(playerOneShape), playerOneShape, null)
        };
    }

    public static long GetScoreForWinner(Shape playerOneShape, Shape playerTwoShape)
    {
        var gameScore = GetGameScore(playerOneShape, playerTwoShape);
        return gameScore + GetShapeScore(playerOneShape);
    }

    private static int GetShapeScore(Shape shape)
    {
        return shape switch
        {
            Shape.Rock => 1,
            Shape.Paper => 2,
            Shape.Scissors => 3,
            _ => 0
        };
    }
}

public static class RockPaperScissor
{
    public static long Play(string input)
    {
        return (from line in input.SplitByNewLine() 
            select line.SplitBySpace() into stringShapes 
            let theirMove = ShapeParser.Parse(stringShapes[0]) 
            let ourMove = ShapeParser.Parse(stringShapes[1]) 
            select ShapeParser.GetScoreForWinner(ourMove, theirMove))
            .Sum();
    }
    
    public static long Play2(string input)
    {
        return (from line in input.SplitByNewLine() 
            select line.SplitBySpace() into stringShapes 
            let theirMove = ShapeParser.Parse(stringShapes[0]) 
            let ourMove = ShapeParser.Parse(theirMove, stringShapes[1]) 
            select ShapeParser.GetScoreForWinner(ourMove, theirMove)).Sum();
    }
}