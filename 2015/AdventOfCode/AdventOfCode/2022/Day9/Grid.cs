using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day9;

public class Grid
{
    private Dictionary<(int, int), Cell> _cellDictionary = new();


    private Rope _rope;

    public static Grid New(int amountOfRope = 1)
    {
        var rope = new Rope(new Cell(0,0, true));

        var next = rope;
        for (var i = 0; i < amountOfRope; i++) 
            next = next.SetNext();

        return new()
        {
            _rope = rope,
            _cellDictionary = new Dictionary<(int, int), Cell> {{(0,0), new Cell(0,0, true)}}
        };
    }

    public List<Cell> GetVisitedCells() 
        => _cellDictionary.Values.Where(x => x.Visited).ToList();

    public static Cell GetCell(int x, int y) => new(x, y);

    private void SetVisited(int x, int y)
    {
        if (_cellDictionary.ContainsKey((x, y)))
            return;

        var cell = new Cell(x, y, true);
        _cellDictionary.Add((x, y), cell);
    }

    public Cell GetHead() => _rope.Head();

    public void MoveRope(int x, int y)
    {
        var tail = _rope.Move(x, y);
        var tailCell = tail.GetCell();
        SetVisited(tailCell.X, tailCell.Y);
    }
}

public class Rope
{
    private Cell _head;
    private Rope? _next;

    public Rope(Cell start) => _head = start;

    public Rope Move(int x, int y)
    {
        _head = new Cell(x, y);
        
        if (_next == null)
            return this;

        if (IsTailAdjacent())
            return GetTail();

        var nextHead = _next._head;
        
        var newX = nextHead.X == _head.X ? nextHead.X : nextHead.X < _head.X ? nextHead.X + 1 : nextHead.X - 1;
        var nexY = nextHead.Y == _head.Y ? nextHead.Y : nextHead.Y < _head.Y ? nextHead.Y + 1 : nextHead.Y - 1;
        return _next.Move(newX, nexY);
    }

    private Rope GetTail() 
        => _next == null ? this : _next.GetTail();

    private bool IsTailAdjacent(int distance =  1)
    {
        var tailCoords = _next!._head;
        var headCoords = _head;

        var xDiff = Math.Abs(headCoords.X - tailCoords.X);
        var yDiff = Math.Abs(headCoords.Y - tailCoords.Y);

        return xDiff <= distance && yDiff <= distance;
    }

    public Rope SetNext()
    {
        _next = new Rope(new Cell(0, 0));
        return _next;
    }

    public Cell GetCell() => _head;

    public Cell Head() => _head;
}

public record Cell(int X, int Y, bool Visited)
{
    public Cell(int x, int y)
        : this(x, y, false)
    {
        Visited = true;
    }

    public (int x,int y) GetRightNeighbour() => (X + 1, Y);
    public (int x,int y) GetLeftNeighbour() => (X - 1, Y);
    public (int x,int y) GetTopNeighbour() => (X, Y - 1);
    public (int x,int y) GetBottomNeighbour() => (X, Y+1);
}

public abstract record Instruction(int Amount)
{
    protected abstract Cell GetNextCell(Grid grid);

    public Grid ExecuteInstruction(Grid grid)
    {
        for (var index = 0; index < Amount; index++)
        {
            var newCell = GetNextCell(grid);
            grid.MoveRope(newCell.X, newCell.Y);
        }

        return grid;
    }

    public static IEnumerable<Instruction> Parse(string input)
    {
        return input.SplitByNewLine()
            .Select(instruction => instruction.SplitBySpace())
            .Select(line => (Instruction)(line[0] switch
            {
                "R" => new MoveRightInstruction(int.Parse(line[1])),
                "L" => new MoveLeftInstruction(int.Parse(line[1])),
                "U" => new MoveUpInstruction(int.Parse(line[1])),
                "D" => new MoveDownInstruction(int.Parse(line[1])),
                _ => throw new NotImplementedException()
            })).ToArray();
    }
}

public record MoveUpInstruction(int Amount) : Instruction(Amount)
{
    protected override Cell GetNextCell(Grid grid)
    {
        var head = grid.GetHead();
        var topNeighbour = head.GetTopNeighbour();
        return Grid.GetCell(topNeighbour.x, topNeighbour.y);
    }
}

public record MoveDownInstruction(int Amount) : Instruction(Amount)
{
    protected override Cell GetNextCell(Grid grid)
    {
        var head = grid.GetHead();
        var neighbour = head.GetBottomNeighbour();
        return Grid.GetCell(neighbour.x, neighbour.y);
    }
}

public record MoveRightInstruction(int Amount) : Instruction(Amount)
{
    protected override Cell GetNextCell(Grid grid)
    {
        var head = grid.GetHead();
        var neighbour = head.GetRightNeighbour();
        return Grid.GetCell(neighbour.x, neighbour.y);
    }
}

public record MoveLeftInstruction(int Amount) : Instruction(Amount)
{
    protected override Cell GetNextCell(Grid grid)
    {
        var head = grid.GetHead();
        var neighbour = head.GetLeftNeighbour();
        return Grid.GetCell(neighbour.x, neighbour.y);
    }
}