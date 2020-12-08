namespace AdventOfCode2020.Day8
{
    public abstract record Operation(string PlusMinus, int Number)
    {
        public abstract void Execute(ExecutionContext executionContext);
    }
}