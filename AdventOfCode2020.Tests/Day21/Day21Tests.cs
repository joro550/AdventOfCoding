using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day21;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day21
{
    public class Day21Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day21Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

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
        
        [Fact]
        public void Puzzle2Example1()
        {
            const string input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";
            
            var menu = MenuParser.Parse(input);

            var foodsWithNoAllergens = menu.GetFoodWithAllergens();

            var odered = string.Join(",", foodsWithNoAllergens.OrderBy(x => x.Key).Select(x => x.Value.Name));
            Assert.Equal("mxmxvkd,sqjhc,fvjkl", odered);
        }
        
        [Fact]
        public void Puzzle2()
        {
            var exampleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day21.PuzzleInput.txt");
            var menu = MenuParser.Parse(exampleInput);

            var foodsWithNoAllergens = menu.GetFoodWithAllergens();
            var odered = string.Join(",", foodsWithNoAllergens.OrderBy(x => x.Key).Select(x => x.Value.Name));
            Assert.Equal("lkv,lfcppl,jhsrjlj,jrhvk,zkls,qjltjd,xslr,rfpbpn", odered);

        }
    }
}