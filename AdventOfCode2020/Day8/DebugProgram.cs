using System.Collections.Generic;

namespace AdventOfCode2020.Day8
{
    public record DebugProgram
    {
        public readonly List<Operation> Operations;

        public DebugProgram(List<Operation> operations) 
            => Operations = operations;

        public ExecutionContext Execute()
        {
            var context = new ExecutionContext();
            
            while (!context.Complete && context.CurrentOperation < Operations.Count)
            {
                var operation = Operations[context.CurrentOperation];
                context.Execute(operation);
            }

            return context;
        }
    }
}