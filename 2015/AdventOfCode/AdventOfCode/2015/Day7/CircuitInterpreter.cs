using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2015.Day7
{
    public class CircuitInterpreter
    {
        public static Program Parse(string script)
        {
            var tokens = new TokenQueue(Tokenize(script));
            var instructions = new List<Instruction>();

            var token = tokens.GetNext();

            while (token.TokenType != TokenType.EndOfScript)
            {
                var instruction = ToInstruction(token, tokens);
                
                var nextToken = tokens.GetNext();
            
                while (nextToken.TokenType != TokenType.EndOfLine)
                {
                    Token rightToken;
                
                    switch (nextToken.TokenType)
                    {
                        case TokenType.Assign:
                            rightToken = tokens.GetNext();
                            instruction = new AssignInstruction(instruction, new SetWireInstruction(rightToken.Value));
                            break; 
                        case TokenType.And:
                            rightToken = tokens.GetNext();
                            instruction = new AndInstruction(instruction, ToInstruction(rightToken, tokens));
                            break;
                        case TokenType.Or:
                            rightToken = tokens.GetNext();
                            instruction = new OrInstruction(instruction, ToInstruction(rightToken, tokens));
                            break;
                        case TokenType.Not:
                            rightToken = tokens.GetNext();
                            instruction = new NotInstruction(ToInstruction(rightToken, tokens));
                            break;
                        case TokenType.LShift:
                            rightToken = tokens.GetNext();
                            instruction = new LeftShiftInstruction(instruction, ToInstruction(rightToken, tokens));
                            break;
                        case TokenType.RShift:
                            rightToken = tokens.GetNext();
                            instruction = new RightShiftInstruction(instruction, ToInstruction(rightToken, tokens));
                            break;
                    }
                
                    nextToken = tokens.GetNext();
                }
                

                instructions.Add(instruction);
                token = tokens.GetNext();
            }


            return new Program(instructions);
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
        

        private static IEnumerable<Token> Tokenize(string script)
        {
            var tokens = new List<Token>();
            foreach (var line in script.Split(Environment.NewLine))
            {
                tokens.AddRange(line.Split(" ").Select(ToToken));
                tokens.Add(new Token(TokenType.EndOfLine, string.Empty));
            }

            tokens.Add(new Token(TokenType.EndOfScript, string.Empty));
            return tokens;
        }

        private static Token ToToken(string word)
        {
            return word switch
            {
                "->" => new Token(TokenType.Assign, string.Empty),
                "AND" => new Token(TokenType.And, string.Empty),
                "OR" => new Token(TokenType.Or, string.Empty),
                "LSHIFT" => new Token(TokenType.LShift, string.Empty),
                "RSHIFT" => new Token(TokenType.RShift, string.Empty),
                "NOT" => new Token(TokenType.Not, string.Empty),
                _ => int.TryParse(word, out var value)
                    ? new Token(TokenType.Number, value.ToString())
                    : new Token(TokenType.WireName, word)
            };
        }
    }


    public class TokenQueue
    {
        private readonly Queue<Token> _tokenQueue;

        public TokenQueue(IEnumerable<Token> tokens) 
            => _tokenQueue = new Queue<Token>(tokens);

        public Token GetNext()
        {
            if (_tokenQueue.TryDequeue(out var token))
                return token;
            return new Token(TokenType.Unknown, string.Empty);
        }

        public Token PeekNext()
        {
            if(_tokenQueue.TryPeek(out var token))
                return token;
            return new Token(TokenType.Unknown, string.Empty);
        }
    }

    public class Program
    {
        private readonly List<Instruction> _instructions;
        
        public Program(List<Instruction> instructions) 
            => _instructions = instructions;

        public ProgramResult Run()
        {
            var result = new ProgramResult();
            var visitor = new InstructionVisitor(result);

            foreach (var instruction in _instructions) 
                instruction.Accept(visitor);

            return result;
        }
    }

    public class ProgramResult
    {
        private readonly Dictionary<string, ushort> _wires = new();
        private ushort _register;

        public ushort GetWireValue(string wireName) 
            => _wires.ContainsKey(wireName) ? _wires[wireName] : (ushort)0;

        public void SetWireValue(string wireName, ushort value)
        {
            if (!_wires.ContainsKey(wireName))
                _wires.Add(wireName, 0);
            _wires[wireName] = value;
        }

        public void SetRegister(ushort value) => _register = value;
        public ushort GetRegister() => _register;
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

    public record Token(TokenType TokenType, string Value);

    public abstract record Instruction
    {
        public abstract void Accept(Visitor visitor);
    }
    
    public record NullInstruction : Instruction
    {
        public override void Accept(Visitor visitor) => visitor.VisitInstruction(this);
    }

    public record ClearInstruction : Instruction
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

    public record SetWireInstruction(string Name) : Instruction
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
        public abstract void VisitInstruction(SetWireInstruction instruction);
        public abstract void VisitInstruction(AssignInstruction instruction);
        public abstract void VisitInstruction(AndInstruction instruction);
        public abstract void VisitInstruction(OrInstruction instruction);
        public abstract void VisitInstruction(LeftShiftInstruction instruction);
        public abstract void VisitInstruction(NotInstruction instruction);
        public abstract void VisitInstruction(RightShiftInstruction instruction);
        public abstract void VisitInstruction(ClearInstruction instruction);
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
            => _result.SetRegister(_result.GetWireValue(instruction.Name));

        public override void VisitInstruction(SetWireInstruction instruction)
        {
            var register = _result.GetRegister();
            _result.SetWireValue(instruction.Name, register);
        }

        public override void VisitInstruction(AssignInstruction instruction)
        {
            instruction.Left.Accept(this);
            instruction.Right.Accept(this);
            new ClearInstruction().Accept(this); // clear register
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

        public override void VisitInstruction(ClearInstruction instruction) 
            => _result.SetRegister(0);
    }
}