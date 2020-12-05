using System;
using System.Linq;

namespace AdventOfCode._2015.Day2
{
    public static class RecordParser
    {
        public static Rectangle Parse(string input)
        {
            var values = input.Split("x");
            return new Rectangle(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
        }
    }
    
    public record Rectangle(int Length, int Width, int Height)
    {
        public WrappingPaper ConvertToWrappingPaper()
        {
            var values = new[]
            {
               Length * Width,
               Width * Height,
               Height * Length
            };

            var exactMeasurement = values.Aggregate(0, (c, v) => c + 2 * v);
            return new WrappingPaper(exactMeasurement, values.Min());
        }
        
        public Ribbon ConvertToRibbon()
        {
            var perimeters = new[]
            {
                Length + Length + Width + Width,
                Width + Width + Height + Height,
                Length + Length + Height + Height
            };

            return new(perimeters.Min(), Length * Width * Height);
        }
    }

    public record WrappingPaper(int ExactMeasurement, int Slack)
    {
        public int Total() 
            => ExactMeasurement + Slack;
    }

    public record Ribbon(int ExactLength, int BowLength)
    {
        public int Total() 
            => ExactLength + BowLength;
    }
}