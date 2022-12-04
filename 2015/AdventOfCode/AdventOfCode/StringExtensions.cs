using System;
using System.Collections.Generic;

namespace AdventOfCode.Tests;

public static class StringExtensions
{
    public static IEnumerable<string> SplitByDoubleNewLine(this string value)
    {
        return value.Split(Environment.NewLine + Environment.NewLine);
    }
    
    public static string[] SplitByNewLine(this string value)
    {
        return value.Split(Environment.NewLine);
    }
    
    public static string[] SplitBySpace(this string value)
    {
        return value.Split(" ");
    }
    
    public static string[] SplitByComma(this string value)
    {
        return value.Split(",");
    }
    
    public static string[] SplitByDash(this string value)
    {
        return value.Split("-");
    }


    public static string ToString(this string[] values)
    {
        return string.Join(Environment.NewLine, values);
    }
}

