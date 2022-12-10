using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day10;

public class CycleCounter
{
    public static Dictionary<int, int> Parse(string input)
    {
        var operations = Operations(input);

        var crt = new Crt(1, 1);
        var dictionary = new Dictionary<int, int> {{1,1}};
        foreach (var operation in operations)
        {
            var list = operation.Execute(crt);
            
            foreach (var item in list)
            {
                dictionary.Add(item.Cycle, item.Value);
                crt = item;
            }
        }

        return dictionary;
    }
    
    public static string Parse2(string input)
    {
        var operations = Parse(input);
        
        string crt = string.Empty;
        for (int i = 1; i <= 6; i++)
        {
            crt += Crt2.CreateLine(i, operations) + Environment.NewLine;
        }
        
        return crt;
    }

    private static IEnumerable<Operation> Operations(string input)
    {
        var operations = input.SplitByNewLine()
            .Select(line => line.SplitBySpace())
            .Select(lineParts => (Operation)(lineParts[0] switch
            {
                "noop" => new NoOpOperation(),
                "addx" => new AddOperation(int.Parse(lineParts[1])),
                _ => throw new ArgumentOutOfRangeException()
            }));
        return operations;
    }
}

internal record Crt2
{
    private List<string> _line;


    public static string CreateLine(int line, Dictionary<int, int> cycles)
    {
        var position = 0;
        string lineString = string.Empty;

        var start = (line - 1) * 40 + 1;
        for (var i = start; i < line * 40; i++, position++)
        {
            var cyclePosition = cycles[i];
            var startSprite = cyclePosition - 1;
            var endSprite = cyclePosition + 1;

            lineString += position >= startSprite && position <= endSprite ? "#" : ".";
        }
        
        return lineString;
    }
}




internal record Crt(int Cycle, int Value);


internal abstract record Operation(int Cost)
{
    public abstract List<Crt> Execute(Crt crt);
    public abstract IEnumerable<Crt> Execute2(Crt crt);
}

file record NoOpOperation() : Operation(1)
{
    public override List<Crt> Execute(Crt crt)
    {
        var list = new List<Crt>();
        var localCrt = crt;
        
        for (var i = 0; i < Cost; i++)
        {
            localCrt = localCrt with { Cycle = localCrt.Cycle + 1 };
            list.Add(localCrt);
        }

        return list;
    }

    public override IEnumerable<Crt> Execute2(Crt crt)
    {
        var localCrt = crt;
        
        for (var i = 0; i < Cost; i++)
        {
            localCrt = localCrt with { Cycle = localCrt.Cycle + 1 };
            yield return localCrt;
        }
    }
}


file record AddOperation(int Value) : Operation(2)
{
    public override List<Crt> Execute(Crt crt)
    {
        var list = new List<Crt>();
        var localCrt = crt;
        
        for (var i = 0; i < Cost-1; i++)
        {
            localCrt = localCrt with { Cycle = localCrt.Cycle + 1 };
            list.Add(localCrt);
        }
        
        list.Add(new Crt(localCrt.Cycle + 1, localCrt.Value + Value));

        return list;
    }

    public override IEnumerable<Crt> Execute2(Crt crt)
    {
        var localCrt = crt;
        
        for (var i = 0; i < Cost-1; i++)
        {
            localCrt = localCrt with { Cycle = localCrt.Cycle + 1 };
            yield return localCrt;
        }
        yield return new Crt(localCrt.Cycle + 1, localCrt.Value + Value);
    }
}

