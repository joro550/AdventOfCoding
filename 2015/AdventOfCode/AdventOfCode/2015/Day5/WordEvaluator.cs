using System.Linq;
using System.Collections.Generic;

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