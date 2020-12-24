using System;
using System.Text;

namespace AdventOfCode2020.Day24
{
    public record Position(float X, float Y)
    {
        public string GetKey()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"X:{X}, Y:{Y}");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}