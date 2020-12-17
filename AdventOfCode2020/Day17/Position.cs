using System;
using System.Text;

namespace AdventOfCode2020.Day17
{
    public record Position(int X, int Y, int Z)
    {
        public static bool operator <=(Position left, Position right) 
            => left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z;

        public static bool operator >=(Position left, Position right) 
            => left.X >= right.X && left.Y >= right.Y && left.Z >=right.Z;

        public static Position operator +(Position left, int num) 
            => left with { X = left.X + num, Y = left.Y + num, Z = left.Z + num};
        
        public static Position operator -(Position left, int num) 
            => left with { X = left.X - num, Y = left.Y - num, Z = left.Z - num};
        
        public virtual string Base64Encode() 
            => Convert.ToBase64String(Encoding.UTF8.GetBytes($"X:{X},Y:{Y},Z:{Z}"));
    }
}