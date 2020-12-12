using System;
using System.Linq;
using AdventOfCode2020.Day12;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day12
{
    public class Day12Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day12Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [InlineData("N10", Direction.North, 10)]
        [InlineData("E11", Direction.East, 11)]
        [InlineData("S12", Direction.South, 12)]
        [InlineData("W13", Direction.West, 13)]
        [InlineData("F14", Direction.Forward, 14)]
        [InlineData("L140", Direction.Left, 140)]
        [InlineData("R100", Direction.Right, 100)]
        [Theory]
        public void TestParser(string input, Direction expectedDirection, int expectedNumber)
        {
            var instructions = InstructionParser.ParseInstructions(input);
            var (direction, number) = Assert.Single(instructions)!;
            Assert.Equal(expectedDirection, direction);
            Assert.Equal(expectedNumber, number);
        }

        [InlineData("F10|N3|F7|R90|F11", 25)]
        [InlineData("F10|N3|F7|R180|F11", 9)]
        [Theory]
        public void Example1(string input, int distance)
        {
            var instructions = InstructionParser.ParseInstructions(input.Replace("|", Environment.NewLine));
            var ship = new Ship(FacingDirection.East, new Position());

            foreach (var instruction in instructions) 
                ship = ship.PerformInstruction(instruction);

            Assert.Equal(distance,ship.GetManhanttanDistance());
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day12.PuzzleInput.txt");
            
            var instructions = InstructionParser.ParseInstructions(input);
            var ship = new Ship(FacingDirection.East, new Position());
            ship = instructions.Aggregate(ship, (current, instruction) => current.PerformInstruction(instruction));
            
            _testOutputHelper.WriteLine(ship.GetManhanttanDistance().ToString());
        }
        
        
        [InlineData("F10|N3|F7|R90|F11", 286)]
        [Theory]
        public void Example2(string input, int distance)
        {
            var instructions = InstructionParser.ParseInstructions(input.Replace("|", Environment.NewLine));
            var waypoint = new Waypoint(FacingDirection.East, new Position {Horizontal = 10, Vertical = 1});
            var ship = new WaypointShip(FacingDirection.East, new Position(), waypoint);

            foreach (var instruction in instructions) 
                ship = ship.PerformInstruction(instruction);

            Assert.Equal(distance, ship.GetManhanttanDistance());
        }
        
        
        [Fact]
        public void SolvePuzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day12.PuzzleInput.txt");
            
            var instructions = InstructionParser.ParseInstructions(input);
            var waypoint = new Waypoint(FacingDirection.East, new Position {Horizontal = 10, Vertical = 1});
            var ship = new WaypointShip(FacingDirection.East, new Position(), waypoint);
            ship = instructions.Aggregate(ship, (current, instruction) => current.PerformInstruction(instruction));
            
            _testOutputHelper.WriteLine(ship.GetManhanttanDistance().ToString());
        }
    }
}