using System.Linq;
using AdventOfCode._2022.Day7;
using Xunit;

namespace AdventOfCode.Tests._2022.Day7;

public class Day7Tests
{
    [Fact]
    public void Example1()
    {
        const string history = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
        var fileSystem = Lexer.DoThing(history);
        var size = DirectorySizeCounter.CountSize(fileSystem);
        var sum = size.Values.Sum();
        Assert.Equal(95437, sum);
    }
    
    [Fact]
    public void Example2()
    {
        const string history = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
        var fileSystem = Lexer.DoThing(history);
        var size = DirectorySizeCounter.CountSize(fileSystem);
        
        const long totalDiskSize = 70000000;
        var spaceTaken = DirectorySizeCounter.CalculateSpaceTake(fileSystem);
        var spaceNeeded = totalDiskSize - spaceTaken;
        
        var sum = size.Values
            .Where(x => x >= spaceNeeded);

        var minimumSize = sum.Min();
        Assert.Equal(24933642, minimumSize);
    }
    
    [Fact]
    public void Puzzle1()
    {
        string history = FileReader.GetResource("2022", "7");
        var fileSystem = Lexer.DoThing(history);
        var size = DirectorySizeCounter.CountSize(fileSystem);
        
        
        var sum = size.Values.Where(x=> x <= 100000).Sum();
        Assert.Equal(1845346, sum);
    }
    
    [Fact]
    public void Puzzle2()
    {
        string history = FileReader.GetResource("2022", "7");
        var fileSystem = Lexer.DoThing(history);
        var size = DirectorySizeCounter.CountSize(fileSystem);
        
        const long totalDiskSize = 70000000;
        var spaceTaken = DirectorySizeCounter.CalculateSpaceTake(fileSystem);
        var spaceLeft = totalDiskSize - spaceTaken;
        long thing = 30000000 - spaceLeft;

        
        var sum = size.Values
            .Where(x => x >= thing);

        var minimumSize = sum.Min();
        Assert.Equal(3636703, minimumSize);
    }
}