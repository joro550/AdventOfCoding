using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day5
{
    public class Vents
    {
        private List<Vent> _vents;
        private Vents(List<Vent> vents) => _vents = vents;

        public static Vents ParseValidLines(string input)
        {
            var vents = input.Split(Environment.NewLine)
                .Select(Vent.Parse)
                .Where(vent => vent.IsValid())
                .ToList();
            return new Vents(vents);
        }
        
        public static Vents ParseAllLines(string input)
        {
            var vents = input.Split(Environment.NewLine)
                .Select(Vent.Parse)
                .ToList();
            return new Vents(vents);
        }

        public Map CreateMap()
        {
            var map = new Map();
            foreach (var coords in _vents.Select(vent => vent.GetPointsAffected2()))
            {
                foreach (var (x, y) in coords)
                {
                    map.AddPoint(x, y);
                }
            }

            return map;
        }
    }

    public record Map
    {
        private readonly Dictionary<Coords, int> _mapPoints = new();

        public void AddPoint(int x, int y)
        {
            var coords = new Coords(x, y);
            if (_mapPoints.ContainsKey(coords))
                _mapPoints[coords]++;
            else
                _mapPoints.Add(coords, 1);
        }

        public int GetValue(int x, int y)
        {
            var coords = new Coords(x, y);
            return _mapPoints.ContainsKey(coords) ? _mapPoints[coords] : 0;
        }

        public long Count(int value)
        {
            long count = 0;
            foreach (var (_, v) in _mapPoints)
            {
                if (v >= value)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public record Vent(Coords Start, Coords End)
    {
        public static Vent Parse(string input)
        {
            var thing = input.Split(" ");
            var start = Coords.Parse(thing[0]);
            var end = Coords.Parse(thing[2]);
            return new Vent(start, end);
        }

        public bool IsHorizontal() => Start.X == End.X;
        public bool IsVertical() => Start.Y == End.Y;
        public bool IsValid() => IsHorizontal() || IsVertical();

        public List<Coords> GetPointsAffected()
        {
            var pointsAffected = new List<Coords>();
            var startAt = Start < End ? Start : End;
            var endAt = Start > End ? Start : End;

            var isHorizontal = IsHorizontal();

            for (int i = isHorizontal ? startAt.Y : startAt.X,
                 count = 0; 
                 i <= (isHorizontal ? endAt.Y : endAt.X); 
                 i++, count ++)
            {
                var coordsToAdd = isHorizontal
                    ? new Coords(startAt.X, startAt.Y + count)
                    : new Coords(startAt.X + count, startAt.Y);
                pointsAffected.Add(coordsToAdd);
            }

            return pointsAffected;
        }
        public List<Coords> GetPointsAffected2()
        {
            var pointsAffected = new List<Coords>();

            var thing = new Func<int, int, int>(
                (val, expect) => val < expect ? val + 1 : val == expect ? val : val - 1);

            var coord = Start;
            pointsAffected.Add(coord);
            while (coord != End)
            {
                var newX = thing(coord.X, End.X);
                var newY = thing(coord.Y, End.Y);
                coord = new Coords(newX, newY);
                pointsAffected.Add(coord);
            }
            return pointsAffected;
        }
    }

    public record Coords(int X, int Y)
    {
        public static Coords Parse(string input)
        {
            var numbers = input.Split(",");
            return new Coords(
                int.Parse(numbers[0]), int.Parse(numbers[1]));
        }

        public static bool operator >(Coords left, Coords right) 
            => left.X > right.X || left.Y > right.Y;

        public static bool operator <(Coords left, Coords right) 
            => left.X < right.X || left.Y < right.Y;
    }
}