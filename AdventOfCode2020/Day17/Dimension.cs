﻿using System.Collections.Generic;
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

            foreach (var cube in Cubes.Values.Where(c => !c.HasGeneratedNeighbours))
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