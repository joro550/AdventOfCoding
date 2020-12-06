using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day6
{
    public static class AnswerParser
    {
        public static IEnumerable<Group> Parse(string input) =>
            input.Split(Environment.NewLine + Environment.NewLine)
                .Select(group => string.Join("", group.Split(Environment.NewLine)))
                .Select(answers => new Group(answers)).ToList();


        public static IEnumerable<GroupAnswer> ParsePuzzle2(string input) =>
            input.Split(Environment.NewLine + Environment.NewLine)
                .Select(group => new GroupAnswer(group.Split(Environment.NewLine).ToList()));
    }

    public record GroupAnswer(List<string> Answers)
    {
        public int GetGroupAnswerCount()
        {
            return Answers.Count == 1
                ? Answers[0].Length
                : Answers[0].Count(character => Answers.Skip(1).All(a => a.Contains(character)));
        }
    }

    public record Group(string Answers)
    {
        public int GetGroupAnswerCount() 
            => Answers.Distinct().Count();
    }
}