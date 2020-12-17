using System;
using System.Text;

namespace AdventOfCode2020.Day17
{
    public record FourDPosition(int X, int Y, int Z, int W) : Position(X, Y, Z)
    {
        public static bool operator <=(FourDPosition left, FourDPosition right) 
            => left.X <= right.X && left.Y <= right.Y && left.Z <= right.Z && left.W <= right.W;

        public static bool operator >=(FourDPosition left, FourDPosition right) 
            => left.X >= right.X && left.Y >= right.Y && left.Z >= right.Z && left.W >= right.W;
        
        public static FourDPosition operator +(FourDPosition left, int num) 
            => left with { X = left.X + num, Y = left.Y + num, Z = left.Z + num, W = left.W + num};
        
        public static FourDPosition operator -(FourDPosition left, int num) 
            => left with { X = left.X - num, Y = left.Y - num, Z = left.Z - num, W = left.W - num};
        
        public override string Base64Encode() 
            => Convert.ToBase64String(Encoding.UTF8.GetBytes($"X:{X},Y:{Y},Z:{Z},W:{W}"));
    }
}