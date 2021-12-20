using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015.Day5
{
    public class WordEvaluator
    {
        public List<Rule> Rules { get; }
            = new();

        public bool IsNiceWord(string word) 
            => Rules.All(rule => rule.Evaluate(word));
    }

    public record WordExclusion(string Word) : Rule
    {
        public override bool Evaluate(string word) 
            => !word.Contains(Word);
    }
    
    public record RepeatingLetterRule(int JumpNumber = 2) : Rule
    {
        public override bool Evaluate(string word)
        {
            for (var i = 0; i < word.Length; i++)
            {
                var jumpLetter = i + JumpNumber;
                if (jumpLetter >= word.Length)
                    return false;

                if (char.Equals(word[i], word[jumpLetter]))
                    return true;
            }

            return false;
        }
    }
    
    public record LetterDuplicationRule(int NumberOfLetters, int Times) : Rule
    {
        public override bool Evaluate(string word)
        {
            var queue = new Queue<char>(word);

            var currentCount = 0;
            var lastLetter = '\0';

            while (queue.TryDequeue(out var letter))
            {
                if (letter == lastLetter)
                {
                    currentCount++;
                    if (currentCount == Times)
                        return true;
                    continue;
                }

                lastLetter = letter;
                currentCount = 0;
            }
            return false;
        }
    }
    
    public record RepeatingPairRule() : Rule
    {
        public override bool Evaluate(string word)
        {
            var result = word.Zip(word.Skip(1), (first, second) => new { pair = $"{first}{second}" })
                .Select(s => new { sValue = s, matches = Regex.Matches(word, s.pair) })
                .Distinct()
                .Where(c => c.matches.Count > 1)
                .GroupBy(c => c.sValue);

            foreach (var r in result)
            {
                var matches = Regex.Matches(word, r.Key.pair).Select(m => m.Index).ToList();
                
                for (var i = 0; i < matches.Count; i++)
                {
                    for (var j = i+1; j < matches.Count; j++)
                    {
                        var difference = matches[j] - matches[i];
                        if (difference > 1)
                            return true;
                    }
                }
            }

            return false;
        }
    }

    public record LetterEvalRule(char[] Letters, int Min) : Rule
    {
        public override bool Evaluate(string word)
        {
            var count = word.Count(letter => Letters.Any(x => x == letter));
            return count >= Min;
        }
    }

    public abstract record Rule
    {
        public abstract bool Evaluate(string word);
    }
}