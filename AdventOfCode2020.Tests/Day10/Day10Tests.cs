using System;
using System.Linq;
using AdventOfCode2020.Day10;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day10
{
    public class Day10Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day10Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ExampleInput()
        {
            var adapters = AdapterParser.GetAdapters(@"16
10
15
5
1
11
7
19
6
12
4");
            var socket = new Adapter(0);
            var device = new Device(adapters);
            var returnValue = AdapterPlugger.PlugAdaptersIn(socket, device, adapters);
            
            Assert.Equal(7, returnValue[1]);
            Assert.Equal(5, returnValue[3]);
        }
        
        [Fact]
        public void ExampleInput2()
        {
            var adapters = AdapterParser.GetAdapters(@"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3");
            var socket = new Adapter(0);
            var device = new Device(adapters);
            var returnValue = AdapterPlugger.PlugAdaptersIn(socket, device, adapters);
            
            Assert.Equal(22, returnValue[1]);
            Assert.Equal(10, returnValue[3]);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day10.PuzzleInput.txt");

            var adapters = AdapterParser.GetAdapters(input);
            var socket = new Adapter(0);
            var device = new Device(adapters);
            var returnValue = AdapterPlugger.PlugAdaptersIn(socket, device, adapters);

            var sum = returnValue[1] * returnValue[3];
            _testOutputHelper.WriteLine(sum.ToString());
        }

        [Fact]
        public void Puzzle2ExampleInput()
        {
            var adapters = AdapterParser.GetAdapters(@"16
10
15
5
1
11
7
19
6
12
4");
            var socket = new Adapter(0);
            var device = new Device(adapters);
            var returnValue = new AdapterPlugger().CreateAdapterTree(socket, device, adapters);
            var leafCount = new TreeWalker()
                .CountType<Device>(returnValue);

            Assert.Equal(8, leafCount);
        }

        [Fact]
        public void SolvePuzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day10.PuzzleInput.txt");

            var adapters = AdapterParser.GetAdapters(input);
            var socket = new Adapter(0);
            var device = new Device(adapters);
            var returnValue = new AdapterPlugger().CreateAdapterTree(socket, device, adapters);
            var leafCount = new TreeWalker()
                .CountType<Device>(returnValue);
            _testOutputHelper.WriteLine(leafCount.ToString());
        }
    }
}