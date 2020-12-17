using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day17
{
    public record Dimension(Dictionary<string, Cube> Cubes)
    {
        private readonly List<Rule> _rules = new()
        {
            new ActiveRule(),
            new InactiveRule()
        };

        public Dimension Simulate()
        {
            var cubes = new Dictionary<string, Cube>();

            //Create neighbours
            var cubesToProcess = GenerateNeighbours();

            foreach (var (_, cube) in cubesToProcess)
            {
                foreach (var rule in _rules.Where(r => r.ShouldApply(cube)))
                {
                    var newCube = rule.Apply(this, cube);
                    cubes.Add(newCube.Position.Base64Encode(), newCube);
                }
            }

            return this with { Cubes = cubes };
        }

        private Dictionary<string, Cube> GenerateNeighbours()
        {
            var cubeList = new Dictionary<string, Cube>();
            foreach (var cubes in Cubes.Values)
            {
                foreach (var position in cubes.GetNeighbourCoordinates())
                {
                    var key = position.Base64Encode();
                    if (cubeList.ContainsKey(key)) 
                        continue;
                    
                    var cubeToAdd = Cubes.ContainsKey(key) ? Cubes[key] : Cube.CreateCube(position);
                    cubeList.Add(key, cubeToAdd);
                }
            }

            return cubeList;
        }
        
        
        public string Print()
        {
            var sb = new StringBuilder();
            foreach (var cube in Cubes.Values)
            {
                sb.Append(cube.GetString());
            }

            return sb.ToString();
        }
    }
}