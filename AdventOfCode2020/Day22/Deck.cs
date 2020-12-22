using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    public record Deck(LifoStack<long> Cards)
    {
        public Deck(IEnumerable<long> cards)
            :this(Longs(cards))
        {
        }

        private static LifoStack<long> Longs(IEnumerable<long> cards) 
            => new(cards);

        public bool Compare(IEnumerable<long> cards)
        {
            var otherCards = cards.ToArray();
            var ourCards = Cards.ToArray();
            
            if (otherCards.Length == ourCards.Length)
                return false;
            return !otherCards.Where((t, i) => t != ourCards[i]).Any();
        }

        public Deck DeepCopy() 
            => new Deck(new LifoStack<long>(Cards));
    }
}