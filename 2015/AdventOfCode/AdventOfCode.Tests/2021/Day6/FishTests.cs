using System.Linq;
using AdventOfCode._2021.Day6;
using Xunit;

namespace AdventOfCode.Tests._2021.Day6
{
    public class FishTests
    {
        [Fact]
        public void Example1()
        {
            var input = "3,4,3,1,2";
            var result = FishSimulator.Simulate(input, 18);
            Assert.Equal(26, result);
        }

        [Fact]
        public void TestParse()
        {
            var input = "3,4,3,1,2";
            var thing = LanternFish2.FromList(input.Split(",").Select(int.Parse).ToList());
        }
        
        
        [Fact]
        public void Puzzle1()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2021.Day6.PuzzleInput.txt");
            
            var result = FishSimulator.Simulate(input, 80);
            Assert.Equal(350149L, result);
        }
        
        [Fact]
        public void Puzzle2()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2021.Day6.PuzzleInput.txt");
            
            var result = FishSimulator.Simulate(input, 256);
            Assert.Equal(1590327954513L, result);
        }
    }
}