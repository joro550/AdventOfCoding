using Xunit;
using AdventOfCode._2015.Day4;

namespace AdventOfCode.Tests._2015.Day4
{
    public class AdventCoinTests
    {
        private CoinMiner _miner;

        public AdventCoinTests() 
            => _miner = new CoinMiner();

        [Fact]
        public void Example1()
        {
            const string key = "abcdef";
            
            var thing = _miner.GetValidCoinNumber(key);
            Assert.Equal(609043, thing);
        }
        
        [Fact]
        public void Example2()
        {
            const string key = "pqrstuv";
            var thing = _miner.GetValidCoinNumber(key);
            Assert.Equal(1048970, thing);
        }
        
        [Fact]
        public void Puzzle1()
        {
            const string key = "iwrupvqb";
            var thing = _miner.GetValidCoinNumber(key);
            Assert.Equal(346386, thing);
        }
        
        [Fact]
        public void Puzzle2()
        {
            const string key = "iwrupvqb";

            var coinMiner = new CoinMiner(6);
            var thing = coinMiner.GetValidCoinNumber(key);
            Assert.Equal(346386, thing);
        }
    }
}