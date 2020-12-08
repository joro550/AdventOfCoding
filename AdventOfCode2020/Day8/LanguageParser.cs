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
        private readonly List<Operation> _operations;
        private readonly ExecutionContext _context;

        public DebugProgram(List<Operation> operations)
        {
            _operations = operations;
            _context = new ExecutionContext();
        }

        public ExecutionContext Execute()
        {
            var visitor = new FindLoop(_context);
            
            while (!_context.Complete && _context.CurrentOperation < _operations.Count)
                try
                {
                    _operations[_context.CurrentOperation].Accept(visitor);
                }
                catch (Exception)
                {
                    
                }

            return _context;
        }
    }

    public class ExecutionContext
    {
        public int CurrentOperation { get; set; }
        public int Accumulator { get; set; }
        public bool Complete { get; set; }
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
            if(_lineNumbers.All(ln => ln != Context.CurrentOperation))
               _lineNumbers.Add(Context.CurrentOperation);
            else
            {
                Context.Complete = true;
                return;
            }
            
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

    public class InfiniteLoopDetectedException : Exception
    {
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