namespace AdventOfCode2020.Day8
{
    public record JumpOperation(string PlusMinus, int Number)
        : Operation(PlusMinus, Number)
    {
        public override void Execute(ExecutionContext executionContext)
        {
            switch (PlusMinus)
            {
                case "+":
                    executionContext.CurrentOperation += Number;
                    break;
                case "-":
                    executionContext.CurrentOperation -= Number;
                    break;
            }
        }
    }
}