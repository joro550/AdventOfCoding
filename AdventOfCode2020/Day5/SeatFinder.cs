using System;
using System.Linq;

namespace AdventOfCode2020.Day5
{
    public static class SeatFinder
    {
        public static Seat FindSeat(string input, int maxRows, int maxColumns)
        {
            var row = GetRow(input[..7], maxRows);
            var column = GetColumn(input[7..], maxColumns);
            return new Seat(row, column);
        }

        private static int GetRow(string input, int maxRows)
        {
            var rows = new Range(0, maxRows -1);
            rows = input[..^1].Aggregate(rows, (current, c) => current.ProcessCharacter(c));
            return rows.GetRow(input[^1]);
        }
        
        private static int GetColumn(string input, int maxColumns)
        {
            var columns = new Range(0, maxColumns -1);
            columns = input[..^1].Aggregate(columns, (current, character) => current.ProcessCharacter(character));
            return columns.GetColumn(input[^1]);
        }
    }

    public record Range(int Lower, int Higher)
    {
        private static int Add(Range range) 
            => range.Lower + range.Higher;

        private static Range HalfUpperQuadrant(Range range)
            => range with {Higher = (int) Math.Round(Add(range) / 2d, MidpointRounding.ToNegativeInfinity) };

        private static Range HalfLowerQuadrant(Range range)
            => range with {Lower = (int) Math.Round(Add(range) / 2d, MidpointRounding.ToPositiveInfinity) };

        public Range ProcessCharacter(char character) =>
            character switch
            {
                'F' => HalfUpperQuadrant(this),
                'B' => HalfLowerQuadrant(this),
                'L' => HalfUpperQuadrant(this),
                'R' => HalfLowerQuadrant(this),
                _ => new Range(Lower, Higher)
            };

        public int GetRow(char character) =>
            character switch
            {
                'F' => Lower,
                'B' => Higher,
                _ => 0,
            };

        public int GetColumn(char character) =>
            character switch
            {
                'L' => Lower,
                'R' => Higher,
                _ => 0,
            };
    }

    public record Seat(int Row, int Column)
    {
        public int GetSeatId() 
            => Row * 8 + Column;
    }
}