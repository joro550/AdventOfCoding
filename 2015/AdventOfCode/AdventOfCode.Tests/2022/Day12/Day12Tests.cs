using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit.Abstractions;

namespace AdventOfCode.Tests._2022.Day12;

public class Day12Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day12Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Example1()
    {
        var input = FileReader.GetExample("2022", "12");
        var grid = Grid.Parse(input);

        var gridPosition = PathFinder.Pathfind(grid);
        var list = PathFinder.FindShortestPath(gridPosition);
        Assert.Equal(31, list - 1);
    }

    [Fact]
    public void Example2()
    {
        var input = FileReader.GetExample("2022", "12");
        var grid = Grid.Parse(input);

        var startingPositions = grid.GetGrid().Where(x => x.Value == 1);
        var shortestPath = int.MaxValue;

        foreach (var startingPosition in startingPositions)
        {
            var gridPosition = PathFinder.Pathfind(grid, startingPosition);
            var list = PathFinder.FindShortestPath(gridPosition)- 1;
            if (list < shortestPath)
            {
                shortestPath = list;
            }
        }
        
        Assert.Equal(29, shortestPath);
    }

    [Fact]
    public void Puzzle1()
    {
        var input = FileReader.GetResource("2022", "12");
        var grid = Grid.Parse(input);

        var gridPosition = PathFinder.Pathfind(grid);
        var list = PathFinder.FindShortestPath(gridPosition);
        Assert.Equal(462, list - 1);
    }

    [Fact]
    public void Puzzle2()
    {
        var input = FileReader.GetResource("2022", "12");
        var grid = Grid.Parse(input);

        var startingPositions = grid.GetGrid().Where(x => x.Value == 1);
        var shortestPath = int.MaxValue;

        foreach (var startingPosition in startingPositions)
        {
            var gridPosition = PathFinder.Pathfind(grid, startingPosition);
            var list = PathFinder.FindShortestPath(gridPosition)- 1;
            if (list < shortestPath)
            {
                shortestPath = list;
            }

        }
        
        Assert.Equal(451, shortestPath);
    }
}

file abstract record PathFinder
{
    public static GridPosition Pathfind(Grid grid, GridPosition? start = null)
    {
        var nodes = grid.GetGrid().ToList();

        foreach (var node in nodes)
        {
            node.gCost = int.MaxValue;
            node.hCost = int.MaxValue;
            node.Parent = null;
        }

        start ??= nodes.First(x => x.IsStart);
        var end = nodes.First(x => x.IsEnd);
        
        start.gCost = 0;
        start.hCost = CalculateDistance(start, end);

        var closedList = new HashSet<Coords>();
        
        var queue = new PriorityQueue<GridPosition, int>();
        queue.Enqueue(start, start.fCost);
        
        var openList2 = new HashSet<Coords> { start.GetCoords() };

        while (openList2.Any())
        {
            var current = queue.Dequeue();

            closedList.Add(current.GetCoords());
            openList2.Remove(current.GetCoords());

            if (current == end)
                return end;
            
            var maximumElevationIncrease = current.Value + 1;

            foreach (var neighbour in current.GetNeighbours(grid))
            {
                if(neighbour.Value > maximumElevationIncrease)
                    continue;
                
                if(closedList.Contains(neighbour.GetCoords()))
                    continue;
                
                var tentativeGCost = current.gCost + CalculateDistance(current, neighbour);
                if (tentativeGCost > neighbour.gCost)
                    continue;

                neighbour.Parent = current;
                neighbour.gCost = tentativeGCost;
                neighbour.hCost = CalculateDistance(neighbour, end);

                if (openList2.Contains(neighbour.GetCoords())) 
                    continue;
                
                openList2.Add(neighbour.GetCoords());
                queue.Enqueue(neighbour, neighbour.fCost);
            }
        }

        return null;
    }

    private static int CalculateDistance(GridPosition current, GridPosition dest)
    {
        var xDistance = Math.Abs(current.X - dest.X);
        var yDistance = Math.Abs(current.Y - dest.Y);
        return Math.Min(xDistance, yDistance) + 10 * Math.Abs(xDistance - yDistance);
    }

    public static int FindShortestPath(GridPosition position)
    {
        if (position == null)
            return int.MaxValue;
        
        var count = 0;

        var currentNode = position;
        do
        {
            currentNode = currentNode.Parent;
            count += 1;
        } while (currentNode != null);

        return count;
    }
}

file record Coords(int X, int Y);

file record GridPosition(int X, int Y, int Value = 0, bool IsStart = false, bool IsEnd = false)
{
    private Coords _coords = new(X, Y);
    private List<GridPosition> _neighbours = new();
    
    public int gCost { get; set; } = int.MaxValue;
    public int hCost { get; set; } = int.MaxValue;
    public int fCost => gCost + hCost;

    public GridPosition? Parent { get; set; }
    
    public IEnumerable<GridPosition> GetNeighbours(Grid grid)
    {
        if (_neighbours.Count > 0)
            return _neighbours;

        return _neighbours.AddWhere(grid.GetPosition(X - 1, Y), position => position != null)
            .AddWhere(grid.GetPosition(X + 1, Y), position => position != null)
            .AddWhere(grid.GetPosition(X, Y - 1), position => position != null)
            .AddWhere(grid.GetPosition(X, Y + 1), position => position != null);
    }

    public Coords GetCoords() => _coords;
}

file record Grid
{
    private GridPosition[,] _grid;
    private List<GridPosition> _flatGrid;

    public static Grid Parse(string input)
    {
        var lines = input.SplitByNewLine();

        var height = lines.Length;
        var width = lines[0].Length;

        var grid = new GridPosition[height, width];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                var value = lines[y][x].ToString();
                
                var elevation = Elevation.GetElevation(value);
                var isStart = value == "S";
                var isEnd = value == "E";
                grid[y, x] = new GridPosition(x, y, isEnd ? 26 : elevation, isStart, isEnd);
            }
        }
        
        return new Grid { _grid = grid, _flatGrid = ToFlatGrid(grid)};
    }

    private static List<GridPosition> ToFlatGrid(GridPosition[,] grid)
    {
        var list = new List<GridPosition>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                list.Add(grid[i,j]);
            }
        }
        return list;
    }

    public GridPosition GetPosition(int x, int y)
    {
        if (x < 0 || x >= _grid.GetLength(1) ||
            y < 0 || y >= _grid.GetLength(0))
            return null;
        return _grid[y, x];
    }

    public IEnumerable<GridPosition> GetGrid() => _flatGrid;
}

file static class Elevation
{
    private static readonly Dictionary<string, int> _elevation 
        = FillElevations();

    public static int GetElevation(string value) => _elevation.ContainsKey(value) ? _elevation[value] : 1;

    private static Dictionary<string,int> FillElevations()
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        var dictionary = new Dictionary<string, int>();
        
        for (var i = 0; i < alphabet.Length; i++)
        {
            dictionary.Add(alphabet[i].ToString(), i+1);
        }

        return dictionary;
    }
}