using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace AdventOfCode.Tests._2022.Day13;

public class Day13Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day13Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ListParserAsInteger()
    {
        var input = "[1]";
        var tokens = TokenParser.Parse(input);
        var lst = LstParser.Parse(tokens);
        var actual = lst[0] as Integer;
        Assert.Equal(1, actual!.GetValue());
    }
    
    [Fact]
    public void ListParserAsList()
    {
        var input = "[[1]]";
        var tokens = TokenParser.Parse(input);
        var lst = LstParser.Parse(tokens);
        var actual = lst[0] as Lst;
        var integer = actual![0] as Integer;
        Assert.Equal(1, integer!.GetValue());
    }
    
    [Fact]
    public void ListComparision()
    {
        Lst left = "[1,1,4,1,1]".ToLst();
        Lst right = "[1,1,5,1,1]".ToLst();
        var comparison = left.Compare(right);
        Assert.Equal(Thing.Lower, comparison);
    }
    
    
    [Fact]
    public void ListTest()
    {
        Lst left = "[10,[10],2]".ToLst();
        Assert.Equal(10, (left[0] as Integer).GetValue());
        Assert.Equal(10, ((left[1] as Lst)[0] as Integer).GetValue());
        Assert.Equal(2, (left[2] as Integer).GetValue());
    }

    [Fact]
    public void Example1()
    {
        var input = FileReader.GetExample("2022", "13")
            .SplitByDoubleNewLine();

        var things = new List<Thing>();
        for (var index = 0; index < input.Count; index++)
        {
            var t = input[index];
            var thing = t.SplitByNewLine();
            var left = thing[0].ToLst();
            var right = thing[1].ToLst();
            things.Add(left.Compare(right));
        }

        var sum = 0;
        for (int i = 0; i < things.Count; i++)
        {
            if(things[i] == Thing.Lower)
                sum += i + 1;
        }

        Assert.Equal(13, sum);
    }
    
    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "13")
            .SplitByDoubleNewLine();

        var comparisons = (from t in input 
            select t.SplitByNewLine() into line 
            let left = line[0].ToLst() 
            let right = line[1].ToLst() 
            select left.Compare(right)).ToList();

        var sum = 0;
        for (int i = 0; i < comparisons.Count; i++)
        {
            if(comparisons[i] == Thing.Lower)
                sum += i + 1;
        }

        Assert.Equal(5350, sum);
    }
    
    [Fact]
    public void Example2()
    {
        var input = FileReader.GetExample("2022", "13")
            .SplitByNewLine()
            .Where(x =>!string.IsNullOrEmpty(x));
        var lists = input.Select(blah => blah.ToLst()).ToList();

        var firstFalg = "[[2]]".ToLst();
        var secondFalg = "[[6]]".ToLst();
        lists.Add(firstFalg);
        lists.Add(secondFalg);
        lists.Sort();

        int firstFlagIndex = -1;
        int secondFlagIndex = -1;
        for (int i = 0; i < lists.Count; i++)
        {
            if (lists[i] == firstFalg)
                firstFlagIndex = i + 1;
            if (lists[i] == secondFalg)
                secondFlagIndex = i+1;
        }

        foreach (var l in lists)
        {
            _testOutputHelper.WriteLine(l.Print());
        }

        Assert.Equal(140, firstFlagIndex * secondFlagIndex);
    }
    
    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "13")
            .SplitByNewLine()
            .Where(x =>!string.IsNullOrEmpty(x));
        var lists = input.Select(blah => blah.ToLst()).ToList();

        var firstFalg = "[[2]]".ToLst();
        var secondFalg = "[[6]]".ToLst();
        lists.Add(firstFalg);
        lists.Add(secondFalg);
        lists.Sort();

        int firstFlagIndex = -1;
        int secondFlagIndex = -1;
        for (int i = 0; i < lists.Count; i++)
        {
            if (lists[i] == firstFalg)
                firstFlagIndex = i +1;
            if (lists[i] == secondFalg)
                secondFlagIndex = i +1;
        }

        Assert.Equal(19570, firstFlagIndex * secondFlagIndex);
    }
}

file static class StringExtensions
{
    public static Lst ToLst(this string value)
    {
        var tokens = TokenParser.Parse(value);
        return LstParser.Parse(tokens);
    }
}

file record LstParser
{
    public static Lst Parse(Queue<Token> tokens)
    {
        var thing = tokens.Dequeue();
        return ParseValues(tokens);
    }

    private static Lst ParseValues(Queue<Token> tokens)
    {
        var lst = new Lst();
        while (tokens.TryDequeue(out var value))
        {
            if (value is Digit d)
            {
                lst.Add(new Integer(d.Value));
            }
            else if (value is OpenList)
            {
                lst.Add(ParseValues(tokens));
            }
            else if (value is CloseList)
            {
                return lst;
            }
        }

        return lst;
    }
}

file abstract record Val
{
    public abstract Thing Compare(Integer value);
    public abstract Thing Compare(Lst  value);
    public abstract string Print();
}

file record Integer : Val
{
    private readonly int _val;

    public Integer(int val) => _val = val;

    public override Thing Compare(Integer value)
    {
        if (_val < value._val)
            return Thing.Lower;
        return _val > value._val 
            ? Thing.Higher 
            : Thing.Equal;
    }

    public override Thing Compare(Lst value)
    {
        var lst = new Lst();
        lst.Add(this);
        return lst.Compare(value);
    }

    public override string Print() => _val.ToString();

    public int GetValue() => _val;
}

file record Lst : Val, IComparable<Lst>
{
    private List<Val> Values 
        = new();

    private Val Take(int i) => i >= Count ? null : Values[i];

    public Val this[int index]
    {
        get => Values[index];
        set => Values[index] = value;
    }

    public int Count => Values.Count;
    public void Add(Val parse) => Values.Add(parse);
    
    public override Thing Compare(Integer value)
    {
        var lst = new Lst();
        lst.Add(value);
        return Compare(lst);
    }

    public override Thing Compare(Lst value)
    {
        var max = Math.Max(Count, value.Count);
        for (int i = 0; i < max; i++)
        {
            var ourValue = Take(i);
            var theirValue = value.Take(i);

            if (ourValue == null && theirValue == null)
                return Thing.Equal;
            if (ourValue == null)
                return Thing.Lower;
            if (theirValue == null)
                return Thing.Higher;

            var compare = theirValue switch
            {
                Integer b => ourValue.Compare(b),
                Lst l => ourValue.Compare(l)
            };

            if (compare != Thing.Equal)
                return compare;
        }

        return Thing.Equal;
    }

    public int CompareTo(Lst other)
    {
        return Compare(other) switch
        {
            Thing.Equal => 0,
            Thing.Higher => 1,
            Thing.Lower => -1,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override string Print()
    {
        var val = Values.Aggregate("[", (current, value) => current + value.Print() + ",");
        return val + "]";
    }
}

public enum Thing
{
    Lower,
    Higher, 
    Equal
}

file record TokenParser
{
    public static Queue<Token> Parse(string input)
    {
        var queue = new Queue<Token>();
        
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i].ToString();
            if (c == "[")
            {
                queue.Enqueue(new OpenList());
            }
            else if (c == "]")
            {
                queue.Enqueue(new CloseList());
            }
            else
            {
                if (!char.IsDigit(input[i])) 
                    continue;

                var numberString = input[i].ToString();
                
                while(char.IsDigit(input[i+1]))
                {
                    i++;
                    numberString += input[i];
                    
                } 

                queue.Enqueue(new Digit(int.Parse(numberString)));
            }
        }

        return queue;
    }
}


file record Token;

file record OpenList : Token;
file record CloseList : Token;
file record Digit(int Value) : Token;