using System.Linq;

namespace AdventOfCode2020.Day17
{
    public record InactiveRule : Rule
    {
        public override bool ShouldApply(Cube cube) 
            => !cube.Active;

        public override Cube Apply(Dimension dimension, Cube cube)
        {
            if (cube.Active)
                return cube;
                    
            var activeNeighbours = GetNeighboursInRange(dimension, cube)
                .Count(x => x.Value.Active);
            if (activeNeighbours == 3)
                return cube with {Active = true};
            return cube;
        }
    }
}