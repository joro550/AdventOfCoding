using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020.Tests.Day23
{
    public class LinkedListTests
    {
        [Fact]
        public void LinkedList()
        {
            var list = new LinkedList<int>(new [] {3,8,9,1,2,5,4,6,7});
            
            
            var node = list.Find(3);
            for (var i = 0; i < 3; i++)
            {
                var nextValue = node.Next;
                list.Remove(nextValue);
            }

            Assert.Equal(new[] {3, 2, 5, 4, 6, 7 }, list);
        }
    }
}