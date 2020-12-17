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

            foreach (var (key, value) in Cubes) 
                cubeList.Add(key, value);

            foreach (var (key, cube) in Cubes)
            {
                foreach (var position in cube.GetNeighbourCoordinates())
                {
                    var positionKey = position.Base64Encode();
                    if (cubeList.ContainsKey(positionKey) || Cubes.ContainsKey(positionKey)) 
                        continue;

                    cubeList.Add(positionKey, Cube.CreateCube(position));
                }
            }

            return cubeList;
        }
        
        
        
        public Dimension Simulate2()
        {
            var cubes = new Dictionary<string, Cube>();

            //Create neighbours
            GenerateNeighbours2();

            foreach (var (_, cube) in Cubes)
            {
                foreach (var rule in _rules.Where(r => r.ShouldApply(cube)))
                {
                    var newCube = rule.Apply(this, cube);
                    cubes.Add(newCube.Position.Base64Encode(), newCube);
                }
            }

            return this with { Cubes = cubes };
        }
        
        
        private void GenerateNeighbours2()
        {
            var cubesWithoutGeneratedNeighbours = Cubes.Where(c => !c.Value.HasGeneratedNeighbours).ToArray();
            
            var seenPositions = new HashSet<Position>();
            
            for (var index = 0; index < cubesWithoutGeneratedNeighbours.Length; index++)
            {
                var cube = cubesWithoutGeneratedNeighbours[index];

                foreach (var position in cube.Value.GetNeighbourCoordinates())
                {
                    if(seenPositions.Any(p => p == position))
                        continue;

                    seenPositions.Add(position);
                    
                    if(Cubes.Any(c => c.Value.Position == position))
                        continue;


                    var base64Encode = position.Base64Encode();
                    try
                    {
                        Cubes.Add(base64Encode, Cube.CreateCube(position));
                    }
                    catch
                    {
                        
                    }
                }
            }
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