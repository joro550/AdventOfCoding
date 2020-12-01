namespace AdventOfCode2020.Day1
{
    public static class NumberRetriever
    {
        public static Numbers GetNumbersThatReachTarget(int[] numbers, int target)
        {
            foreach (var number in numbers)
            {
                foreach (var n in numbers)
                {
                    var potentialAnswer = new Numbers(number, n);

                    if (potentialAnswer.Add() == target)
                        return potentialAnswer;
                }
            }
            
            return new(0, 0);
        }
        
        public static Triplet GetTripletThatReachTarget(int[] numbers, int target)
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                for (var j = 0; j < numbers.Length; j++)
                {
                    if(i == j) continue;
                    
                    for (var k = 0; k < numbers.Length; k++)
                    {
                        if(i == k || j == k) continue;
                        
                        var triplet = new Triplet(numbers[i], numbers[j], numbers[k]);
                        if (triplet.Add() == target)
                            return triplet;
                    }
                }
            }
            
            return new (0, 0, 0);
        }
    }

    public record Numbers(int FirstNumber, int SecondNumber)
    {           
        public virtual int Add()
            => FirstNumber + SecondNumber;

        public virtual int Multiply() 
            => FirstNumber * SecondNumber;
    }

    public record Triplet(int FirstNumber, int SecondNumber, int ThirdNumber) 
        : Numbers (FirstNumber, SecondNumber)
    {
        public override int Add()
            => FirstNumber + SecondNumber + ThirdNumber;
        
        public override int Multiply() 
            => FirstNumber * SecondNumber * ThirdNumber;
    }
}