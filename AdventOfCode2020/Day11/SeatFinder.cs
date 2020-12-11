using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day11
{
    public class SeatFinder
    {
        private readonly List<Rule> _rules
            = new()
            {
                new EmptyWithNoOccupiedRule(),
                new OccupiedRule(4)
            };
        
        private readonly List<Rule> _secondRules
            = new()
            {
                new EmptyWithNoOccupiedRule(),
                new OccupiedRule(5)
            };

        public Map Execute(Map spaces)
        {
            var newMap = new Map();
            for (var y = 0; y < spaces.Columns.Count; y++)
            {
                var row = new Row();
                for (var x = 0; x < spaces.Columns[y].Rows.Count; x++)
                {
                    var seat = spaces.Columns[y].Rows[x];
                    foreach (var rule in _rules.Where(rule => rule.DoesRuleApply(spaces.Columns[y].Rows[x])))
                        seat = rule.Execute(spaces.Columns[y].Rows[x], rule.GetAdjacentSpaces(spaces, x, y));
                    
                    row.Rows.Add(seat);
                }
                newMap.Columns.Add(row);
            }
            
            return newMap;
        }

        public Map Execute2(Map spaces)
        {
            var newMap = new Map();
            for (var y = 0; y < spaces.Columns.Count; y++)
            {
                var row = new Row();
                for (var x = 0; x < spaces.Columns[y].Rows.Count; x++)
                {
                    var seat = spaces.Columns[y].Rows[x];
                    foreach (var rule in _secondRules.Where(rule => rule.DoesRuleApply(spaces.Columns[y].Rows[x])))
                        seat = rule.Execute(spaces.Columns[y].Rows[x], rule.GetAdjacentSeats(spaces, x, y));
                    
                    row.Rows.Add(seat);
                }
                newMap.Columns.Add(row);
            }
            
            return newMap;
        }
    }

    public record EmptyWithNoOccupiedRule : Rule
    {
        public override bool DoesRuleApply(Space space) => space is EmptySeat;
        
        public override Space Execute(Space space, IEnumerable<Space> spaces)
        {
            return space is EmptySeat && spaces.All(s => s is not OccupiedSeat) 
                ? new OccupiedSeat() 
                : space;
        }
    }
    
    public record OccupiedRule : Rule
    {
        private readonly int _leastAmountOfSeats;

        public OccupiedRule(int leastAmountOfSeats)
        {
            _leastAmountOfSeats = leastAmountOfSeats;
        }
        
        public override bool DoesRuleApply(Space space) => space is OccupiedSeat;
        
        public override Space Execute(Space space, IEnumerable<Space> spaces)
        {
            if (space is not OccupiedSeat)
                return space;

            var count = spaces.Count(x => x is OccupiedSeat);
            return count >= _leastAmountOfSeats ? new EmptySeat() : space;
        }
    }

    public abstract record Rule
    {
        public abstract bool DoesRuleApply(Space space);
        public abstract Space Execute(Space space, IEnumerable<Space> spaces);

        public IEnumerable<Space> GetAdjacentSpaces(Map spaces, int x, int y)
        {
            return new List<Space>()
            {
                // top left
                spaces.GetCoords(x - 1, y - 1),
                // top middle
                spaces.GetCoords(x, y - 1),
                //top right
                spaces.GetCoords(x + 1, y - 1),

                // middle left
                spaces.GetCoords(x - 1, y),
                // middle right
                spaces.GetCoords(x + 1, y),

                // bottom left
                spaces.GetCoords(x - 1, y + 1),
                // bottom middle
                spaces.GetCoords(x, y + 1),
                // bottom right
                spaces.GetCoords(x + 1, y + 1),
            };
        }

        public IEnumerable<Space> GetAdjacentSeats(Map spaces, int x, int y)
        {
            var directions = new List<Func<int, int, (int, int)>>
            {
                // top left
                (ix, iy) => (ix - 1, iy - 1),
                // top middle
                (ix, iy) => (ix, iy - 1),
                //top right
                (ix, iy) => (ix + 1, iy - 1),

                // middle left
                (ix, iy) => (ix - 1, iy),
                // middle right
                (ix, iy) =>(ix + 1, iy),

                // bottom left
                (ix, iy) =>(ix - 1, iy + 1),
                // bottom middle
                (ix, iy) =>(ix, iy + 1),
                // bottom right
                (ix, iy) =>(ix + 1, iy + 1),
            };
            
            var returnSpaces = new List<Space>();

            foreach (var direction in directions)
            {
                Space space;
                var coords = (x, y);

                do
                {
                    coords = direction(coords.Item1, coords.Item2);
                    space = spaces.GetCoords(coords.Item1, coords.Item2);

                } while (space is Floor);

                returnSpaces.Add(space);
            }
            return returnSpaces;
        }
    }
}