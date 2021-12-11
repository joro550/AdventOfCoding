using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode._2021.Day4
{
    public class Bingo
    {
        private NumbersToCall _numbersToCall;
        private List<BingoBoard> _boards;
        private Stack<BingoBoard> _wonBoards = new();

        public Bingo(List<BingoBoard> boards, NumbersToCall numbersToCall)
        {
            _boards = boards;
            _numbersToCall = numbersToCall;
        }

        public bool BoardHasWon()
            => _boards.Any(board => board.HasWon());

        public int? Play()
        {
            var numberToCall = _numbersToCall.GetNextNumber();
            if (numberToCall == null) return null;

            foreach (var board in _boards.Where(b=>!b.HasWon()))
            {
                board.NumberCalled(numberToCall.Value);
                if (board.HasWon())
                    _wonBoards.Push(board);
            }

            
            return numberToCall;
        }

        public List<BingoBoard> GetNonWonBoards()
        {
            return _boards.Where(x => !x.HasWon()).ToList();
        }

        public BingoBoard GetWinningBoard() 
            => _boards.FirstOrDefault(x => x.HasWon());

        public BingoBoard GetLastWinningBoard()
        {
            return _wonBoards.Pop();
        }
    }

    public static class PuzzleParser
    {
        public static Bingo Parse1(string input)
        {
            var bingo = input
                .Split(Environment.NewLine + Environment.NewLine);

            var numbersToCall = NumbersToCall.Parse(bingo[0]);
            var boards = bingo[1..].Select(BingoBoard.Parse).ToList();
            return new Bingo(boards, numbersToCall);
        }
    }

    public record BingoCell(int Value, bool Marked = false);

    public class BingoBoard
    {
        private readonly List<List<BingoCell>> _cells;

        private BingoBoard(List<List<BingoCell>> cells)
            => _cells = cells;

        public static BingoBoard Parse(string input)
        {
            var lines = input.Split(Environment.NewLine);
            var bingoCells = new List<List<BingoCell>>();
            
            for (var i = 0; i < lines.Length; i++)
            {
                var row = lines[i].Split(" ")
                    .Where(l => !string.IsNullOrEmpty(l));
                
                bingoCells.Add(
                    row.Select((t, j) 
                        => new BingoCell(int.Parse(t)))
                        .ToList());
            }

            return new BingoBoard(bingoCells);
        }

        public BingoCell GetCellByValue(int value)
        {
            return _cells.SelectMany(list => list.Where(bingoCell => bingoCell.Value == value)).FirstOrDefault();
        }

        public BingoCell GetCell(int x, int y)
            => _cells[x][y];

        public void NumberCalled(int number)
        {
            foreach (var list in _cells)
            {
                for (var j = 0; j < list.Count; j++)
                {
                    if (list[j].Value != number) continue;
                    list[j] = list[j] with { Marked = true };
                    break;
                }
            }
        }

        public bool HasWon()
        {
            if (_cells.Any(t => t.All(x => x.Marked)))
            {
                return true;
            }

            var rows = _cells.Select(x => x).ToArray();

            for (int i = 0; i < rows.Length; i++)
            {
                var hasWonByColumn = true;
                foreach (var t in _cells)
                {
                    if (t[i].Marked == false)
                    {
                        hasWonByColumn = false;
                    }
                }

                if (hasWonByColumn)
                    return true;
            }
            
            return false;
        }

        public long GetUncalledSum()
        {
            long value = 0;
            for (int i = 0; i < _cells.Count; i++)
            {
                for (int j = 0; j < _cells[i].Count; j++)
                {
                    if(_cells[i][j].Marked) continue;
                    value += _cells[i][j].Value;
                }
            }

            return value;
        }
    }

    public class NumbersToCall
    {
        private readonly Queue<int> _numberQueue;

        private NumbersToCall(IEnumerable<int> numbers) 
            => _numberQueue = new Queue<int>(numbers);

        public static NumbersToCall Parse(string content) =>
            new(content.Split(",").Select(int.Parse));

        public int? GetNextNumber()
        {
            if (_numberQueue.TryDequeue(out var result))
                return result;
            return null;
        }
    }
}