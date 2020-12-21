using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day19
{
    /*
        Tile 2311:
        ..##.#..#.
        ##..#.....
        #...##..#.
        ####.#...#
        ##.##.###.
        ##...#.###
        .#.#.#..##
        ..#....#..
        ###...#.#.
        ..###..###
     */
    public static class TileParser
    {
        public static List<Tile> Parse(string input)
        {
            var tiles = new List<Tile>();
            var numberRegex = new Regex("[0-9]+");

            foreach (var tileSet in input.Split(Environment.NewLine+Environment.NewLine))
            {
                var values = tileSet.Split(Environment.NewLine);
                var id = long.Parse(numberRegex.Match(values[0]).Value);

                var y = 0;
                
                var spaceList = new List<Space>();
                foreach (var lines in values[1..])
                {
                    spaceList.AddRange(lines.Select((item, x) => item switch
                    {
                        '#' => new Space(x, y, true),
                        _ => new Space(x, y, false)
                    }));
                    
                    y++;
                }

                tiles.Add(new Tile(id, spaceList));
            }

            return tiles;
        }
    }
}