using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day6
{
    public static class AnswerParser
    {
        public static IEnumerable<Group> Parse(string input) =>
            input.Split(Environment.NewLine + Environment.NewLine)
                .Select(group => new Group(group.Split(Environment.NewLine).ToList()));

        public static IEnumerable<Group> ParsePuzzle2(string input) =>
            input.Split(Environment.NewLine + Environment.NewLine)
                .Select(group => new GroupAnswer(group.Split(Environment.NewLine).ToList()));
    }

    public record GroupAnswer(List<string> Answers) : Group(Answers)
    {
        public override int GetGroupAnswerCount()
        {
            return Answers.Count == 1
                ? Answers[0].Length
                : Answers[0].Count(character => Answers.Skip(1).All(a => a.Contains(character)));
        }
    }

    public record Group(List<string> Answers)
    {
        public virtual int GetGroupAnswerCount() =>
            string.Join("", Answers)
                .Distinct()
                .Count();
    }
        
}