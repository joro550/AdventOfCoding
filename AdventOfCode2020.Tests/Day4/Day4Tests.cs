using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day4;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day4
{
    public class Day4Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day4Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void PassportWithAllFieldsIsValid()
        {
            var passport = Passport.Parse(@"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm");

            var areValid = passport.Select(p => p.Validate(GetPassportFields()))
                .ToList();
            
            Assert.True(areValid[0]);
        }
        
        [Fact]
        public void PassportWithFieldsMissingIsInvalid()
        {
            var passport = Passport.Parse(@"iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929");

            var areValid = passport.Select(p => p.Validate(GetPassportFields()))
                .ToList();
            
            Assert.False(areValid[0]);
        }
        
        [Fact]
        public void PassportWithCidFieldsMissingIsValid()
        {
            var passport = Passport.Parse(@"hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm");

            var areValid = passport.Select(p => p.Validate(GetPassportFields()))
                .ToList();
            
            Assert.True(areValid[0]);
        }

        [Fact]
        public void ExampleInputPuzzle1()
        {
            var passport = Passport.Parse(@"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in");

            var areValid = passport.Select(p => p.Validate(GetPassportFields()))
                .ToList();
            
            Assert.True(areValid[0]);
            Assert.False(areValid[1]);
            Assert.True(areValid[2]);
            Assert.False(areValid[3]);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day4.PuzzleInput.txt");

            var passport = Passport.Parse(puzzleInput);
            var validCount = passport.Count(p => p.Validate(GetPassportFields()));

            _testOutputHelper.WriteLine(validCount.ToString());
        }

        [Fact]
        public void ExampleInput1()
        {
            var passport = Passport.Parse(@"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007");

            var areValid = passport.Select(p => p.ValidatePuzzle2(GetPassportFields()))
                .ToList();
            
            Assert.False(areValid[0]);
            Assert.False(areValid[1]);
            Assert.False(areValid[2]);
            Assert.False(areValid[3]);
        }
        
        [Fact]
        public void ExampleInput2()
        {
            var passport = Passport.Parse(@"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719");

            var areValid = passport.Select(p => p.ValidatePuzzle2(GetPassportFields()))
                .ToList();
            
            Assert.True(areValid[0]);
            Assert.True(areValid[1]);
            Assert.True(areValid[2]);
            Assert.True(areValid[3]);
        }

        [Fact]
        public void SolvePuzzle2()
        {
            var puzzleInput = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day4.PuzzleInput.txt");

            var passport = Passport.Parse(puzzleInput);
            var validCount = passport.Count(p => p.ValidatePuzzle2(GetPassportFields()));

            _testOutputHelper.WriteLine(validCount.ToString());
        }

        private static IEnumerable<PassportFields> GetPassportFields()
        {
            return new PassportFields[]
            {
                new YearField(4, 1920, 2002, "byr", true),
                new YearField(4, 2010, 2020, "iyr", true),
                new YearField(4, 2020, 2030, "eyr", true),
                new HeightField("hgt", true),
                new RegexField("^#[0-9A-Fa-f]{6}$", "hcl", true),
                new RegexField("amb|blu|brn|gry|grn|hzl|oth", "ecl", true),
                new RegexField("^[0-9]{9}$", "pid", true),
                new("cid", false),
            };
        }
    }
}