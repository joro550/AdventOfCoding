using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AdventOfCode2020.Day13;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day13
{
    public class Day13Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        [Fact]
        public void ParserTest()
        {
            var input = "939|7,13,x,x,59,x,31,19".Replace("|", Environment.NewLine);
            var busInfo = BusScheduleParser.Parse(input);

            Assert.Equal(939, busInfo.EarliestDepartureTime);
            Assert.Equal(5, busInfo.Schedules.Count);
        }
        
        [Theory]
        [InlineData(5, 0, 5, 10, 15)]
        [InlineData(11, 0, 11, 22, 33)]
        [InlineData(7, 931, 938, 945)]
        [InlineData(13, 936, 949)]
        public void WhenTimeStampIsDivisibleById_BusDepartsIsTrue(int id, params int[] departTimes)
        {
            var schedule = new Bus(id);

            foreach (var departTime in departTimes) 
                Assert.True(schedule.DoesDepartAt(departTime));
        }

        [Fact]
        public void Example1()
        {
            var input = "939|7,13,x,x,59,x,31,19".Replace("|", Environment.NewLine);
            var busInfo = BusScheduleParser.Parse(input);

            var canDepart = false;
            var departTime = 0L;
           
            Bus bus = null;
            
            for ( var timeStamp = busInfo.EarliestDepartureTime; canDepart == false; timeStamp++)
            {
                foreach (var schedule in busInfo.Schedules)
                {
                    canDepart = schedule.DoesDepartAt(timeStamp);
                    if (!canDepart) continue;
                    
                    bus = schedule;
                    departTime = timeStamp;
                    break;
                }
            }

            var sum = (departTime - busInfo.EarliestDepartureTime) * bus.Id;
            Assert.Equal(295, sum);
        }

        [Fact]
        public void SolvePuzzle1()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day13.PuzzleInput.txt");
            var busInfo = BusScheduleParser.Parse(input);

            var canDepart = false;
            var departTime = 0L;
           
            Bus bus = null;
            
            for ( var timeStamp = busInfo.EarliestDepartureTime; canDepart == false; timeStamp++)
            {
                foreach (var schedule in busInfo.Schedules)
                {
                    canDepart = schedule.DoesDepartAt(timeStamp);
                    if (!canDepart) continue;
                    
                    bus = schedule;
                    departTime = timeStamp;
                    break;
                }
            }

            var sum = (departTime - busInfo.EarliestDepartureTime) * bus.Id;
            Assert.Equal(333, sum);
        }
        
        public static TheoryData<string, (int, int)[]> Data2 = new()
        {
            {"939|7,13,x,x,59,x,31,19", new [] {(7, 0), (13, 1)}},
        };

        public Day13Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [MemberData(nameof(Data2))]
        public void ParserTest2(string input, params (int id, int departTime)[] info)
        {
            var busInfo = BusScheduleParser.Parse2(input.Replace("|", Environment.NewLine));
            Assert.Equal(5, busInfo.Schedules.Count);

            for (var index = 0; index < info.Length; index++)
            {
                var (id, departAtMinute) = Assert.IsType<BusSchedule>(busInfo.Schedules[index]);
                Assert.Equal(info[index].id, id);
                Assert.Equal(info[index].departTime, departAtMinute);
            }
        }

        [Theory]
        [InlineData("939|7,13,x,x,59,x,31,19", 1068788)]
        public void Example2(string input, long expectedTimestamp)
        {
            var busInfo = BusScheduleParser.Parse2(input.Replace("|", Environment.NewLine));

            var cache = new Cache(busInfo.Schedules.Select(b => b.Id));
            
            var canDepart = false;
            var departTime = 0L;

            for (var timeStamp = 1; canDepart == false; timeStamp++)
            {
                cache.Tick(timeStamp);
                
                foreach (var schedule in busInfo.Schedules.OfType<BusSchedule>())
                {
                    var canBusDepart = schedule.DoesDepartAt(timeStamp);
                    if (!canBusDepart) continue;
                    if (!cache.AddDeparture(schedule, timeStamp)) continue;
                    
                    departTime = timeStamp;
                    canDepart = true;
                    break;
                }
            }
            
            Assert.Equal(expectedTimestamp, departTime);
        }
        
        [Theory]
        [InlineData("939|7,13,x,x,59,x,31,19", 1068781)]
        [InlineData("939|17,x,13,19", 3417)]
        [InlineData("939|67,7,59,61", 754018)]
        [InlineData("939|67,x,7,59,61", 779210)]
        [InlineData("939|1789,37,47,1889", 1202161486)]
        public void Example2_CRT(string input, long expectedTimestamp)
        {
            var busInfo = BusScheduleParser.Parse2(input.Replace("|", Environment.NewLine));

            var offset = 0;
            var period = ((BusSchedule) busInfo.Schedules[0]).Id;
            foreach (var schedule in busInfo.Schedules.OfType<BusSchedule>().ToArray()[1..])
            {
                while ((offset + schedule.TimeStampDelta) % schedule.Id != 0)
                    offset += period;
                period *= schedule.Id;
            }
            
            Assert.Equal(expectedTimestamp, offset);
        }
        
        [Fact]
        public void SolvePuzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day13.PuzzleInput.txt");
            var busInfo = BusScheduleParser.Parse2(input);

            var offset = 0L;
            long period = ((BusSchedule) busInfo.Schedules[0]).Id;
            foreach (var schedule in busInfo.Schedules.OfType<BusSchedule>().ToArray()[1..])
            {
                while ((offset + schedule.TimeStampDelta) % schedule.Id != 0)
                    offset += period;
                period *= schedule.Id;
            }

            _testOutputHelper.WriteLine(offset.ToString());
        }
    }

    public class Cache
    {
        private readonly int[] _busIds;
        private readonly int _firstBusId;
        
        private int _nextBusId;
        private bool _shouldClearNextTick;
        private long _timeStampCache;
        
        private readonly List<int> _currentCache = new();
        private long _tStamp;

        public Cache(IEnumerable<int> busIds)
        {
            _busIds = busIds.ToArray();
            _firstBusId = _nextBusId = _busIds.First();
        }

        public void Tick(long timestamp)
        {
            // If the timestamp has ticked over and we we haven't got all the
            // needed ids then just reset and try again
            if (_shouldClearNextTick && _timeStampCache != timestamp && _currentCache.Any())
                ResetCache();
        }
        
        public bool AddDeparture(BusSchedule schedule, long timestamp)
        {
            var delta = timestamp - _tStamp;
            var (id, timeStampDelta) = schedule;
            // If we got the busId that we expected add it to the cache
            // and move the next bus number along
            if (id == _nextBusId && _tStamp == 0 || delta == timeStampDelta)   
            {
                _currentCache.Add(id);
                _nextBusId = NextBusId(id);
                
                if (id == _firstBusId)
                    _tStamp = timestamp;
            }
            // If we didn't get the bus number we have to clear 
            // everything in the next tick
            else
            {
                _shouldClearNextTick = true;            
                _timeStampCache = timestamp;
            }

            return _currentCache.Count == _busIds.Length;
        }

        private void ResetCache()
        {
            _tStamp = 0;
            _shouldClearNextTick = false;
            _currentCache.Clear();
            _nextBusId = _busIds.First();
        }

        private int NextBusId(int busId)
        {
            for (var i = 0; i < _busIds.Length; i++)
            {
                if (busId != _busIds[i]) 
                    continue;
                
                var nextIndex = i + 1;
                return nextIndex >= _busIds.Length 
                    ? _firstBusId 
                    : _busIds[nextIndex];
            }

            return _firstBusId;
        }
    }
}