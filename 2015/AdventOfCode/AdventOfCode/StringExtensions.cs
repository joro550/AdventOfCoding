using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode._2022.Day3;

namespace AdventOfCode.Tests;

public static class StringExtensions
{
    public static List<string> SplitByDoubleNewLine(this string value)
    {
        return value.Split(Environment.NewLine + Environment.NewLine).ToList();
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

    public static List<T> AddWhere<T>(this List<T> list, T item, Func<T, bool> where)
    {
        if (where(item))
        {
            list.Add(item);
        }

        return list;
    }
}

