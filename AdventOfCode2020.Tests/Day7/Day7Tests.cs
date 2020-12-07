using Xunit;
using Xunit.Abstractions;
using AdventOfCode2020.Day7;

namespace AdventOfCode2020.Tests.Day7
{
    public class Day7Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day7Tests(ITestOutputHelper testOutputHelper) 
            => _testOutputHelper = testOutputHelper;

        [Fact]
        public void ExamplePuzzle()
        {
            const string input = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

            var bags = BagParser.Parse(input);
            var traverser = new Traverser(bags);
            var count = traverser.FindThing("shiny", "gold");
            Assert.Equal(4, count);
        }
        
        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day7.PuzzleInput.txt");

            var bags = BagParser.Parse(puzzleInput);
            var traverser = new Traverser(bags);
            var count = traverser.FindThing("shiny", "gold");

            _testOutputHelper.WriteLine(count.ToString());
        }
    }
}