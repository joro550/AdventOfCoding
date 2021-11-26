using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2015.Day9
{
    public class DistanceCalculator
    {
        public int Puzzle1(string input)
        {
            
            
            return 0;
        }
        
        
        
        
        
        public int GetShortestDistance(string input)
        {
            var locations = GetLocations(input);
            var distances = new LocationDistance();
            foreach (var lines in input.Split(Environment.NewLine))
            {
                var parts = lines.Split(" ");

                var firstLocation = parts[0];
                var secondLocation = parts[2];
                var value = long.Parse(parts[^1]);

                var l = locations.Get(firstLocation) & locations.Get(secondLocation);
                distances.SetDistance(l, value);
            }

            return 0;
        }

        private static Locations GetLocations(string input)
        {
            var locations = new Locations();
            foreach (var location in input.Split(Environment.NewLine)
                         .SelectMany(line => line.Split(" "))
                         .Distinct())
            {
                locations.Add(location);
            }
            return locations;
        }
    }

    public class LocationDistance
    {
        private Dictionary<int, long> _distance 
            = new();

        public void SetDistance(int locations, long distance) 
            => _distance.Add(locations, distance);
    }

    public class Locations
    {
        private readonly Dictionary<string, int> _location 
            = new();

        private int _currentValue = 0x01;
        
        public Locations()
        {
            
        }

        public void Add(string locationName)
        {
            _location.Add(locationName, _currentValue);
            _currentValue <<= 0x01;
        }

        public int Get(string location)
        {
            return _location[location];
        }
    }
}