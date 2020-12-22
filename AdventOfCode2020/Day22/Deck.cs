using System.Collections.Generic;

namespace AdventOfCode2020.Day22
{
    public record Deck(LifoStack<long> Cards)
    {
        public Deck(List<long> cards)
            :this(Longs(cards))
        {
        }

        private static LifoStack<long> Longs(List<long> cards)
        {
            return new(cards);
        }
    }
}