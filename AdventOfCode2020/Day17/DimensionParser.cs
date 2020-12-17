using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public static class DimensionParser
    {
        public static Dimension Parse3D(string input)
        {
            var cubes = new HashSet<Cube>();
            
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
                    cubes.Add(new Cube(new Position(x, y, 0), active, false));
                    x++;
                }

                y++;
            }
            
            return new Dimension(cubes);
        }
        
        public static Dimension Parse4D(string input)
        {
            var cubes = new HashSet<Cube>();
            
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
                    cubes.Add(new FourDimensionCube(new FourDPosition(x, y, 0, 0), active, false));
                    x++;
                }

                y++;
            }
            
            return new Dimension(cubes);
        }
    }
    
    public record Dimension(HashSet<Cube> Cubes)
    {
        private readonly List<Rule> _rules = new()
        {
            new ActiveRule(),
            new InactiveRule()
        };

        private Cube CreateIfNotExists(Position position) 
            => Cubes.Any(c => c.Position == position) ? null : Cube.CreateCube(position);

        public Dimension Simulate()
        {
            var cubes = new HashSet<Cube>();
            
            //Create neighbours
            GenerateNeighbours();

            foreach (var cube in Cubes)
            {
                foreach (var rule in _rules.Where(r => r.ShouldApply(cube)))
                {
                    cubes.Add(rule.Apply(this, cube));
                }
            }

            return this with { Cubes = cubes};
        }

        private void GenerateNeighbours()
        {
            var cubesWithoutGeneratedNeighbours = Cubes.Where(c => !c.HasGeneratedNeighbours).ToArray();
            
            var seenPositions = new HashSet<Position>();
            
            for (var index = 0; index < cubesWithoutGeneratedNeighbours.Length; index++)
            {
                var cube = cubesWithoutGeneratedNeighbours[index];
                cubesWithoutGeneratedNeighbours[index] = cube with {HasGeneratedNeighbours = true};

                foreach (var position in cube.GetNeighbourCoordinates())
                {
                    if(seenPositions.Any(p => p == position))
                        continue;
                    
                    seenPositions.Add(position);
                    var cubeToAdd = CreateIfNotExists(position);
                    if (cubeToAdd == null)
                        continue;
                    Cubes.Add(cubeToAdd);
                }
            }
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
                    
            var activeNeighbours = GetNeighboursInRange(dimension, cube)
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
            var activeNeighbours = GetNeighboursInRange(dimension, cube)
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
        
        protected static IEnumerable<Cube> GetNeighboursInRange(Dimension dimension, Cube cube)
        {
            var range = cube.GetRange();
            var neighbours = dimension.Cubes.Where(c =>
            {
                var position = c.Position;

                return position >= range.Min &&
                       position <= range.Max &&
                       c != cube;
            });
            return neighbours;
        }
    }
    
    public record Cube(Position Position, bool Active, bool HasGeneratedNeighbours)
    {
        protected readonly HashSet<Position> NeighbourPositions 
            = new();
        
        public static Cube CreateCube(Position p)
        {
            return p switch
            {
                FourDPosition position => new FourDimensionCube(position, false, false),
                _ => new Cube(p, false, false)
            };
        }

        public (Position Min, Position Max) GetRange()
        {
            var minPosition = Position - 1;
            var maxPosition = Position + 1;
            return (minPosition, maxPosition);
        } 

        public virtual IEnumerable<Position> GetNeighbourCoordinates()
        {
            if (NeighbourPositions.Any()) 
                return NeighbourPositions;

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var z = 0; z < 3; z++)
                    {
                        var newX = x == 0 ? -1 : x == 1 ? 0 : 1;
                        var newY = y == 0 ? -1 : y == 1 ? 0 : 1;
                        var newZ = z == 0 ? -1 : z == 1 ? 0 : 1;
                        NeighbourPositions.Add(new Position(Position.X + newX, Position.Y + newY, Position.Z + newZ));
                    }
                } 
            }
            return NeighbourPositions;
        }
    }
    
    public record FourDimensionCube(Position Position, bool Active, bool HasGeneratedNeighbours)
        :Cube(Position, Active, HasGeneratedNeighbours)
    {
        public override IEnumerable<Position> GetNeighbourCoordinates()
        {
            if (NeighbourPositions.Any()) 
                return NeighbourPositions;
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var z = 0; z < 3; z++)
                    {
                        for (var w = 0; w < 3; w++)
                        {
                            var newX = x == 0 ? -1 : x == 1 ? 0 : 1;
                            var newY = y == 0 ? -1 : y == 1 ? 0 : 1;
                            var newZ = z == 0 ? -1 : z == 1 ? 0 : 1;
                            var newW = w == 0 ? -1 : w == 1 ? 0 : 1;

                            var position = Position as FourDPosition ?? throw new Exception("not 4d");
                            NeighbourPositions.Add(new FourDPosition(position.X + newX, position.Y + newY, position.Z + newZ, position.W + newW));
                        }
                    }
                } 
            }
            return NeighbourPositions;
        } 
    }

    public record Position(int X, int Y, int Z)
    {
        public static bool operator <=(Position left, Position right) 
            => left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z;

        public static bool operator >=(Position left, Position right) 
            => left.X >= right.X && left.Y >= right.Y && left.Z >=right.Z;

        public static Position operator +(Position left, int num) 
            => left with { X = left.X + num, Y = left.Y + num, Z = left.Z + num};
        
        public static Position operator -(Position left, int num) 
            => left with { X = left.X - num, Y = left.Y - num, Z = left.Z - num};
    }

    public record FourDPosition(int X, int Y, int Z, int W) : Position(X, Y, Z)
    {
        public static bool operator <=(FourDPosition left, FourDPosition right) 
            => left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z && left.W <= right.W;

        public static bool operator >=(FourDPosition left, FourDPosition right) 
            => left.X >= right.X && left.Y >= right.Y && left.Z >= right.Z && left.W >= right.W;
        
        public static FourDPosition operator +(FourDPosition left, int num) 
            => left with { X = left.X + num, Y = left.Y + num, Z = left.Z + num, W = left.W + num};
        
        public static FourDPosition operator -(FourDPosition left, int num) 
            => left with { X = left.X - num, Y = left.Y - num, Z = left.Z - num, W = left.W - num};
    }
}