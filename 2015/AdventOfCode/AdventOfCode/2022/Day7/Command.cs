using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day7;

public abstract class Command
{
    
}

public class ChangeDirectoryCommand : Command
{
    
}

public class ListDirectoryCommand : Command
{
    
}


public record Leaf;

public record Directory(string Name, string Path,  List<Leaf> Leaves) : Leaf
{
    public Directory Previous { get; }

    private Directory(string name, string path, Directory previous) : this(name, path, new List<Leaf>())
    {
        Previous = previous; 
    }
    public Directory(string name, string path) : this(name, path, new List<Leaf>())
    {
        Previous = this;
    }

    public void AddDirectory(string name)
    {
        var path = Path == "/" ? name : Path + $"/{name}";
        Leaves.Add(new Directory(name, path, this));
    }

    public Directory Traverse(string path)
    {
        var steps = path.Split("/");
        var directories = Leaves.OfType<Directory>().ToArray();

        var directory = this;
        return steps.Aggregate(directory, (current, step) => directories.FirstOrDefault(x => x.Name == step) ?? current);
    }
}

public record Files(long Size, string Name) : Leaf;


public class Lexer
{
    public static Directory DoThing(string history)
    {
        var leaves = new Directory("/", "/");
        var currentDirectory = leaves;
        
        foreach (var line in history.SplitByNewLine())
        {
            if (line.StartsWith("$ ls"))
                continue;

            if (line.StartsWith("$ cd"))
            {
                currentDirectory = HandleCommand(line, leaves, currentDirectory);
                continue;
            }

            if (line.StartsWith("dir"))
            {
                var splitBySpace = line.SplitBySpace();
                currentDirectory.AddDirectory(splitBySpace[1]);
            }
            else
            {
                var lineParts = line.SplitBySpace();
                currentDirectory.Leaves.Add(new Files(long.Parse(lineParts[0]), lineParts[1]));
            }
        }

        return leaves;
    }

    private static Directory HandleCommand(string line, Directory directory, Directory currentDirectory)
    {
        var lineParts = line.SplitBySpace();
        if (lineParts[1] != "cd") 
            return currentDirectory;
        
        if (lineParts[2] == "/")
            return directory;

        if (lineParts[2] != "..")
        {
            var temp = currentDirectory;
            var firstOrDefault = currentDirectory.Leaves.OfType<Directory>().FirstOrDefault(x => x.Name == lineParts[2]);
            return firstOrDefault;
        }

        return currentDirectory.Previous;
    }
}

public static class DirectorySizeCounter
{

    public static long CalculateSpaceTake(Directory directory)
    { 
        long sum = directory.Leaves.OfType<Files>().Sum(x => x.Size);
        foreach (var directoryLeaf in directory.Leaves.OfType<Directory>())
        {
            sum += CountDirectorySize(directoryLeaf);
        }

        return sum;
    }
    
    public static Dictionary<string, long> CountSize(Directory directory)
    {
        var dictionary = new Dictionary<string, long>
        {
            { directory.Name, CountDirectorySize(directory) }
        };

        foreach (var d in directory.Leaves.OfType<Directory>())
        {
            var directorySize = CountSize(d);
            foreach (var thing in directorySize)
            {
                dictionary.Add($"{directory.Name}/{thing.Key}", thing.Value);
            }
        }

        return dictionary;
    }

    private static long CountDirectorySize(Directory directory)
    {
        var sum =  directory.Leaves.OfType<Files>().Sum(x => x.Size);
        var directorySize = directory.Leaves.OfType<Directory>().Sum(CountDirectorySize);
        return sum + directorySize;
    }
}
