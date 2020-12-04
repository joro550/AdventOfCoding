using System.Linq;

namespace AdventOfCode._2015.Day1
{
    public static class FloorParser
    {
        public static Floor Parse(string input)
        {
            var floorNumber = input.Aggregate(0, (current, character) => character switch
            {
                '(' => current + 1,
                ')' => current - 1,
                _ => current + 0
            });
            return new Floor(floorNumber);
        }
        
        public static FloorWithBasePosition ParsePuzzle2(string input)
        {
            var floorNumber = 0;
            int? enteredBasementAtPosition = null;
            
            for (var index = 0; index < input.Length; index++)
            {
                floorNumber = input[index] switch
                {
                    '(' => floorNumber + 1,
                    ')' => floorNumber - 1,
                    _ => floorNumber + 0
                };

                if (enteredBasementAtPosition is null && floorNumber == -1) 
                    enteredBasementAtPosition = index + 1;
            }

            return new FloorWithBasePosition(enteredBasementAtPosition, floorNumber);
        }
    }

    public record FloorWithBasePosition(int? BasementAtPosition, int FloorNumber) : Floor(FloorNumber);
    public record Floor(int FloorNumber);
}