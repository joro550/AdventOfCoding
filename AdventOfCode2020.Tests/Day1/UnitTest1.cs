using Xunit;
using System;
using Xunit.Abstractions;
using AdventOfCode2020.Day1;

namespace AdventOfCode2020.Tests.Day1
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void Puzzle1()
        {
            const string exampleInput = @"1721
979
366
299
675
1456";
            var numbers = new NumberParser()
                .Parse(exampleInput, Environment.NewLine);

            var (firstNumber, secondNumber) = NumberRetriever.GetNumbersThatReachTarget(numbers, 2020);

            const int answer = 514579;
            Assert.Equal(1721, firstNumber);
            Assert.Equal(299, secondNumber);
            Assert.Equal(514579, answer);
        }
        
        [Fact]
        public void Puzzle2()
        {
            const string exampleInput = @"1721
979
366
299
675
1456";
            var numbers = new NumberParser()
                .Parse(exampleInput, Environment.NewLine);

            var (firstNumber, secondNumber, thirdNumber) 
                = NumberRetriever.GetTripletThatReachTarget(numbers, 2020);

            Assert.Equal(979, firstNumber);
            Assert.Equal(366, secondNumber);
            Assert.Equal(675, thirdNumber);
        }
        
        [Fact]
        public void SolvePuzzle()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day1.PuzzleInput.txt");
            
            var numbers = new NumberParser()
                .Parse(exampleInput, Environment.NewLine);

            var number = NumberRetriever.GetNumbersThatReachTarget(numbers, 2020);

            var answer = number.Multiply();
            _testOutputHelper.WriteLine(answer.ToString());
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day1.PuzzleInput.txt");
            var numbers = new NumberParser()
                .Parse(exampleInput, Environment.NewLine);

            var number = NumberRetriever.GetTripletThatReachTarget(numbers, 2020);
            Assert.Equal(170098110, number.Multiply());
            _testOutputHelper.WriteLine(number.Multiply().ToString());
        }
    }
}