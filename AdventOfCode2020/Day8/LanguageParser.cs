using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day8
{
    public static class LanguageParser
    {
        private static readonly Dictionary<Regex, Func<string, int, Operation>> Keywords =
            new()
            {
                {new Regex("nop (-|\\+)([0-9]+)"), (pm, n) => new NoOperation(pm, n)},
                {new Regex("jmp (-|\\+)([0-9]+)"), (pm, n) => new JumpOperation(pm, n)},
                {new Regex("acc (-|\\+)([0-9]+)"), (pm, n) => new AccumulateOperation(pm, n)}
            };
        
        public static DebugProgram Parse(string input)
        {
            var operations = (from line in input.Split(Environment.NewLine)
                from keyword in Keywords
                let match = keyword.Key.Match(line)
                where match.Success
                let plusMinus = match.Groups[1].Value
                let number = int.Parse(match.Groups[2].Value)
                select keyword.Value(plusMinus, number)).ToList();

            return new DebugProgram(operations);
        }
    }
    
    public record DebugProgram
    {
        public readonly List<Operation> Operations;

        public DebugProgram(List<Operation> operations)
        {
            Operations = operations;
        }

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

    public class ExecutionContext
    {
        public int CurrentOperation { get; set; }
        public int Accumulator { get; set; }
        public bool Complete { get; set; }
        
        private readonly List<int> _lineNumbers = new();

        public void Execute(Operation operation)
        {
            operation.Accept(new FindLoop(this));
            
            if(_lineNumbers.All(ln => ln != CurrentOperation))
                _lineNumbers.Add(CurrentOperation);
            else
                Complete = true;
        }
        
    }
    
    public record FindLoop : OperationVisitor
    {
        private readonly List<int> _lineNumbers = new();
        
        public FindLoop(ExecutionContext context) 
            :base(context)
        {
        }

        public override void Visit(NoOperation operation)
        {
            Context.CurrentOperation++;
        }

        public override void Visit(AccumulateOperation operation)
        {
            switch (operation.PlusMinus)
            {
                case "+":
                    Context.Accumulator += operation.Number;
                    break;
                case "-":
                    Context.Accumulator -= operation.Number;
                    break;
            }
            Context.CurrentOperation++;
        }

        public override void Visit(JumpOperation operation)
        {
            switch (operation.PlusMinus)
            {
                case "+":
                    Context.CurrentOperation += operation.Number;
                    break;
                case "-":
                    Context.CurrentOperation -= operation.Number;
                    break;
            }
        }
    }

    public abstract record OperationVisitor
    {
        public readonly ExecutionContext Context;

        protected OperationVisitor(ExecutionContext context) 
            => Context = context;

        public abstract void Visit(NoOperation operation);
        public abstract void Visit(AccumulateOperation operation);
        public abstract void Visit(JumpOperation operation);
    }

    public record JumpOperation(string PlusMinus, int Number)
        : Operation(PlusMinus, Number)
    {
        public override void Accept(OperationVisitor visitor) 
            => visitor.Visit(this);
    }
    
    public record AccumulateOperation (string PlusMinus, int Number)
        : Operation(PlusMinus, Number)
    {
        public override void Accept(OperationVisitor visitor) 
            => visitor.Visit(this);
    }  
    
    public record NoOperation (string PlusMinus, int Number)
        : Operation(PlusMinus, Number)
    {
        public override void Accept(OperationVisitor visitor) 
            => visitor.Visit(this);
    }

    public abstract record Operation(string PlusMinus, int Number)
    {
        public abstract void Accept(OperationVisitor visitor);
    }
}