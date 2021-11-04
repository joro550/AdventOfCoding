using System;
using System.Linq;
using AdventOfCode._2015.Day6;
using Xunit;

namespace AdventOfCode.Tests._2015.Day6
{
    public class RuleInterpreterTests
    {
        [Fact]
        public void TurnOnRules_ReturnsTurnOnRule()
        {
            const string ruleString = "turn on 0,0 through 999,999";
            var rule = RuleInterpreter.Eval(ruleString);
            var turnOnRule = Assert.IsType<TurnOnRule>(rule);
            Assert.Equal(0, turnOnRule.From.X);
            Assert.Equal(0, turnOnRule.From.Y);
            Assert.Equal(999, turnOnRule.To.Y);
            Assert.Equal(999, turnOnRule.To.Y);
        }
        
        [Fact]
        public void TurnOffRules_ReturnsTurnOffRule()
        {
            const string ruleString = "turn off 0,0 through 999,999";
            var rule = RuleInterpreter.Eval(ruleString);
            var turnOffRule = Assert.IsType<TurnOffRule>(rule);
            
            Assert.Equal(0, turnOffRule.From.X);
            Assert.Equal(0, turnOffRule.From.Y);
            Assert.Equal(999, turnOffRule.To.Y);
            Assert.Equal(999, turnOffRule.To.Y);
        }
        
        [Fact]
        public void ToggleRules_ReturnsToggleRule()
        {
            const string ruleString = "toggle 0,0 through 999,999";
            var rule = RuleInterpreter.Eval(ruleString);
            var toggleRule = Assert.IsType<ToggleRule>(rule);
            Assert.Equal(0, toggleRule.From.X);
            Assert.Equal(0, toggleRule.From.Y);
            Assert.Equal(999, toggleRule.To.Y);
            Assert.Equal(999, toggleRule.To.Y);
        }
    }

    public class Solve
    {
        [Fact]
        public void Example1()
        {
            const string ruleString = "toggle 0,0 through 99,99";
            var rule = RuleInterpreter.Eval(ruleString);

            var newGrid = rule.ExecuteRule(LightGrid.Create(100, 100));

            var lightsLit = newGrid.GetLights();
            Assert.Equal(10000, lightsLit);
        }
        
        [Fact]
        public void Puzzle1()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day6.PuzzleInput.txt");

            var lines = input.Split(Environment.NewLine);
            var lightGrid = LightGrid.Create(1000, 1000);

            lightGrid = lines.Select(RuleInterpreter.Eval)
                .Aggregate(lightGrid, (current, rule) => rule.ExecuteRule(current));

            var lightsLit = lightGrid.GetLights();
            Assert.Equal(10000, lightsLit);
        }
        
        [Fact]
        public void Puzzle2()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day6.PuzzleInput.txt");

            var lines = input.Split(Environment.NewLine);
            var lightGrid = LightGrid.Create(1000, 1000);

            lightGrid = lines.Select(RuleInterpreter.Eval)
                .Aggregate(lightGrid, (current, rule) => rule.ExecuteRule(current));

            var lightsLit = lightGrid.GetLightBrightness();
            Assert.Equal(13614336, lightsLit);
        }
    }

}