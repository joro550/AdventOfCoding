using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    public record Player(long Id, Deck Deck)
    {
        public Card GetNextCard() =>
            !Deck.Cards.Any() 
                ? null 
                : new Card(Id, Deck.Cards.Pop());

        public (long Id, long card) GetNextCard2()
        {
            return !Deck.Cards.Any() ? (0, 0) : (Id, Deck.Cards.Pop());
        }

        public void AddCardToDeck(IEnumerable<Card> cards)
        {
            foreach (var (_, number) in cards) 
                Deck.Cards.Push(number);
        }

        public long CalculateScore()
        {
            if (!Deck.Cards.Any())
                return 0;

            var weighting = Deck.Cards.Count();
            long score = 0;
            var value = Deck.Cards.Pop();
            while (value != default)
            {
                score += value * weighting;
                weighting--;
                value = Deck.Cards.Pop();
            }

            return score;
        }


        public Player DeepCopy() 
            => new(Id, Deck.DeepCopy());
    }
}