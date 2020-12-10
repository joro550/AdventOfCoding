using System;

namespace AdventOfCode2020.Day10
{
    public class Adapter : IComparable<Adapter>
    {
        public int Jolt { get; protected init; }

        public Adapter(int jolt) => Jolt = jolt;


        public int CompareTo(Adapter other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 
                1 : Jolt.CompareTo(other.Jolt);
        }
    }
}