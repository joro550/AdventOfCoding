using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4
{
    public class Passport
    {
        private readonly Dictionary<string, string> _fields
            = new();

        public bool Validate(IEnumerable<PassportFields> fields)
        {
            return fields.All(field => !field.IsRequired || _fields.ContainsKey(field.Name) );
        }

        public bool ValidatePuzzle2(IEnumerable<PassportFields> fields)
        {
            return fields.All(field =>
                !field.IsRequired || _fields.ContainsKey(field.Name) && field.Validate(_fields[field.Name]));
        }

        public static IEnumerable<Passport> Parse(string passportData)
        {
            var passPortLines = passportData.Split(Environment.NewLine);

            var passPorts = new List<Passport>();
            var passport = new Passport();
            
            foreach (var passPortLine in passPortLines)
            {
                if (string.IsNullOrWhiteSpace(passPortLine))
                {
                    passPorts.Add(passport);
                    passport = new Passport();
                }

                var fields = GetFields(passPortLine.Split(" "));
                foreach (var (key, value) in fields) 
                    passport._fields.Add(key, value);

            }

            passPorts.Add(passport);
            return passPorts.ToArray();
        }
        
        private static Dictionary<string, string> GetFields(IEnumerable<string> values)
        {
            return values.Select(value => value.Split(":"))
                .ToDictionary(fields => fields[0], fields => fields[^1]);
        }
    }

    public record YearField(int MinNumbers, int MinYear, int MaxYear, string Name, bool IsRequired)
        : PassportFields(Name, IsRequired)
    {
        public override bool Validate(string value)
        {
            var yearValue = int.Parse(value);
            if (value.Length < MinNumbers)
                return false;

            return yearValue >= MinYear && yearValue <= MaxYear;
        }
    }

    public record HeightField(string Name, bool IsRequired)
        : PassportFields(Name, IsRequired)
    {
        public override bool Validate(string value)
        {
            var hasHeight = int.TryParse(value[..^2], out var height);
            var measurement = value[^2..];

            if (!hasHeight) 
                return false;
            
            return measurement switch
            {
                "cm" => height >= 150 && height <= 2030,
                "in" => height >= 59 && height <= 76,
                _ => false
            };
        }
    }
    
    public record RegexField(string RegexPattern, string Name, bool IsRequired)
        : PassportFields(Name, IsRequired)
    {
        public override bool Validate(string value) 
            => Regex.Match(value, RegexPattern).Success;
    }

    public record PassportIdField (string Name, bool IsRequired)
        : PassportFields(Name, IsRequired)
    {
        private readonly Regex _colourRegex = new("[0-9]{9}");
        
        public override bool Validate(string value) 
            => _colourRegex.IsMatch(value);
    }
    

    public record PassportFields(string Name, bool IsRequired)
    {
        public virtual bool Validate(string value) => true;
    }
}