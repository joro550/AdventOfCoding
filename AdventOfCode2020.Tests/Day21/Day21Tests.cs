using System.Linq;
using AdventOfCode2020.Day21;
using Xunit;

namespace AdventOfCode2020.Tests.Day21
{
    public class Day21Tests
    {
        [Fact]
        public void Example1()
        {
            const string input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";
            
            var menu = MenuParser.Parse(input);
            var foodsWithNoAllergens = menu.GetFoodWithNoAllergens().Select(x => x.Name);

            var count = menu.GetInstances(foodsWithNoAllergens);
            Assert.Equal(5, count);
        }
        
        [Fact]
        public void Puzzle1()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day21.PuzzleInput.txt");
            var menu = MenuParser.Parse(exampleInput);

            var foodsWithNoAllergens = menu.GetFoodWithNoAllergens().Select(x => x.Name);
            var count = menu.GetInstances(foodsWithNoAllergens);
            Assert.Equal(2779, count);
        }
    }
}