namespace AdventOfCode2020.Day8
{
    public record NoOperation (string PlusMinus, int Number)
        : Operation(PlusMinus, Number)
    {
        public override void Execute(ExecutionContext executionContext)
        {
            executionContext.CurrentOperation++;
        }
    }
}