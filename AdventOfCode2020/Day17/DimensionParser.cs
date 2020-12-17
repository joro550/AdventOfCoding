using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public static class DimensionParser
    {
        public static Dimension Parse3D(string input)
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
        
        public static Dimension2 Parse4D(string input)
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
                    cubes.Add(new FourDimensionCube(x, y, 0, 0, active));
                    x++;
                }

                y++;
            }
            
            return new Dimension2(cubes);
        }
    }

    public record Dimension(List<Cube> Cubes)
    {
        private readonly List<Rule> _rules = new()
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

        private void CreateIfNotExists(Position position)
        {
            var cube = Cubes.SingleOrDefault(c => c.GetPosition() == position);
            if (cube != null)
                return;
            
            Cubes.Add(Cube.CreateCube(position));
        }

        public Dimension Simulate()
        {
            var cubes = new List<Cube>();

            //Create neighbours
            foreach (var cube in Cubes.ToArray())
            {
                foreach(var position in cube.GetNeighbourCoordinates())
                {
                    CreateIfNotExists(position);
                }
            }
            
            foreach (var cube in Cubes)
            {
                foreach (var rule in _rules.Where(r => r.ShouldApply(cube)))
                {
                    cubes.Add(rule.Apply(this, cube));
                }
            }

            return this with { Cubes = cubes};
        }
    }
    
    public record Dimension2(HashSet<Cube> Cubes)
    {
        private readonly List<Rule> _rules = new()
        {
            new ActiveRule(),
            new InactiveRule()
        };

        private void CreateIfNotExists(Position position)
        {
            var cube = Cubes.SingleOrDefault(c => c.GetPosition() == position);
            if (cube != null)
                return;
            
            Cubes.Add(Cube.CreateCube(position));
        }

        public Dimension2 Simulate()
        {
            var cubes = new HashSet<Cube>();

            //Create neighbours
            foreach (var cube in Cubes.ToArray())
            {
                foreach(var position in cube.GetNeighbourCoordinates())
                {
                    CreateIfNotExists(position);
                }
            }
            
            foreach (var cube in Cubes)
            {
                foreach (var rule in _rules.Where(r => r.ShouldApply(cube)))
                {
                    cubes.Add(rule.Apply(this, cube));
                }
            }

            return this with { Cubes = cubes};
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

        public override Cube Apply(Dimension2 dimension, Cube cube)
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

        public override Cube Apply(Dimension2 dimension, Cube cube)
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
        public abstract Cube Apply(Dimension2 dimension, Cube cube);

        protected static IEnumerable<Cube> GetNeighboursInRange(Dimension dimension, Cube cube)
        {
            var range = cube.GetRange();
            var neighbours = dimension.Cubes.Where(c =>
            {
                var position = c.GetPosition();

                return position >= range.Min &&
                       position <= range.Max &&
                       c != cube;
            });
            return neighbours;
        }
        protected static IEnumerable<Cube> GetNeighboursInRange(Dimension2 dimension, Cube cube)
        {
            var range = cube.GetRange();
            var neighbours = dimension.Cubes.Where(c =>
            {
                var position = c.GetPosition();

                return position >= range.Min &&
                       position <= range.Max &&
                       c != cube;
            });
            return neighbours;
        }
    }
    

    public record Cube(int X, int Y, int Z, bool Active)
    {
        public Cube(Position position, bool active)
            :this(position.X, position.Y, position.Z, active)
        {
            
        }

        public static Cube CreateCube(Position p)
        {
            return p switch
            {
                FourDPosition position => new FourDimensionCube(position, false),
                _ => new Cube(p, false)
            };
        }
        
        public string GetString() 
            => Active ? "#" : ".";
        
        
        public virtual (Position Min, Position Max) GetRange()
        {
            var minPosition = new Position(X - 1, Y - 1, Z - 1);
            var maxPosition = new Position(X + 1, Y + 1, Z + 1);
            return (minPosition, maxPosition);
        } 

        public virtual IEnumerable<Position> GetNeighbourCoordinates()
        {
            var positionList = new List<Position>();
            
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var z = 0; z < 3; z++)
                    {
                        var newX = x == 0 ? -1 : x == 1 ? 0 : 1;
                        var newY = y == 0 ? -1 : y == 1 ? 0 : 1;
                        var newZ = z == 0 ? -1 : z == 1 ? 0 : 1;
                        positionList.Add(new Position(X + newX, Y + newY, Z + newZ));
                    }
                } 
            }
            
            return positionList;
        }

        public virtual Position GetPosition() 
            => new(X, Y, Z);
    }
    
    public record FourDimensionCube(int X, int Y, int Z, int W, bool Active)
        :Cube(X,Y,Z, Active)
    {
        
        public FourDimensionCube(FourDPosition position, bool active)
            :this(position.X, position.Y, position.Z, position.W, active)
        {
            
        }
        
        public override (Position Min, Position Max) GetRange()
        {
            var minPosition = new FourDPosition(X - 1, Y - 1, Z - 1, W-1);
            var maxPosition = new FourDPosition(X + 1, Y + 1, Z + 1, W+1);
            return (minPosition, maxPosition);
        }

        public override FourDPosition GetPosition()
            => new(X, Y, Z, W);

        public override IEnumerable<Position> GetNeighbourCoordinates()
        {

            var positionList = new List<Position>();
            
            
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
                            
                            positionList.Add(
                                new FourDPosition(X + newX, Y + newY, Z + newZ, W + newW));
                        }
                    }
                } 
            }
            
            return positionList;
        } 
    }

    public record Position(int X, int Y, int Z)
    {
        public static bool operator <=(Position left, Position right) 
            => left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z;

        public static bool operator >=(Position left, Position right) 
            => left.X >= right.X && left.Y >= right.Y && left.Z >=right.Z;
    }

    public record FourDPosition(int X, int Y, int Z, int W) : Position(X, Y, Z)
    {
        public static bool operator <=(FourDPosition left, FourDPosition right) 
            => left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z && left.W <= right.W;

        public static bool operator >=(FourDPosition left, FourDPosition right) 
            => left.X >= right.X && left.Y >= right.Y && left.Z >= right.Z && left.W >= right.W;
        
    }
}