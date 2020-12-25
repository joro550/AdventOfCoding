using System;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day25
{
    public class Day25Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day25Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Example()
        {
            const int keysPublicKey = 5764801;
            const int doorsPublicKey = 17807724;
            
            var keyLoopSize = FindLoopSize(keysPublicKey, 7);
            var doorLoopSize = FindLoopSize(doorsPublicKey, 7);
            
            var keysEncryptionKey = GetEncryptionKey(keysPublicKey, doorLoopSize);
            var doorEncryptionKey = GetEncryptionKey(doorsPublicKey, keyLoopSize);

            Assert.Equal(8, keyLoopSize);
            Assert.Equal(11, doorLoopSize);
            Assert.Equal(14897079, doorEncryptionKey);
            Assert.Equal(14897079, keysEncryptionKey);
        }

        [Fact]
        public void Puzzle1()
        {
            const int keysPublicKey = 15113849;
            const int doorsPublicKey = 4206373;

            var keyLoopSize = FindLoopSize(keysPublicKey, 7);
            var doorLoopSize = FindLoopSize(doorsPublicKey, 7);
            
            var keysEncryptionKey = GetEncryptionKey(keysPublicKey, doorLoopSize);
            var doorEncryptionKey = GetEncryptionKey(doorsPublicKey, keyLoopSize);
            
            _testOutputHelper.WriteLine($"key:{keysEncryptionKey}, door: {doorEncryptionKey}");
        }

        private long GetEncryptionKey(int subject, int loopSize)
        {
            var value = 1L;
            for (var i = 0; i < loopSize; i++)
                value = (value * subject % 20201227);

            return value;
        }


        private int FindLoopSize(int key, int subject)
        {
            var value = 1;
            var loopSize = 0;

            while (value != key)
            {
                value = (value * subject % 20201227);
                loopSize++;
            }

            return loopSize;
        }
    }
}