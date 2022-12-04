using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day4;

public static class SectorParser
{
    private static Sector ParseSector(string value)
    {
        var parts = value.SplitByDash();
        return new Sector(int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    public static bool OneCoversTheOther(string pair)
    {
        var sectors = pair.SplitByComma();
        var (firstSector, secondSector) = (ParseSector(sectors[0]), ParseSector(sectors[1]));

        if (secondSector.Start >= firstSector.Start && secondSector.End <= firstSector.End)
            return true;
        
        return firstSector.Start >= secondSector.Start && firstSector.End <= secondSector.End;
    }

    private static bool OneSharesSector(string pair)
    {
        var sectors = pair.SplitByComma();
        var (firstSector, secondSector) = (ParseSector(sectors[0]), ParseSector(sectors[1]));
        
        if (secondSector.Start <= firstSector.End && secondSector.End >= firstSector.Start)
            return true;

        return false;
    }

    public static long Puzzle1Solver(IEnumerable<string> input)
    {
        return input.LongCount(OneCoversTheOther);
    }

    public static long Puzzle2Solver(IEnumerable<string> input)
    {
        return input.LongCount(OneSharesSector);
    }
}

public record Sector(int Start, int End);