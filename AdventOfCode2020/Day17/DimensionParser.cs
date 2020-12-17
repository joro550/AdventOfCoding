using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public static class DimensionParser
    {
        public static Dimension Parse3D(string input)
        {
            var cubes = new Dictionary<string, Cube>();
            
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
                    var position = new Position(x, y, 0);
                    var cube = new Cube(position, active);
                    cubes.Add(position.Base64Encode(), cube);
                    x++;
                }

                y++;
            }
            
            return new Dimension(cubes);
        }
        
        public static Dimension Parse4D(string input)
        {
            var cubes = new Dictionary<string, Cube>();
            
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
                    var position = new FourDPosition(x, y, 0, 0);
                    var fourDimensionCube = new FourDimensionCube(position, active);
                    cubes.Add(position.Base64Encode(), fourDimensionCube);
                    x++;
                }

                y++;
            }
            return new Dimension(cubes);
        }
    }
}