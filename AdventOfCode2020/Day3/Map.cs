using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day3
{
    public class Map
    {
        private readonly List<Row> _rows;
        private Map(List<Row> rows) => _rows = rows;

        public static Map LoadMap(string mapString)
        {
            var rowStrings = mapString.Split(Environment.NewLine);

            var rows = rowStrings.Select(rowString => rowString
                    .Select(space => space switch
                    {
                        '.' => new Space(),
                        '#' => new Tree(),
                        _ => throw new ArgumentOutOfRangeException()
                    })
                    .ToList())
                .Select(spaces => new Row(spaces))
                .ToList();
            
            return new Map(rows);
        }

        public Space GetCoords(int x, int y)
        {
            if (y >= _rows.Count)
                return new Freedom();
            
            while (x >= _rows[y].Spaces.Count)
                x -= _rows[y].Spaces.Count;

            return _rows[y].Spaces[x];
        }
    }

    public record Row(List<Space> Spaces);

    public record Freedom : Space;
    public record Tree : Space;
    public record Space;
}