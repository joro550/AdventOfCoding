using AdventOfCode._2015.Day7;
using Xunit;

namespace AdventOfCode.Tests._2015.Day7
{
    public class CircuitTests
    {
        [Fact]
        public void AssignTest()
        {
            const string script = "123 -> x";
            var program = CircuitInterpreter.Parse(script);
            var runResult = program.Run();
            var value = runResult.GetWireValue("x");
            Assert.Equal(123, value);
        }
        
        [Fact]
        public void AndTest()
        {
            const string script = @"123 -> x
456 -> y
x AND y -> d";
            var program = CircuitInterpreter.Parse(script);
            var runResult = program.Run();
            
            Assert.Equal(72, runResult.GetWireValue("d"));
        }
        
        [Fact]
        public void NotTest()
        {
            const string script = @"123 -> x
NOT x -> h";
            var program = CircuitInterpreter.Parse(script);
            var runResult = program.Run();
            
            Assert.Equal(65412, runResult.GetWireValue("h"));
        }
        
        [Fact]
        public void ExampleTest()
        {
            const string script = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i";
            
            var program = CircuitInterpreter.Parse(script);
            var runResult = program.Run();
            
            Assert.Equal(72, runResult.GetWireValue("d"));
            Assert.Equal(507, runResult.GetWireValue("e"));
            Assert.Equal(492, runResult.GetWireValue("f"));
            Assert.Equal(114, runResult.GetWireValue("g"));
            Assert.Equal(65412, runResult.GetWireValue("h"));
            Assert.Equal(65079, runResult.GetWireValue("i"));
            Assert.Equal(123, runResult.GetWireValue("x"));
            Assert.Equal(456, runResult.GetWireValue("y"));
        }
        
        [Fact]
        public void Puzzle1Test()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day7.PuzzleInput.txt");
            
            var program = CircuitInterpreter.Parse(input);
            var runResult = program.Run();
            
            Assert.Equal(0, runResult.GetWireValue("a"));
        }
    }
}