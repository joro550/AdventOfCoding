using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    public static class BusScheduleParser
    {
        public static BusInfo Parse(string input)
        {
            var strings = input.Split(Environment.NewLine);
            var departureTime = int.Parse(strings[0]);

            var schedules = new List<Bus>();

            foreach (var info in strings[1].Split(","))
            {
                var canParse = int.TryParse(info, out var busId);
                if (canParse)
                {
                    schedules.Add(new Bus(busId));
                }
            }

            return new BusInfo
            {
                EarliestDepartureTime = departureTime,
                Schedules = schedules
            };
        }
        
        
        public static BusInfo Parse2(string input)
        {
            var strings = input.Split(Environment.NewLine);
            var schedules = new List<Bus>();

            var info = strings[1].Split(",").ToArray();

            for (var index = 0; index < info.Length; index++)
            {
                var canParse = int.TryParse(info[index], out var busId);
                if (!canParse) continue;

                schedules.Add(new BusSchedule(busId, index));
            }

            return new BusInfo
            {
                Schedules = schedules
            };
        }
    }
    
    public record BusInfo
    {
        public long EarliestDepartureTime { get; init; }
        public List<Bus> Schedules { get; init; } = 
            new();
        
    }
    
    public record BusSchedule(int Id, int TimeStampDelta) 
        : Bus(Id)
    {
    }
    
    public record Bus(int Id)
    {
        public bool DoesDepartAt(long timeStamp)
        {
            if (timeStamp == 0) return true;
            return timeStamp % Id == 0;
        }
    }
}