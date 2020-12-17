using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public record Dimension2(HashSet<Cube> Cubes)
    {
        private readonly List<Rule> _rules = new()
        {
            new ActiveRule(),
            new InactiveRule()
        };

        public Dimension2 Simulate()
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
                    
                    if(Cubes.Any(c => c.Position == position))
                        continue;
                    
                    
                    Cubes.Add(Cube.CreateCube(position));
                }
            }
        }
    }
}