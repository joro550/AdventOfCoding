using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020.Day11
{
    public class SeatParser
    {
        public static List<Space> Parse(string input)
        {
            var seats = (from line in input.Split(Environment.NewLine)
                from character in line
                select character switch
                {
                    '.' => new Floor(),
                    '#' => new OccupiedSeat(),
                    'L' => new EmptySeat(),
                    _ => new Space()
                }).ToList();

            return seats;
        }

        public static Map Parse2(string input)
        {
            var map = new Map();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var row = new Row();
                foreach (var character in line)
                {
                    row.Rows.Add(
                        character switch
                        {
                            '.' => new Floor(),
                            '#' => new OccupiedSeat(),
                            'L' => new EmptySeat(),
                            _ => new Space()
                        });
                }

                map.Columns.Add(row);
            }

            return map;
        }
    }

    public class Map
    {
        public List<Row> Columns { get; } 
            = new();

        public Space GetCoords(int x, int y)
        {
            if (y < 0 || y >= Columns.Count)
                return new DeadZone();
            
            if (x < 0 || x >= Columns[y].Rows.Count)
                return new DeadZone();

            return Columns[y].Rows[x];
        }

        public List<Space> GetAllSpaces() 
            => Columns.SelectMany(row => row.Rows).ToList();

        public bool Compare(Map other)
        {
            return !Columns.Where((t1, i) => t1.Rows.Where((t, j) => t != other?.Columns[i].Rows[j]).Any()).Any();
        }

        public Map DeepCopy()
        {
            var map = new Map();
            foreach (var rows in Columns.Select(column => column.Rows.ToList()))
            {
                map.Columns.Add(new Row {Rows = rows});
            }

            return map;
        }

        public string Print()
        {
            var sb = new StringBuilder();
            
            
            foreach (var column in Columns)
            {
                foreach (var row in column.Rows)
                {
                    sb.Append(row.Character);
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }

    public class Row
    {
        public List<Space> Rows { get; set; }
            = new();
    }


    public record EmptySeat : Space
    {
        public override string Character => "L";
    }

    public record OccupiedSeat : Space
    {
        
        public override string Character => "#";
    }

    public record Floor : Space
    {
        public override string Character => ".";
    }
    
    
    public record DeadZone : Space
    {
        public override string Character => ".";
    }

    public record Space
    {
        public virtual string Character => ".";
    };

}