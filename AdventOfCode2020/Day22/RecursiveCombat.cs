using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    public record RecursiveCombat(Dictionary<long, Player> Players) 
    {
        private Player _winner;
        private bool _isComplete;
        private readonly Dictionary<long, List<Deck>> _decksSeen =new();
        public long Rounds = 0;

        public RecursiveCombat(IEnumerable<Player> players)
            : this(players.ToDictionary(player => player.Id))
        {
        }

        public bool Complete()
        {
            if (_isComplete)
                return true;
            return Players.Count(p => p.Value.Deck.Cards.Any()) == 1;
        }

        public Player GetWinner()
            => _winner != null ? _winner : Players.Single(p => p.Value.Deck.Cards.Any()).Value;

        private static RecursiveCombat CopyFrom(RecursiveCombat game) 
            => new(game.Players.Values.Select(player => player.DeepCopy()).ToList());

        public void PlayRound()
        {
            // if either deck has been seen then a winner is declared
            if (HasDeckBeenSeen())
            {
                _isComplete = true;
                return;
            }
            
            // Get the next cards
            var cards = Players.Select(p => p.Value.GetNextCard()).ToDictionary(x => x.Owner);
            
            // play the sub game
            var cardArray = cards.Values.ToArray();
            if (ShouldPlaySubGame(cardArray))
            {
                PlaySubGame(cardArray);
                return;
            }
            
            // We don't have enough cards to keep recurse
            var highestCards = cards.OrderByDescending(i => i.Value.Number).ToArray();
            var playerId = highestCards[0].Value.Owner;
            
            var owner = Players[playerId];
            owner.AddCardToDeck(highestCards.Select(x => x.Value));
            Rounds++;
        }

        private bool ShouldPlaySubGame(IEnumerable<Card> cards)
        {
            var shouldRecurse = true;
            // If both players have enough cards then we need to lay down a sub game
            foreach (var (owner, number) in cards)
            {
                var player = Players[owner];
                if (player.Deck.Cards.Count() < number)
                    shouldRecurse = false;
            }

            return shouldRecurse;
        }

        private void PlaySubGame(Card[] cards)
        {
            var subGame = CopyFrom(this);
            while (!subGame.Complete())
            {
                subGame.PlayRound();
            }

            var winner = Players[subGame.GetWinner().Id];
            winner.AddCardToDeck(cards.Where(x => x.Owner == winner.Id));
            winner.AddCardToDeck(cards.Where(x => x.Owner != winner.Id));
            Rounds += subGame.Rounds;
            return;
        }

        private bool HasDeckBeenSeen()
        {
            foreach (var (id, playerDeck) in Players)
            {
                if (!_decksSeen.ContainsKey(id))
                {
                    _decksSeen.Add(id, new List<Deck> {playerDeck.Deck.DeepCopy()});
                    continue;
                }

                var seenDecks = _decksSeen[id];
                if (!seenDecks.Any(deck => playerDeck.Deck.Compare(deck.Cards)))
                {
                    _decksSeen[id].Add(playerDeck.Deck.DeepCopy());
                    continue;
                }
                
                // If we've seen this deck before then player 1 is the winner
                _winner = Players[1];
                return true;
            }

            return false;
        }
        
    }
}