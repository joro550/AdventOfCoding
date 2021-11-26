using System.Collections.Generic;

namespace AdventOfCode._2015.Day8
{
    public class StringCounter
    {
        private static readonly List<char> BrokenCharacters = new List<char>
        {
            '"',
            '\\',
        };

        public static Counter Count(string value)
        {
            var count = 2; // 2 to count the quote characters
            foreach (var v in value)
            {
                if (BrokenCharacters.Contains(v))
                {
                    count += 2;
                }
                else
                {
                    count++;
                }
            }
            
            return new Counter(count, value.Length);
        }
    }

    public record Counter(int CodeCharacters, int StringCharacters);
}