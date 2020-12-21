using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day19
{
    public record OuterEdges(Dictionary<Side, List<Space>> Edges)
    {
        public OuterEdges(List<Space> top, List<Space> bottom, List<Space> left, List<Space> right)
            : this(Convert(top, bottom, left, right))
        {
            
            
        }

        private static Dictionary<Side, List<Space>> Convert(List<Space> top, List<Space> bottom, List<Space> left, List<Space> right)
        {
            return new()
            {
                {Side.Top, top},
                {Side.Left, left},
                {Side.Right, right},
                {Side.Bottom, bottom}
            };
        }
        
        public (Side Left, Side Right, bool WasFlippedMatch) Match(OuterEdges otherOuterEdges)
        {
            foreach (var (key, value) in Edges)
            {
                var activatedList = value.Select(a => a.IsActivated).ToList();
                var flippedActivatedList = value.Select(a => a.IsActivated).Reverse().ToList();

                foreach (var (otherEdgeKey, otherEdgeValue) in otherOuterEdges.Edges)
                {
                    var isFlippedMatch = true;
                    var isUnflippedMatch = true;
                    
                    var otherActivatedList = otherEdgeValue.Select(a => a.IsActivated).ToList();

                    for (var i = 0; i < otherActivatedList.Count; i++)
                    {
                        if (otherActivatedList[i] != activatedList[i])
                            isUnflippedMatch = false;
                    }
                    
                    for (var i = 0; i < otherActivatedList.Count; i++)
                    {
                        if (otherActivatedList[i] != flippedActivatedList[i])
                            isFlippedMatch = false;
                    }

                    if (isUnflippedMatch || isFlippedMatch)
                    {
                        return (key, otherEdgeKey, isFlippedMatch);
                    }
                }                
            }

            return (Side.None, Side.None, false);
        }
    }
}