using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    public record Game(List<Player> Players)
    {
        public virtual bool Complete()
        {
            var playersWithCards = Players.Count(p => p.Deck.Cards.Any());
            return playersWithCards == 1;
        }
        
        public virtual void PlayRound()
        {
            var cards = Players.Select(p => p.GetNextCard());
            var highestCards = cards.OrderByDescending(i => i.Number).ToList();

            var playerId = highestCards.Take(1).Single().Owner;
            var owner = Players.Single(x => x.Id == playerId);
            owner.AddCardToDeck(highestCards);
        } 
    }
}