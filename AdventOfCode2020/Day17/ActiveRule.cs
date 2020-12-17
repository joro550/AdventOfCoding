using System.Linq;

namespace AdventOfCode2020.Day17
{
    public record ActiveRule : Rule
    {
        public override bool ShouldApply(Cube cube) 
            => cube.Active;

        public override Cube Apply(Dimension dimension, Cube cube)
        {
            var activeNeighbours = GetNeighboursInRange(dimension, cube)
                .Count(x => x.Value.Active);

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
}