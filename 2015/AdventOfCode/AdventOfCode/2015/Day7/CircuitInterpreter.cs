using System;
using System.Collections.Generic;

namespace AdventOfCode._2015.Day7
{
    public static class CircuitInterpreter
    {
        public static Program Parse(string script)
        {
            var instructions = new Dictionary<string, Instruction>();
            var tokens = new TokenQueue(Tokenizer.Tokenize(script));

            var token = tokens.GetNext();
            while (!token.IsEndOfScript())
            {
                var instruction = ToInstruction(token, tokens);
                
                token = tokens.GetNext();
                var wireName = string.Empty;
                while (!token.IsEndOfLine())
                {
                    switch (token.TokenType)
                    {
                        case TokenType.Assign:
                            instruction = AssignInstruction(tokens, instruction);
                            wireName = tokens.GetCurrent().Value;

                            break;
                        case TokenType.And:
                            instruction = AndInstruction(tokens, instruction);
                            break;
                        case TokenType.Or:
                            instruction = OrInstruction(tokens, instruction);
                            break;
                        case TokenType.Not:
                            instruction = NotInstruction(tokens);
                            break;
                        case TokenType.LShift:
                            instruction = LShiftInstruction(tokens, instruction);
                            break;
                        case TokenType.RShift:
                            instruction = RShiftInstruction(tokens, instruction);
                            break;
                    }

                    token = tokens.GetNext();
                }
                
                instructions.Add(wireName, instruction);
                
                token = tokens.GetNext();
            }

            return new Program(instructions);
        }

        private static Instruction RShiftInstruction(TokenQueue tokens, Instruction instruction)
        {
            var rightToken = tokens.GetNext();
            return new RightShiftInstruction(instruction, ToInstruction(rightToken, tokens));
        }

        private static Instruction LShiftInstruction(TokenQueue tokens, Instruction instruction)
        {
            var rightToken = tokens.GetNext();
            return new LeftShiftInstruction(instruction, ToInstruction(rightToken, tokens));;
        }

        private static Instruction NotInstruction(TokenQueue tokens)
        {
            var rightToken = tokens.GetNext();
            return new NotInstruction(ToInstruction(rightToken, tokens));
        }

        private static Instruction OrInstruction(TokenQueue tokens, Instruction instruction)
        {
            var rightToken = tokens.GetNext();
            return new OrInstruction(instruction, ToInstruction(rightToken, tokens));
        }

        private static Instruction AndInstruction(TokenQueue tokens, Instruction instruction)
        {
            var rightToken = tokens.GetNext();
            return new AndInstruction(instruction, ToInstruction(rightToken, tokens));
        }

        private static Instruction AssignInstruction(TokenQueue tokens, Instruction instruction)
        {
            var rightToken = tokens.GetNext();
            return new AssignInstruction(instruction, new GetWireInstruction(rightToken.Value));
        }

        private static Instruction ToInstruction(Token token, TokenQueue tokens)
        {
            Func<Instruction> func = token.TokenType switch
            {
                TokenType.Number => () => new NumberInstruction(ushort.Parse(token.Value)),
                TokenType.WireName => () => new GetWireInstruction(token.Value),
                TokenType.Not => () =>
                {
                    var rightToken = tokens.GetNext();
                    return new NotInstruction(new GetWireInstruction(rightToken.Value));
                },
                _ => () => new NullInstruction()
            };

            return func();
        }
    }

    public class Program
    {
        private readonly Dictionary<string, Instruction> _wires = new();
        
        public Program(Dictionary<string, Instruction> wires) 
            => _wires = wires;

        public ushort GetWireValue(string wireName)
        {
            var result = new ProgramResult(_wires);
            var visitor = new InstructionVisitor(result);

            _wires[wireName].Accept(visitor);
            
            return result.GetResult();
        }
    }

    public class ProgramResult
    {
        private ushort _result;
        private ushort _register;
        private readonly Dictionary<string, Instruction> _wires;
        private readonly Dictionary<string, ushort> _wireValues;

        public ProgramResult(Dictionary<string, Instruction> wires, Dictionary<string, ushort> cache = null)
        {
            _wires = wires;
            _wireValues = cache ?? new Dictionary<string, ushort>();
        }

        public ushort GetWireValue(string wireName)
        {
            if (_wireValues.ContainsKey(wireName))
                return _wireValues[wireName];

            var result = new ProgramResult(_wires, _wireValues);
            var visitor = new InstructionVisitor(result);
            _wires[wireName].Accept(visitor);
            var value = result.GetResult();
            _wireValues.Add(wireName, value);
            
            return value;
        }

        public void SetRegister(ushort value) => _register = value;
        public ushort GetRegister() => _register;

        public void SetResult()
        {
            _result = _register;
            _register = default;
        }

        public ushort GetResult() => _result;

        public Dictionary<string,Instruction> GetWires() 
            => _wires;
    }

    public enum TokenType
    {
        Unknown,
        EndOfLine,
        EndOfScript,

        Number,
        Assign,
        WireName,
        
        And,
        Or,
        LShift,
        RShift,
        Not
    }

    public record Token(TokenType TokenType, string Value)
    {
        public bool IsEndOfLine() => TokenType == TokenType.EndOfLine;
        public bool IsEndOfScript() => TokenType == TokenType.EndOfScript;


        public static Token Assign() => new(TokenType.Assign, string.Empty);
    }

    public abstract record Instruction
    {
        public abstract void Accept(Visitor visitor);
    }
    
    public record NullInstruction : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record NumberInstruction(ushort Number) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record GetWireInstruction(string Name) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record AndInstruction(Instruction Left, Instruction Right) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record OrInstruction(Instruction Left, Instruction Right) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record LeftShiftInstruction(Instruction Left, Instruction Right) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record RightShiftInstruction(Instruction Left, Instruction Right) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record AssignInstruction(Instruction Left, Instruction Right) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record NotInstruction(Instruction Right) : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public abstract class Visitor
    {
        public abstract void VisitInstruction(NullInstruction instruction);
        public abstract void VisitInstruction(NumberInstruction instruction);
        public abstract void VisitInstruction(GetWireInstruction instruction);
        public abstract void VisitInstruction(AssignInstruction instruction);
        public abstract void VisitInstruction(AndInstruction instruction);
        public abstract void VisitInstruction(OrInstruction instruction);
        public abstract void VisitInstruction(LeftShiftInstruction instruction);
        public abstract void VisitInstruction(NotInstruction instruction);
        public abstract void VisitInstruction(RightShiftInstruction instruction);
    }

    public class InstructionVisitor : Visitor
    {
        private readonly ProgramResult _result;

        public InstructionVisitor(ProgramResult result) => _result = result;

        public override void VisitInstruction(NullInstruction instruction)
        {
        }

        public override void VisitInstruction(NumberInstruction instruction)
            => _result.SetRegister(instruction.Number);

        public override void VisitInstruction(GetWireInstruction instruction)
        {
            var wireInstruction = _result.GetWireValue(instruction.Name);
            _result.SetRegister(wireInstruction);
        }

        public override void VisitInstruction(AssignInstruction instruction)
        {
            instruction.Left.Accept(this);
            _result.SetResult();
        }

        public override void VisitInstruction(AndInstruction instruction)
        {
            instruction.Left.Accept(this);
            var register = _result.GetRegister();
            instruction.Right.Accept(this);
            
            _result.SetRegister((ushort)(register & _result.GetRegister()));
        }

        public override void VisitInstruction(OrInstruction instruction)
        {
            instruction.Left.Accept(this);
            var register = _result.GetRegister();
            instruction.Right.Accept(this);
            
            _result.SetRegister((ushort)(register | _result.GetRegister()));
        }

        public override void VisitInstruction(LeftShiftInstruction instruction)
        {
            instruction.Left.Accept(this);
            var register = _result.GetRegister();
            instruction.Right.Accept(this);
            
            _result.SetRegister((ushort)(register << _result.GetRegister()));
        }

        public override void VisitInstruction(NotInstruction instruction)
        {
            instruction.Right.Accept(this);
            var value = (ushort)~_result.GetRegister();

            _result.SetRegister(value);
        }

        public override void VisitInstruction(RightShiftInstruction instruction)
        {
            instruction.Left.Accept(this);
            var register = _result.GetRegister();
            instruction.Right.Accept(this);
            
            _result.SetRegister((ushort)(register >> _result.GetRegister()));
        }
    }
}