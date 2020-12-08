namespace AdventOfCode2020.Day8
{
    public record AccumulateOperation (string PlusMinus, int Number)
        : Operation(PlusMinus, Number)
    {
        public override void Execute(ExecutionContext executionContext)
        {
            switch (PlusMinus)
            {
                case "+":
                    executionContext.Accumulator += Number;
                    break;
                case "-":
                    executionContext.Accumulator -= Number;
                    break;
            }
            executionContext.CurrentOperation++;
        }
    }
}