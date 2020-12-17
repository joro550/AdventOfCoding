using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    public abstract record Rule
    {
        public abstract bool ShouldApply(Cube cube);
        public abstract Cube Apply(Dimension dimension, Cube cube);
        public abstract Cube Apply(Dimension2 dimension, Cube cube);

        protected static Dictionary<string, Cube> GetNeighboursInRange(Dimension dimension, Cube cube)
        {
            var range = cube.GetNeighbourCoordinates();
            var neighbours = new Dictionary<string, Cube>();
            foreach (var position in range)
            {
                var key = position.Base64Encode();
                if (neighbours.ContainsKey(key)) 
                    continue;
                
                var containsKey = dimension.Cubes.ContainsKey(key);                
                neighbours.Add(key, containsKey ? dimension.Cubes[key] : Cube.CreateCube(position));
            } 
            
            return neighbours;
        }
        
        protected static IEnumerable<Cube> GetNeighboursInRange(Dimension2 dimension, Cube cube)
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
}