using System.Collections.Generic;

namespace AdventOfCode2020.Day3
{
    public class Navigation
    {
        private List<Move> _move;
        private State _state;

        public Navigation(List<Move> move)
        {
            _move = move;
            _state = new State(0);
        }

        public List<Move> GetMoves() 
            => _move;

        private void ResetState() 
            => _state = _state.Reset();

        public Move GetNextMove()
        {
            if (_state.CurrentIndex >= _move.Count)
                ResetState();
            return _move[_state.CurrentIndex];
        }

        public record State (int CurrentIndex)
        {
            public State Reset() => this with{ CurrentIndex = 0};
        }
    }
}