using Xunit;
using AdventOfCode._2021.Day5;

namespace AdventOfCode.Tests._2021.Day5
{
    public class VentTests
    {
        [Fact]
        public void GivenCoordinatesThenItParsesCorrectly()
        {
            const string input = "0,2 -> 1,3";
            var vent = Vent.Parse(input);
            Assert.Equal(0, vent.Start.X);
            Assert.Equal(2, vent.Start.Y);
            Assert.Equal(1, vent.End.X);
            Assert.Equal(3, vent.End.Y);
        }

        [Theory]
        [InlineData("0,2 -> 0,3")]
        [InlineData("0,2 -> 1,2")]
        public void GivenHorizontalOrVerticalPipeThenIsValidIsTrue(string input)
        {
            var vent = Vent.Parse(input);
            Assert.True(vent.IsValid());
        }

        [Theory]
        [InlineData("0,2 -> 1,3")]
        public void GivenDiagonalLineThenIsValidIsFalse(string input)
        {
            var vent = Vent.Parse(input);
            Assert.False(vent.IsValid());
        }

        [Fact]
        public void GetAllPointsForVent()
        {
            const string input = "0,2 -> 0,4";
            var vent = Vent.Parse(input);
            var points = vent.GetPointsAffected2();

            Assert.Contains(new Coords(0, 2), points);
            Assert.Contains(new Coords(0, 3), points);
            Assert.Contains(new Coords(0, 4), points);
        }
        
        [Fact]
        public void GetAllPointsForVentDiagonal()
        {
            const string input = "9,7 -> 7,9";
            var vent = Vent.Parse(input);
            var points = vent.GetPointsAffected2();

            Assert.Contains(new Coords(0, 2), points);
            Assert.Contains(new Coords(0, 3), points);
            Assert.Contains(new Coords(0, 4), points);
        }
        
        [Fact]
        public void GetAllPointsForVentBackwards()
        {
            const string input = "0,4 -> 0,2";
            var vent = Vent.Parse(input);
            var points = vent.GetPointsAffected2();

            Assert.Contains(new Coords(0, 2), points);
            Assert.Contains(new Coords(0, 3), points);
            Assert.Contains(new Coords(0, 4), points);
        }

        [Fact]
        public void Example1()
        {
            const string input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";
            var vents = Vents.ParseValidLines(input);
            var map = vents.CreateMap();
            var value = map.GetValue(3, 4);
            Assert.Equal(2, value);
        }
        
        [Fact]
        public void Example2()
        {
            const string input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";
            var vents = Vents.ParseAllLines(input);
            var map = vents.CreateMap();
            var value = map.GetValue(4, 4);
            Assert.Equal(3, value);
        }

        [Fact]
        public void Puzzle1()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2021.Day5.PuzzleInput.txt");
            
            var vents = Vents.ParseValidLines(input);
            var map = vents.CreateMap();
            var count = map.Count(2);
            Assert.Equal(8060L, count);
        }
        
        [Fact]
        public void Puzzle2()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2021.Day5.PuzzleInput.txt");
            
            var vents = Vents.ParseAllLines(input);
            var map = vents.CreateMap();
            var count = map.Count(2);
            Assert.Equal(21577L, count);
        }
    }
}