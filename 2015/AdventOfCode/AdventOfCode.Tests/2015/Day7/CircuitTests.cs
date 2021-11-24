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
            Assert.Equal((ushort)123, program.GetWireValue("x"));
        }
        
        [Fact]
        public void AndTest()
        {
            const string script = @"123 -> x
456 -> y
x AND y -> d";
            var program = CircuitInterpreter.Parse(script);
            Assert.Equal(72, program.GetWireValue("d"));
        }
        
        [Fact]
        public void NotTest()
        {
            const string script = @"123 -> x
NOT x -> h";
            var program = CircuitInterpreter.Parse(script);
            Assert.Equal(65412, program.GetWireValue("h"));
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
            
            Assert.Equal(72, program.GetWireValue("d"));
            Assert.Equal(507, program.GetWireValue("e"));
            Assert.Equal(492, program.GetWireValue("f"));
            Assert.Equal(114, program.GetWireValue("g"));
            Assert.Equal(65412, program.GetWireValue("h"));
            Assert.Equal(65079, program.GetWireValue("i"));
            Assert.Equal(123, program.GetWireValue("x"));
            Assert.Equal(456, program.GetWireValue("y"));
        }
        
        [Fact]
        public void Puzzle1Test()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day7.PuzzleInput.txt");
            
            var program = CircuitInterpreter.Parse(input);
            Assert.Equal(46065, program.GetWireValue("a"));
        }
        
        [Fact]
        public void Puzzle2Test()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2015.Day7.PuzzleInput2.txt");
            
            var program = CircuitInterpreter.Parse(input);
            Assert.Equal(0, program.GetWireValue("a"));
        }
    }
}