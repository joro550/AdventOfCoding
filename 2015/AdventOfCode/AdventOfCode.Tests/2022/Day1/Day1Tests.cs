using System;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests._2022.Day1
{
    public class Day1Tests
    {
        [Fact]
        public void GivenAListOfCalories()
        {
            var calories = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";
            var elfList =
                AdventOfCode._2022.Day1.CaloriesCounter.Count(
                    calories.Split(Environment.NewLine + Environment.NewLine));
            Assert.Equal(24000, elfList.Max());
        }

        [Fact]
        public void Puzzle1()
        {
            var calories = FileReader.GetResource("AdventOfCode.Tests._2022.Day1.PuzzleInput.txt");
            var elfList =
                AdventOfCode._2022.Day1.CaloriesCounter.Count(
                    calories.Split(Environment.NewLine + Environment.NewLine));
            Assert.Equal(69206, elfList.Max());
        }
        
        [Fact]
        public void Puzzle2()
        {
            var calories = FileReader.GetResource("AdventOfCode.Tests._2022.Day1.PuzzleInput.txt");
            var elfList =
                AdventOfCode._2022.Day1.CaloriesCounter.Count(
                        calories.Split(Environment.NewLine + Environment.NewLine))
                    .OrderDescending()
                    .Take(3)
                    .Sum();
            Assert.Equal(197400, elfList);
        }
    }
}