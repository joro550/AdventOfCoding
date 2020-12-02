using System.Linq;

namespace AdventOfCode2020.Day2
{
    public static class PolicyParser
    {
        public static Policy ParsePolicy(string policyString)
        {
            var policy = policyString.Split(" ");

            var minMax = policy[0].Split("-");
            
            var min = int.Parse(minMax[0]);
            var max = int.Parse(minMax[^1]);
            
            var character = policy[1];
            
            return new Policy(min, max, character);
        }
        
        public static TobogganPolicy ParseTobogganPolicy(string policyString)
        {
            var policy = policyString.Split(" ");

            var positions = policy[0].Split("-");
            
            var firstPosition = int.Parse(positions[0]);
            var secondPosition = int.Parse(positions[^1]);
            
            var character = policy[1];
            
            return new TobogganPolicy(firstPosition, secondPosition, character);
        }
    }

    public static class LineParser
    {
        public static LineInformation ParseLine(string policyString)
        {
            var strings = policyString.Split(":");
            var password = strings[^1].Trim();
            return new(PolicyParser.ParsePolicy(strings[0]), password);
        }
        
        public static LineInformation ParseTobogganLine(string policyString)
        {
            var strings = policyString.Split(":");
            var password = strings[^1].Trim();
            return new(PolicyParser.ParseTobogganPolicy(strings[0]), password);
        }
    }

    public record LineInformation(Policy Policy, string Password);

    public record Policy(int Min, int Max, string Character)
    {
        public virtual bool IsPasswordValid(string password)
        {
            var count = password.Count(character => character == Character[0]);
            return count >= Min && count <= Max;
        }
    }

    public record TobogganPolicy(int FirstPosition, int SecondPosition, string Character)
        : Policy(FirstPosition, SecondPosition, Character)
    {
        public override bool IsPasswordValid(string password)
        {
            // Subtracting one because no index 0
            var firstChar = password[FirstPosition - 1];
            var secondChar = password[SecondPosition - 1];
            var toCompare = Character[0];
            return firstChar == toCompare ^ secondChar == toCompare;
        }
        
    }
}