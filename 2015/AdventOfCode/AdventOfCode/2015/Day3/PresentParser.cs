using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2015.Day3
{
    public static class PresentParser
    {
        public static List<House> Parse(string input)
        {
            var houses = new List<House> {new House(0, 0)};

            foreach (var house in 
                from letter in input 
                let lastHouse = houses[^1] 
                select letter switch
            {
                '>' => lastHouse with { X = lastHouse.X + 1},
                '<' => lastHouse with { X = lastHouse.X - 1},
                '^' => lastHouse with { Y = lastHouse.Y + 1},
                'v' => lastHouse with { Y = lastHouse.Y - 1},
                _ => new House(0, 0)
            })
            {
                houses.Add(house);
            }

            return houses.Distinct()
                .ToList();
        }
        
        public static List<House> ParsePuzzle2(string input)
        {
            var initialHouse = new House(0, 0);
            
            
            var houses = new List<House> {initialHouse};

            for (var i = 0; i < input.Length; i ++)
            {
                houses.Add(i == 0 ? HandleCharacter(houses[^1], input[i]) : HandleCharacter(houses[^2], input[i]));
            }

            
            
            
            return houses.Distinct()
                .ToList();
        }

        private static House HandleCharacter(House house, char letter) =>
            letter switch
            {
                '>' => house with { X = house.X + 1},
                '<' => house with { X = house.X - 1},
                '^' => house with { Y = house.Y + 1},
                'v' => house with { Y = house.Y - 1},
                _ => new House(0, 0)
            };
    }

    public record House(int X, int Y);
}