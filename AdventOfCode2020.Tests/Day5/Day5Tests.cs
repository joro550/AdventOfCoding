using Xunit;
using System;
using System.Linq;
using Xunit.Abstractions;
using AdventOfCode2020.Day5;

namespace AdventOfCode2020.Tests.Day5
{
    public class Day5Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day5Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;
        
        // 128 - rows
        // 8 - columns

        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        [Theory]
        public void GivenSeatInput_CorrectSeatIsAssigned(string input, int expectedSeatId)
        {
            var seat = SeatFinder.FindSeat(input, 128, 8);
            Assert.Equal(expectedSeatId, seat.GetSeatId());
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day5.PuzzleInput.txt");

            var seats = puzzleInput.Split(Environment.NewLine)
                .Select(line => SeatFinder.FindSeat(line, 128, 8))
                .ToList();

            var max = seats.Max(s => s.GetSeatId());
            _testOutputHelper.WriteLine(max.ToString());
            Assert.Equal(906, max);
        }

        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day5.PuzzleInput.txt");

            var seats = puzzleInput.Split(Environment.NewLine)
                .Select(line => SeatFinder.FindSeat(line, 128, 8))
                .Select(seat => seat.GetSeatId())
                .OrderBy(s => s)
                .ToList();

            var seatNumber = seats[0];
            foreach (var seatNum in seats)
            {
                if (seatNum != seatNumber && seatNum != seatNumber + 1)
                {
                    _testOutputHelper.WriteLine((seatNum - 1).ToString());
                    break;
                }
                else
                {
                    seatNumber = seatNum;
                }
            }
        }
    }
}