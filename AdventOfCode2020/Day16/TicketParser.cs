using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day16
{
    public static class TicketParser
    {
        public static Thing Parse(string input)
        {
            var values = input.Split(Environment.NewLine + Environment.NewLine);
            var rules = GetTicketRules(values[0]);
            
            var tickets = values[1].Split(Environment.NewLine)[1..];
            var myTicket = ParseTicket(tickets, rules);
            
            tickets = values[2].Split(Environment.NewLine)[1..];
            var nearbyTickets = ParseTicket(tickets, rules);

            return new Thing
            {
                MyTicket = myTicket.First(), 
                NearbyTickets = nearbyTickets, 
                Fields = rules
            };
        }

        private static List<Ticket> ParseTicket(IEnumerable<string> tickets, List<TicketField> fields)
        {
            var returnTickets = new List<Ticket>();
            
            foreach (var ticket in tickets)
            {
                var values = ticket.Split(",");
                var ticketFields = new List<long>();
                
                for (int i = 0; i < values.Length; i++)
                {
                    // var ticketField = fields[i] with {Value = int.Parse(values[i])};
                    ticketFields.Add(long.Parse(values[i]));
                }

                returnTickets.Add(new Ticket(ticketFields));
            }
            
            return returnTickets;
        }
        

        private static List<TicketField> GetTicketRules(string values)
        {
            var fields = new List<TicketField>();
            foreach (var rule in values.Split(Environment.NewLine))
            {
                var sides = rule.Split(":");
                var name = sides[0];

                var rangeRule = new List<TicketRule>();
                foreach (var range in sides[^1].Split("or"))
                {
                    var minMax = range.Split("-");
                    
                    var min = int.Parse(minMax[0].Trim());
                    var max = int.Parse(minMax[1].Trim());
                    rangeRule.Add(new RangeRule(min, max));
                }
                fields.Add(new TicketField(name, rangeRule));
            }

            return fields;
        }
    }

    public record Thing
    {
        public Ticket MyTicket { get; init; }
        public List<Ticket> NearbyTickets { get; init; }

        public List<TicketField> Fields = new List<TicketField>();
    }
    
    public record Ticket(List<long> Fields)
    {
        public bool IsValid { get; set; } = true;
    }

    //
    // public record Ticket(List<TicketField> Fields)
    // {
    //     
    // }

    public record TicketField(string Name, List<TicketRule> Rules)
    {
        public int Value { get; init; }

        public bool Validate(long value) 
            => Rules.Any(rule => rule.Validate(value));
    }

    public record RangeRule(int Min, int Max) : TicketRule
    {
        public override bool Validate(long input) 
            => input >= Min && input <= Max;
    }

    public abstract record TicketRule
    {
        public abstract bool Validate(long input);
    }
}