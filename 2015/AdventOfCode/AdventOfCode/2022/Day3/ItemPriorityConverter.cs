using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022.Day3;

public static class ItemPriorityConverter
{
    private static readonly Dictionary<string, long> ItemPriority
        = GetItemPriorities();
    private static Dictionary<string, long> GetItemPriorities()
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        var dictionary = new Dictionary<string, long>();
        for (var i = 0; i < alphabet.Length; i++) 
            dictionary.Add(alphabet[i].ToString(), i+1);

        return dictionary;
    }

    public static long GetSumOfSharedItems(string input)
    {
        var splitPoint = input.Length/ 2;
        var firstCompartment = input[..splitPoint];
        var secondCompartment = input[splitPoint..];

        var distinct = (from item in firstCompartment
            where secondCompartment.Contains(item)
            select item).Distinct();

        return distinct.Select(item => item.ToString())
            .Select(itemString => ItemPriority[itemString])
            .Sum();
    }


    public static long Thing(string[] lines)
    {
        var sum = 0L;
        for (var i = 0; i < lines.Length; i += 3)
        {
            var endRange = i + 3 >= lines.Length ? lines.Length : i + 3;
            var groupy = lines[i..endRange]
                .ToArray();

            var sharedBetween =
                (from item in groupy[0]
                    where groupy[1].Contains(item) && groupy[2].Contains(item)
                    select item.ToString()).Distinct().ToList();

            sum += sharedBetween.Sum(x => ItemPriority[x]);
        }

        return sum;
    }
}