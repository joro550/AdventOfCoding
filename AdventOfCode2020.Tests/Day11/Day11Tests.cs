using System;
using System.Linq;
using AdventOfCode2020.Day11;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day11
{
    public class Day11Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day11Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestParser()
        {
            var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            var seats = SeatParser.Parse2(input)
                .GetAllSpaces();
            
            Assert.Equal(0, seats.Count(floor => floor is OccupiedSeat));
            Assert.Equal(29, seats.Count(floor => floor is Floor));
            Assert.Equal(71, seats.Count(floor => floor is EmptySeat));
        }
        
        [InlineData("#LL", 0, 0, typeof(OccupiedSeat))]
        [InlineData("L#L", 1, 0, typeof(OccupiedSeat))]
        [InlineData("LLL|#LL", 0,1, typeof(OccupiedSeat))]
        [Theory]
        public void TestParser2(string input, int x, int y, Type type)
        {
            var seat = SeatParser.Parse2(input.Replace("|", Environment.NewLine))
                .GetCoords(x, y);
            Assert.IsType(type, seat);
        }

        [InlineData("LLL|LLL|LLL", 9)]
        [InlineData("L#L|LLL|LLL", 4)]
        [InlineData("###|##L|LLL", 3)]
        [Theory]
        public void RulesTests(string input, int expectedOccupiedSeats)
        {
            var seats = SeatParser
                .Parse2(input.Replace("|", Environment.NewLine));

            var seatFinder = new SeatFinder();
            var newMap = seatFinder.Execute(seats);
            var space = newMap.GetAllSpaces();
            
            Assert.Equal(expectedOccupiedSeats, space.Count(floor => floor is OccupiedSeat));
        }
        
        [Fact]
        public void Example1()
        {
            var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            
            var seats = SeatParser.Parse2(input);
            var seatFinder = new SeatFinder();

            Map currentMap = null;
            var cont = false;
            
            do
            {
                var map = currentMap?.DeepCopy();
                currentMap = seatFinder.Execute(currentMap ?? seats);

                _testOutputHelper.WriteLine(currentMap?.Print() ?? "Map was null");

                if (map == null)
                    cont = true;
                else if (map.Compare(currentMap))
                    cont = false;

            } while (cont);

            var spaces = currentMap!.GetAllSpaces().Count(x => x is OccupiedSeat);
            Assert.Equal(37, spaces);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day11.PuzzleInput.txt");
            
            var seats = SeatParser.Parse2(input);
            var seatFinder = new SeatFinder();

            Map currentMap = null;
            var cont = false;
            
            do
            {
                var map = currentMap?.DeepCopy();
                currentMap = seatFinder.Execute(currentMap ?? seats);

                if (map == null)
                    cont = true;
                else if (map.Compare(currentMap))
                    cont = false;

            } while (cont);

            var spaces = currentMap!.GetAllSpaces().Count(x => x is OccupiedSeat);
            _testOutputHelper.WriteLine(spaces.ToString());

        }
        
        [Fact]
        public void Example2()
        {
            var input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            
            var seats = SeatParser.Parse2(input);
            var seatFinder = new SeatFinder();

            Map currentMap = null;
            var cont = false;
            
            do
            {
                var map = currentMap?.DeepCopy();
                currentMap = seatFinder.Execute2(currentMap ?? seats);

                _testOutputHelper.WriteLine(currentMap?.Print() ?? "Map was null");

                if (map == null)
                    cont = true;
                else if (map.Compare(currentMap))
                    cont = false;

            } while (cont);

            var spaces = currentMap!.GetAllSpaces().Count(x => x is OccupiedSeat);
            Assert.Equal(26, spaces);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day11.PuzzleInput.txt");
            
            var seats = SeatParser.Parse2(input);
            var seatFinder = new SeatFinder();

            Map currentMap = null;
            var cont = false;
            
            do
            {
                var map = currentMap?.DeepCopy();
                currentMap = seatFinder.Execute2(currentMap ?? seats);

                if (map == null)
                    cont = true;
                else if (map.Compare(currentMap))
                    cont = false;

            } while (cont);

            var spaces = currentMap!.GetAllSpaces().Count(x => x is OccupiedSeat);
            _testOutputHelper.WriteLine(spaces.ToString());
        }
    }
}