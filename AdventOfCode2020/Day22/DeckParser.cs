using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day22
{
    public static class DeckParser
    {
        public static (Player, Player) Parse(string input)
        {
            var numberRegex = new Regex("[0-9]+");

            var playerList = (from lines in input.Split(Environment.NewLine + Environment.NewLine)
                select lines.Split(Environment.NewLine)
                into line
                let id = long.Parse(numberRegex.Match(line[0]).Value)
                let card = line[1..].Select(long.Parse).ToList()
                select new Player(id, new Deck(card)))
                .ToList();
            return (playerList[0], playerList[1]);
        }
    }
}