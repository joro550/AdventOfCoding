using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day5;

public static class StackParser
{
    public static Dictionary<int, Thing> Parse(string input)
    {
        var lines = input.SplitByNewLine().Take(..^1).ToArray();

        while (lines.Any(line => line.Contains("   ")))
        {
            lines = lines.Select(x => x.Replace("    [", "\t [").Replace("]    ", "] \t").Replace("     ", " \t "))
                .ToArray();
        }
        
        var count = lines[0].SplitBySpace().Length;
        var dictionary = CreateDictionary(count);

        var enumerable = lines.Reverse();
        foreach (var line in enumerable)
        {
            var stackItems = line.SplitBySpace();
            for (var i = 0; i < count; i++)
            {
                if(stackItems[i] == "\t") continue;
                dictionary[i].Push(stackItems[i][1].ToString());
            }
        }
        
        return dictionary;
    }

    public static List<Instruction> ParseInstructions(string input)
    {
        return (from line in input.SplitByNewLine()
                let inst = line.SplitBySpace()
                select inst[0] switch
                {
                    "move" => MoveInstruction.Parse(line),
                    _ => throw new ArgumentOutOfRangeException()
                }).Cast<Instruction>()
            .ToList();
    }

    private static Dictionary<int, Thing> CreateDictionary(int count)
    {
        var dictionary = new Dictionary<int, Thing>();
        for (var i = 0; i < count; i++)
            dictionary.Add(i, new Thing(new Stack<string>()));
        return dictionary;
    }
}

public record Thing(Stack<string> Names)
{
    public void Push(string item)
    {
        Names.Push(item);
    }
    
    public string Pop()
    {
        return Names.Pop();
    }

    public bool Any => Names.Any();
}

public abstract record Instruction
{
    public abstract Dictionary<int, Thing> Execute(Dictionary<int, Thing> things);
    public abstract Dictionary<int, Thing> Execute2(Dictionary<int, Thing> things);
}

public record MoveInstruction(int Amount, int FromIndex, int ToIndex) : Instruction
{
    public static MoveInstruction Parse(string line)
    {
        var info = line.SplitBySpace();
        return new MoveInstruction(int.Parse(info[1]), int.Parse(info[3]), int.Parse(info[5]));
    }

    public override Dictionary<int, Thing> Execute(Dictionary<int, Thing> things)
    {
        for (var i = 0; i < Amount; i++)
        {
            if (!things[FromIndex-1].Any)
                return things;
            
            
            things[ToIndex-1].Push(things[FromIndex-1].Pop());
        }

        return things;
    }
    
    public override Dictionary<int, Thing> Execute2(Dictionary<int, Thing> things)
    {
        var list = new List<string>();
        for (var i = 0; i < Amount; i++)
        {
            if (!things[FromIndex-1].Any)
                continue;
            list.Add(things[FromIndex-1].Pop());
        }

        list.Reverse();
        foreach (var name in list)
        {
            things[ToIndex-1].Push(name);
        }

        return things;
    }
}


public static class Executor
{
    public static Dictionary<int, Thing> Execute(IEnumerable<Instruction> instructions, Dictionary<int,Thing> things)
    {
        return instructions.Aggregate(things, (current, instruction) => instruction.Execute(current));
    }
    public static Dictionary<int, Thing> Execute2(IEnumerable<Instruction> instructions, Dictionary<int,Thing> things)
    {
        return instructions.Aggregate(things, (current, instruction) => instruction.Execute2(current));
    }
}