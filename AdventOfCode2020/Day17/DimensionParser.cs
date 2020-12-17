using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day17
{
    public static class DimensionParser
    {
        public static Dimension Parse(string input)
        {
            var cubes = new List<Cube>();
            
            var y = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                var x = 0;
                foreach (var active in line.Select(character => character switch
                    {
                        '#' => true,
                        _ => false
                    }))
                {
                    cubes.Add(new Cube(x, y, 0, active));
                    x++;
                }

                y++;
            }
            
            return new Dimension(cubes);
        }
    }

    public record Dimension(List<Cube> Cubes)
    {
        private List<Rule> Rules = new()
        {
            new ActiveRule(),
            new InactiveRule()
        };
        
        public Cube this[int x, int y, int z]
        {
            get
            {
                var cube = Cubes.SingleOrDefault(c => c.X == x && c.Y == y && c.Z == z);
                if (cube != null) 
                    return cube;

                return new Cube(x, y, z, false);
            }
        }

        private void CreateIfNotExists(int x, int y, int z)
        {
            var cube = Cubes.SingleOrDefault(c => c.X == x && c.Y == y && c.Z == z);
            if (cube != null)
                return;
                
            cube = new Cube(x, y, z, false);
            Cubes.Add(cube);
        }

        public Dimension Simulate()
        {
            var cubes = new List<Cube>();

            //Create neighbours
            foreach (var cube in Cubes.ToArray())
            {
                foreach (var (x, y, z) in cube.GetNeighbourCoordinates())
                {
                    CreateIfNotExists(x, y, z);
                }
            }
            
            foreach (var cube in Cubes)
            {
                foreach (var rule in Rules.Where(r => r.ShouldApply(cube)))
                {
                    cubes.Add(rule.Apply(this, cube));
                }
            }

            return this with { Cubes = cubes};
        }

        public string PrintToString(int[] zIndexes)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var zIndex in zIndexes)
            {
                var maxX = Cubes.Where(c => c.Z == zIndex).Max(x => x.X);
                var maxY = Cubes.Where(c => c.Z == zIndex).Max(x => x.Y);

                stringBuilder.AppendLine(zIndex.ToString());
                for (var yIndex = 0; yIndex < maxY; yIndex++)
                {
                    for (var xIndex = 0; xIndex < maxX; xIndex++)
                    {
                        stringBuilder.Append(this[xIndex, yIndex, zIndex].GetString());
                    }

                    stringBuilder.AppendLine();
                }
                
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }

    public record InactiveRule : Rule
    {
        public override bool ShouldApply(Cube cube) 
            => !cube.Active;

        public override Cube Apply(Dimension dimension, Cube cube)
        {
            if (cube.Active)
                return cube;
                    
            var activeNeighbours = GetNeighbours(dimension, cube)
                .Count(x => x.Active);
            if (activeNeighbours == 3)
                return cube with {Active = true};
            return cube;
        }
    }

    public record ActiveRule : Rule
    {
        public override bool ShouldApply(Cube cube) 
            => cube.Active;

        public override Cube Apply(Dimension dimension, Cube cube)
        {
            var activeNeighbours = GetNeighbours(dimension, cube)
                .Count(x => x.Active);

            if (activeNeighbours < 2 || activeNeighbours > 3)
                return cube with {Active = false};

            return cube;
        }
    }

    public abstract record Rule
    {
        public abstract bool ShouldApply(Cube cube);
        public abstract Cube Apply(Dimension dimension, Cube cube);

        protected static IEnumerable<Cube> GetNeighbours(Dimension dimension, Cube cube)
        {
            return cube.GetNeighbourCoordinates()
                .Select(coordinate => dimension[coordinate.X, coordinate.Y, coordinate.Z])
                .ToArray();
        }
    }
    

    public record Cube(int X, int Y, int Z, bool Active)
    {
        public string GetString() 
            => Active ? "#" : ".";

        public Position[] GetNeighbourCoordinates()
        {
            return new Position[]
            {
                new (X-1, Y, Z),
                new (X+1, Y, Z),
                
                new (X, Y-1, Z),
                new (X, Y+1, Z),
                
                new (X, Y, Z-1),
                new (X, Y, Z+1),
                
                new (X-1, Y-1, Z),
                new (X-1, Y+1, Z),
                new (X+1, Y-1, Z),
                new (X+1, Y+1, Z),
                
                new (X-1, Y, Z-1),
                new (X-1, Y, Z+1),
                new (X+1, Y, Z-1),
                new (X+1, Y, Z+1),
                
                new (X, Y-1, Z-1),
                new (X, Y-1, Z+1),
                new (X, Y+1, Z-1),
                new (X, Y+1, Z+1),
                
                new (X-1, Y-1, Z-1),
                new (X-1, Y+1, Z+1),
                new (X-1, Y-1, Z+1),
                new (X-1, Y+1, Z-1),
                
                new (X+1, Y+1, Z+1),
                new (X+1, Y-1, Z-1),
                new (X+1, Y+1, Z-1),
                new (X+1, Y-1, Z+1),
            };
        } 
    }

    public record Position(int X, int Y, int Z);
}