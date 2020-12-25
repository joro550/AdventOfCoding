using Xunit;
using System;
using System.Collections.Generic;
using AdventOfCode2020.Day22;

namespace AdventOfCode2020.Tests.Day22
{
    public class Day22Tests
    {
        [Fact]
        public void PlayerParser()
        {
            var input = @"Player 1:|9|2|6|3|1||Player 2:|5|8|4|7|10".Replace("|", Environment.NewLine);
            var (player1, player2) = DeckParser.Parse(input);
            
            Assert.Equal(9, player1.Deck.Cards.Pop());
            Assert.Equal(2, player1.Deck.Cards.Pop());
            Assert.Equal(6, player1.Deck.Cards.Pop());
            Assert.Equal(3, player1.Deck.Cards.Pop());
            Assert.Equal(1, player1.Deck.Cards.Pop());
            
            Assert.Equal(5, player2.Deck.Cards.Pop());
            Assert.Equal(8, player2.Deck.Cards.Pop());
            Assert.Equal(4, player2.Deck.Cards.Pop());
            Assert.Equal(7, player2.Deck.Cards.Pop());
            Assert.Equal(10, player2.Deck.Cards.Pop());
        }

        [Fact]
        public void GameTest()
        {
            var input = @"Player 1:|9|2|6|3|1||Player 2:|5|8|4|7|10".Replace("|", Environment.NewLine);
            var (player1, player2) = DeckParser.Parse(input);
            var game = new Game(new List<Player> {player1, player2});
            
            game.PlayRound();
            
            Assert.Equal(2, player1.Deck.Cards.Pop());
            Assert.Equal(6, player1.Deck.Cards.Pop());
            Assert.Equal(3, player1.Deck.Cards.Pop());
            Assert.Equal(1, player1.Deck.Cards.Pop());
            Assert.Equal(9, player1.Deck.Cards.Pop());
            Assert.Equal(5, player1.Deck.Cards.Pop());
        }
        
        [Fact]
        public void GameTest2()
        {
            var input = @"Player 1:|9|2|6|3|1||Player 2:|5|8|4|7|10".Replace("|", Environment.NewLine);
            var (player1, player2) = DeckParser.Parse(input);
            var game = new Game(new List<Player> {player1, player2});

            while (!game.Complete())
            {
                game.PlayRound();
            }
            
            Assert.Equal(3, player2.Deck.Cards.Pop());
            Assert.Equal(2, player2.Deck.Cards.Pop());
            Assert.Equal(10, player2.Deck.Cards.Pop());
            Assert.Equal(6, player2.Deck.Cards.Pop());
            Assert.Equal(8, player2.Deck.Cards.Pop());
            Assert.Equal(5, player2.Deck.Cards.Pop());
            Assert.Equal(9, player2.Deck.Cards.Pop());
            Assert.Equal(4, player2.Deck.Cards.Pop());
            Assert.Equal(7, player2.Deck.Cards.Pop());
            Assert.Equal(1, player2.Deck.Cards.Pop());
        }
        
        [Fact]
        public void ScoreCalcTest()
        {
            var input = @"Player 1:|9|2|6|3|1||Player 2:|5|8|4|7|10".Replace("|", Environment.NewLine);
            var (player1, player2) = DeckParser.Parse(input);
            var game = new Game(new List<Player> {player1, player2});

            while (!game.Complete())
            {
                game.PlayRound();
            }

            Assert.Equal(306, player2.CalculateScore());
        }

        [Fact]
        public void Puzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day22.PuzzleInput.txt");
            
            var (player1, player2) = DeckParser.Parse(input);
            var game = new Game(new List<Player> {player1, player2});

            while (!game.Complete())
            {
                game.PlayRound();
            }
            
            Assert.Equal(35005, player1.CalculateScore());
            Assert.Equal(0, player2.CalculateScore());
        }
        
        [Fact]
        public void ScoreCalcTestPuzzle2()
        {
            var input = @"Player 1:|9|2|6|3|1||Player 2:|5|8|4|7|10".Replace("|", Environment.NewLine);
            var (player1, player2) = DeckParser.Parse(input);
            var game = new RecursiveCombat(new List<Player> {player1, player2});

            while (!game.Complete())
            {
                game.PlayRound();
            }

            Assert.Equal(291, player2.CalculateScore());
        }

        [Fact]
        public void TestInfiniteGame()
        {
            var input = @"Player 1:|43|19||Player 2:|2|29|14".Replace("|", Environment.NewLine);var (player1, player2) = DeckParser.Parse(input);
            var game = new RecursiveCombat(new List<Player> {player1, player2});

            while (!game.Complete())
            {
                game.PlayRound();
            }

            Assert.Equal(78, player2.CalculateScore());
        }

        [Fact]
        public void Puzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day22.PuzzleInput.txt");
            
            var (player1, player2) = DeckParser.Parse(input);
            var game = new RecursiveCombat(new List<Player> {player1, player2});

            while (!game.Complete())
            {
                game.PlayRound();
            }

            Assert.Equal(1800, game.Rounds);
            
            Assert.Equal(9833, game.GetWinner().CalculateScore());
        }
    }
}