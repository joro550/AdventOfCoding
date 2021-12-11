using System.Threading.Tasks;
using AdventOfCode._2021.Day4;
using Xunit;

namespace AdventOfCode.Tests._2021.Day4
{
    public class BingoTests
    {
        [Fact]
        public void GivenNumbersToCallInputThenItIsParsedCorrectly()
        {
            const string input = "1,2,3,4,5";
            var numbersToCall = NumbersToCall.Parse(input);
            Assert.Equal(1, numbersToCall.GetNextNumber());
            Assert.Equal(2, numbersToCall.GetNextNumber());
            Assert.Equal(3, numbersToCall.GetNextNumber());
            Assert.Equal(4, numbersToCall.GetNextNumber());
            Assert.Equal(5, numbersToCall.GetNextNumber());
        }

        [Fact]
        public void GivenBingoBoardThenItIsParsedCorrectly()
        {
            const string input = @"22 13 5
55 23 72
11 88 6";
            var board = BingoBoard.Parse(input);
            Assert.Equal(22, board.GetCell(0,0).Value);
            Assert.Equal(13, board.GetCell(0,1).Value);
            Assert.Equal(5, board.GetCell(0,2).Value);
            
            Assert.Equal(55, board.GetCell(1,0).Value);
            Assert.Equal(23, board.GetCell(1,1).Value);
            Assert.Equal(72, board.GetCell(1,2).Value);
            
            Assert.Equal(11, board.GetCell(2,0).Value);
            Assert.Equal(88, board.GetCell(2,1).Value);
            Assert.Equal(6, board.GetCell(2,2).Value);
        }
        
        [Fact]
        public void GivenBingoBoardThenItCanSearchByValue()
        {
            const string input = @"22 13 5
55 23 72
11 88 6";
            var board = BingoBoard.Parse(input);
            var cell = board.GetCellByValue(13);
            Assert.Equal(13, cell.Value);
            Assert.False(cell.Marked);
        }
        
        [Fact]
        public void GivenBingoBoardWhenNumberIsCalledThenThatNumberIsMarked()
        {
            const string input = @"22 13 5
55 23 72
11 88 6";
            var board = BingoBoard.Parse(input);
            board.NumberCalled(22);
            var cell = board.GetCellByValue(22);
            Assert.Equal(22, cell.Value);
            Assert.True(cell.Marked);
        }
        
        [Fact]
        public void WhenAllNumbersInARowHaveBeenCalledThenCardHasWon()
        {
            const string input = @"22 13 5
55 23 72
11 88 6";
            var board = BingoBoard.Parse(input);
            board.NumberCalled(22);
            board.NumberCalled(13);
            board.NumberCalled(5);
            
            Assert.True(board.HasWon());
        }
        
        [Fact]
        public void WhenAllNumbersInAColumnHaveBeenCalledThenCardHasWon()
        {
            const string input = @"22 13 5
55 23 72
11 88 6";
            var board = BingoBoard.Parse(input);
            board.NumberCalled(22);
            board.NumberCalled(55);
            board.NumberCalled(11);
            
            Assert.True(board.HasWon());
        }

        [Fact]
        public void Puzzle1()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2021.Day4.PuzzleInput.txt");
            
            var game = PuzzleParser.Parse1(input);

            int? lastNumberCalled = null;
            while (game.BoardHasWon() == false)
            {
                lastNumberCalled = game.Play();
            }

            BingoBoard board = game.GetWinningBoard();
            var uncalledSum = board.GetUncalledSum();
            Assert.Equal(8580L, uncalledSum * lastNumberCalled);
        }
        
        [Fact]
        public void Puzzle2()
        {
            var input = FileReader
                .GetResource("AdventOfCode.Tests._2021.Day4.PuzzleInput.txt");
            
            var game = PuzzleParser.Parse1(input);

            int? lastNumberCalled = null;
            while (game.GetNonWonBoards().Count > 0)
            {
                lastNumberCalled = game.Play();
            }

            BingoBoard board = game.GetLastWinningBoard();
            var uncalledSum = board.GetUncalledSum();
            Assert.Equal(1924L, uncalledSum * lastNumberCalled);
        }

        [Fact]
        public void Example1()
        {
            var input = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";
            var game = PuzzleParser.Parse1(input);

            int? lastNumberCalled = null;
            while (game.BoardHasWon() == false)
            {
                lastNumberCalled = game.Play();
            }

            BingoBoard board = game.GetWinningBoard();
            var uncalledSum = board.GetUncalledSum();
            Assert.Equal(4512L, uncalledSum * lastNumberCalled);
        }
        
        [Fact]
        public void Example2()
        {
            var input = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";
            var game = PuzzleParser.Parse1(input);
            
            int? lastNumberCalled = null;
            while (game.GetNonWonBoards().Count > 0)
            {
                lastNumberCalled = game.Play();
            }

            BingoBoard board = game.GetLastWinningBoard();
            var uncalledSum = board.GetUncalledSum();
            Assert.Equal(1924L, uncalledSum * lastNumberCalled);
        }
    }
}