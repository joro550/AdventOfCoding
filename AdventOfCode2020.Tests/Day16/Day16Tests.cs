using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Day16;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2020.Tests.Day16
{
    public class Day16Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day16Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Example1()
        {
            var input = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

            var tickets = TicketParser.Parse(input);
            var invalidFields = new List<long>();

            foreach (var ticket in tickets.NearbyTickets)
            {
                foreach (var field in ticket.Fields)
                {
                    if (tickets.Fields.All(f => !f.Validate(field)))
                    {
                        invalidFields.Add(field);
                    }
                }
            }

            Assert.Equal(71, invalidFields.Sum());
        }
        
        
        [Fact]
        public void Puzzle2Example()
        {
            var input = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9";
            
            var tickets = TicketParser.Parse(input);

            foreach (var ticket in from ticket in tickets.NearbyTickets 
                from field in ticket.Fields 
                where tickets.Fields.All(f => !f.Validate(field)) 
                select ticket)
            {
                ticket.IsValid = false;
            }

            var validTickets = tickets.NearbyTickets.Where(ticket => ticket.IsValid)
                .ToArray();
            
            var fieldDictionary = new Dictionary<int, List<TicketField>>();
            for (var i = 0; i < validTickets.First().Fields.Count; i++) 
                fieldDictionary.Add(i, new List<TicketField>(tickets.Fields));

            foreach (var ticket in validTickets)
            {
                for (int i = 0; i < ticket.Fields.Count; i++)
                {
                    for (var j = 0; j < fieldDictionary[i].Count; j++)
                    {
                        var rule = fieldDictionary[i][j];
                        if (rule.Validate(ticket.Fields[i])) 
                            continue;
                        
                        fieldDictionary[i].RemoveAt(j);
                        j--;
                    }
                }
            }
            
            while (fieldDictionary.Keys.Count != fieldDictionary.Select(d => d.Value.Count).Sum())
            {
                var namesToDelete = (from field in fieldDictionary 
                        where field.Value.Count == 1 
                        select field.Value.First().Name).ToList();
                
                var withMultipleEntry = fieldDictionary.Where(s => s.Value.Count != 1);

                foreach (var entry in withMultipleEntry)
                {
                    for (int i = 0; i < entry.Value.Count; i++)
                    {
                        if (namesToDelete.All(a => a != entry.Value[i].Name)) 
                            continue;
                        
                        entry.Value.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < validTickets.First().Fields.Count; i++)
            {
                var @join = string.Join(",", fieldDictionary[i].Select(field => field.Name));
                _testOutputHelper.WriteLine($"{i} : {@join}");
            }
        }
        
        [Fact]
        public void Puzzle2()
        {
            var input = new FileReader()
                .GetResource("AdventOfCode2020.Tests.Day16.PuzzleInput.txt");
            var tickets = TicketParser.Parse(input);

            // foreach (var ticket in from ticket in tickets.NearbyTickets 
            //     from field in ticket.Fields 
            //     where tickets.Fields.All(f => !f.Validate(field)) 
            //     select ticket)
            // {
            //     ticket.IsValid = false;
            // }
            //
            // var validTickets = tickets.NearbyTickets.Where(ticket => ticket.IsValid)
            //     .ToArray();
            //
            // var fieldDictionary = new Dictionary<int, List<TicketField>>();
            // for (var i = 0; i < validTickets.First().Fields.Count; i++) 
            //     fieldDictionary.Add(i, new List<TicketField>(tickets.Fields));
            //
            //
            // foreach (var ticket in validTickets)
            // {
            //     for (int i = 0; i < ticket.Fields.Count; i++)
            //     {
            //         for (var j = 0; j < fieldDictionary[i].Count; j++)
            //         {
            //             var rule = fieldDictionary[i][j];
            //             if (rule.Validate(ticket.Fields[i])) 
            //                 continue;
            //             
            //             fieldDictionary[i].RemoveAt(j);
            //             j--;
            //         }
            //     }
            // }
            //
            // while (fieldDictionary.Keys.Count != fieldDictionary.Select(d => d.Value.Count).Sum())
            // {
            //     var namesToDelete = (from field in fieldDictionary 
            //         where field.Value.Count == 1 
            //         select field.Value.First().Name).ToList();
            //     
            //     var withMultipleEntry = fieldDictionary.Where(s => s.Value.Count != 1);
            //
            //     foreach (var entry in withMultipleEntry)
            //     {
            //         for (int i = 0; i < entry.Value.Count; i++)
            //         {
            //             if (namesToDelete.All(a => a != entry.Value[i].Name)) 
            //                 continue;
            //             
            //             entry.Value.RemoveAt(i);
            //             i--;
            //         }
            //     }
            // }
            //
            // for (int i = 0; i < validTickets.First().Fields.Count; i++)
            // {
            //     var @join = string.Join(",", fieldDictionary[i].Select(field => field.Name));
            //     _testOutputHelper.WriteLine($"{i} : {@join}");
            // }
            
            /*
                0 : departure date
                1 : departure platform
                2 : arrival station
                3 : departure track
                4 : row
                5 : departure time
                6 : train
                7 : departure station
                8 : duration
                9 : class
                10 : route
                11 : arrival location
                12 : zone
                13 : arrival track
                14 : seat
                15 : wagon
                16 : arrival platform
                17 : type
                18 : departure location
                19 : price
             */

            var departureSum = tickets.MyTicket.Fields[0] *
                               tickets.MyTicket.Fields[1] *
                               tickets.MyTicket.Fields[3] *
                               tickets.MyTicket.Fields[5] *
                               tickets.MyTicket.Fields[7] *
                               tickets.MyTicket.Fields[18];
            _testOutputHelper.WriteLine(departureSum.ToString());
        }
    }
}