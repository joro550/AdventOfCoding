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
        
        [Fact]
        public void ExamplePuzzle2()
        {
            const string input = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";

            var bags = BagParser.Parse(input);
            var traverser = new BagCounter(bags);
            var things = traverser.FindThing("shiny", "gold");

            
            Assert.Equal(126, things);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day7.PuzzleInput.txt");

            var bags = BagParser.Parse(puzzleInput);
            var traverser = new BagCounter(bags);
            var count = traverser.FindThing("shiny", "gold");

            _testOutputHelper.WriteLine(count.ToString());
        }
    }
}