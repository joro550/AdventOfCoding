using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day12
{
    public class InstructionParser
    {
        public static Instruction[] ParseInstructions(string input)
        {
            var instructions = new List<Instruction>();
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                var direction = line[0];
                var number = int.Parse(line[1..]);

                instructions.Add(direction switch
                {
                    'N' => new Instruction(Direction.North, number),
                    'E' => new Instruction(Direction.East, number),
                    'S' => new Instruction(Direction.South, number),
                    'W' => new Instruction(Direction.West, number),
                    'F' => new Instruction(Direction.Forward, number),
                    'L' => new Instruction(Direction.Left, number),
                    'R' => new Instruction(Direction.Right, number),
                    _ => new Instruction(Direction.Forward, 0)
                });
            }
            return instructions.ToArray();

        }
    }


    
    public record Instruction(Direction Direction, int Number);


    public record Position
    {
        public int Vertical { get; init; } = 0;
        public int Horizontal { get; init; } = 0;

        public Position MoveNorth(int number) 
            => this with { Vertical = Vertical + number};
        
        public Position MoveEast(int number)
            => this with { Horizontal = Horizontal + number};
        
        public Position MoveSouth(int number) 
            => this with { Vertical = Vertical - number};

        public Position MoveWest(int number)
            => this with { Horizontal = Horizontal - number};
        
        
        public Position RotateRight()
        {
            return this with { Vertical = -Horizontal, Horizontal = Vertical};
        }
        
        public Position RotateLeft()
        {
            return this with { Vertical = Horizontal, Horizontal = -Vertical};
        }
        
        public static Position operator +(Position one, Position two)
        {
            return one with 
            {
                Vertical = one.Vertical + two.Vertical,
                Horizontal = one.Horizontal + two.Horizontal,
            };

        }

        public (int x, int y) GetPosition()
        {
            var y = Vertical;
            if (y < 0) 
                y *= -1;

            var x = Horizontal;
            if (x < 0)
                x *= -1;

            return (x, y);
        }
        
    };

    public record MoveableObject(FacingDirection FacingDirection, Position Position)
    {
        public virtual MoveableObject PerformInstruction(Instruction instruction)
        {
            return instruction.Direction switch
            {
                Direction.North => this with { Position = Position.MoveNorth(instruction.Number) },
                Direction.East => this with { Position = Position.MoveEast(instruction.Number) },
                Direction.South => this with { Position = Position.MoveSouth(instruction.Number) },
                Direction.West => this with { Position = Position.MoveWest(instruction.Number) },
                
                Direction.Forward => this.FacingDirection switch
                {
                    FacingDirection.North => this with { Position = Position.MoveNorth(instruction.Number) },
                    FacingDirection.East => this with { Position = Position.MoveEast(instruction.Number) },
                    FacingDirection.South => this with { Position = Position.MoveSouth(instruction.Number) },
                    FacingDirection.West => this with { Position = Position.MoveWest(instruction.Number) },
                    _ => this
                },
                
                Direction.Left => this with { FacingDirection = TurnLeft(instruction.Number, FacingDirection)},
                Direction.Right => this with { FacingDirection = TurnRight(instruction.Number, FacingDirection)},
                _ => this
            };
        }

        public FacingDirection TurnLeft(int degree, FacingDirection facingDirection)
        {
            var times = degree / 90;
            var direction = facingDirection;
            
            for (var i = 0; i < times; i++)
            {
                direction = direction switch
                {
                    FacingDirection.North => FacingDirection.West ,
                    FacingDirection.West =>  FacingDirection.South ,
                    FacingDirection.South => FacingDirection.East ,
                    FacingDirection.East => FacingDirection.North ,
                    _ => FacingDirection.East
                };
            }

            return direction;
        }

        public FacingDirection TurnRight(int degree, FacingDirection facingDirection)
        {
            var times = degree / 90;
            var direction = facingDirection;
            
            for (var i = 0; i < times; i++)
            {
                direction = direction switch
                {
                    FacingDirection.North => FacingDirection.East ,
                    FacingDirection.East =>  FacingDirection.South ,
                    FacingDirection.South => FacingDirection.West ,
                    FacingDirection.West => FacingDirection.North ,
                    _ => FacingDirection.East
                };
            }

            return direction;
        }
    }

    public record Waypoint(FacingDirection FacingDirection, Position Position) 
        : MoveableObject(FacingDirection, Position)
    {
        public override Waypoint PerformInstruction(Instruction instruction)
        {
            return base.PerformInstruction(instruction) as Waypoint;
        }

        public Waypoint RotateRight(int degree)
        {
            var times = degree / 90;
            var position = Position;

            for (var i = 0; i < times; i++) 
                position = position.RotateRight();

            return this with {Position = position };
        }

        public Waypoint RotateLeft(int degree)
        {
            var times = degree / 90;
            var position = Position;

            for (var i = 0; i < times; i++) 
                position = position.RotateLeft();

            return this with {Position = position };
        }

    }
    
    public record WaypointShip(FacingDirection FacingDirection, Position Position, Waypoint Waypoint) 
        : MoveableObject(FacingDirection, Position)
    {
        
        public virtual WaypointShip PerformInstruction(Instruction instruction)
        {
            return instruction.Direction switch
            {
                Direction.Forward => MoveToWayPoint(instruction.Number),
                Direction.North => this with { Waypoint = Waypoint.PerformInstruction(instruction) },
                Direction.East => this with {  Waypoint = Waypoint.PerformInstruction(instruction) },
                Direction.South => this with { Waypoint = Waypoint.PerformInstruction(instruction) },
                Direction.West => this with { Waypoint = Waypoint.PerformInstruction(instruction) },
                Direction.Left => this with { Waypoint = Waypoint.RotateLeft(instruction.Number)},
                Direction.Right => this with { Waypoint = Waypoint.RotateRight(instruction.Number)},
                _ => this
            };
        }

        private WaypointShip MoveToWayPoint(int times)
        {
            var position = Position;
            for (var i = 0; i < times; i++) 
                position += Waypoint.Position;

            return this with { Position = position};
        }

        public int GetManhanttanDistance()
        {
            var (x, y) = Position.GetPosition();
            return x + y;
        }
    }
    
    public record Ship(FacingDirection FacingDirection, Position Position) 
        : MoveableObject(FacingDirection, Position)
    {
        public override Ship PerformInstruction(Instruction instruction)
        {
            return base.PerformInstruction(instruction) as Ship;
        }

        public int GetManhanttanDistance()
        {
            var (x, y) = Position.GetPosition();
            return x + y;
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West,
        Forward,
        Left,
        Right,
    }

    public enum FacingDirection
    {
        North,
        East,
        South,
        West
    }
}