using System.Collections.Generic;

namespace AdventOfCode2020.Day3
{
    public class Navigator
    {
        public int X;
        public int Y;
        private readonly Map _map;
        private readonly Navigation _navigation;
        private readonly List<Space> _visitedSpaces;

        public Navigator(int startingX, int startingY, Map map, Navigation navigation)
        {
            X = startingX;
            Y = startingY;
            _map = map;
            _navigation = navigation;
            _visitedSpaces = new();
            _visitedSpaces.Add(GetSpaceOnMap());
        }

        private void Accept(Move navigator) => navigator.VisitNavigator(this);

        public Space GetSpaceOnMap()
            => _map.GetCoords(X, Y);

        public IEnumerable<Space> GetVisitedSpaces() 
            => _visitedSpaces;

        public void Navigate()
        {
            foreach (var move in _navigation.GetMoves()) 
                Accept(move);

            _visitedSpaces.Add(GetSpaceOnMap());
        }
    }

    public record YMove : Move
    {
        public override void VisitNavigator(Navigator navigator) => navigator.Y++;
    }
    
    public record MultipleYMove(int MoveCount) : Move
    {
        public override void VisitNavigator(Navigator navigator) => navigator.Y+= MoveCount;
    }

    public record XMove : Move
    {
        public override void VisitNavigator(Navigator navigator) => navigator.X++;
    }
    
    public record MultipleXMove(int MoveCount) : Move
    {
        public override void VisitNavigator(Navigator navigator) => navigator.X+= MoveCount;
    }

    public abstract record Move
    {
        public abstract void VisitNavigator(Navigator navigator);
    }
}