using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
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
    public void Thing()
    {
        var input = FileReader.GetExample("2022", "12");
        var grid = Grid.Parse(input);

        _testOutputHelper.WriteLine(grid.Visualize());
        var gridPosition = PathFinder.Pathfind(grid);
        var count = PathFinder.FindShortestPath(gridPosition);
        Assert.Equal(31, count);
    }
}

file abstract record PathFinder
{
    public static GridPosition Pathfind(Grid grid)
    {
        var end = grid.GetEnd();
        var currentNode = grid.GetStart();
        var unexploredNodes = grid.GetGrid()
            .ToList();

        while (unexploredNodes.Any())
        {
            currentNode = unexploredNodes.OrderBy(x => x.Distance).First();
            unexploredNodes.Remove(currentNode);
            
            if (currentNode == end)
                return end;
            
            var neighbours = currentNode.GetNeighbours(grid);
            
            foreach (var neighbour in neighbours.Where(n => n != null))
            {
                var maximumElevationChange = currentNode.Value + 1;
                var minimumElevationChange = currentNode.Value - 1;
                
                if ( neighbour.Value > maximumElevationChange ||
                     neighbour.Value < minimumElevationChange)
                    continue;

                if (unexploredNodes.All(x => x != neighbour))
                    continue;

                var currentNodeDistance = currentNode.Distance + neighbour.Value;
                var newDistance = currentNodeDistance < neighbour.Distance;

                if (!newDistance)
                    continue;

                neighbour.Distance = currentNodeDistance;
                neighbour.Parent = currentNode;
            }
        }

        return currentNode;
    }

    public static int FindShortestPath(GridPosition position)
    {
        const int i = 1;
        if (position.Parent == null)
            return 1;
        return i + FindShortestPath(position.Parent);
    }
}

file record GridPosition(int X, int Y, int Value = 0, bool IsStart = false, bool IsEnd = false)
{
    public GridPosition(int distance, int x, int y, int value = 0, bool isStart = false, bool isEnd = false)
    : this(x,y, value, isStart, isEnd)
    {
        Distance = distance;
        X = x;
        Y = y;
        Value = value;
        IsStart = isStart;
        IsEnd = isEnd;
    }

    public int Distance { get; set; } = int.MaxValue;
    public GridPosition? Parent { get; set; }
    
    public IEnumerable<GridPosition> GetNeighbours(Grid grid)
    {
        return new[]
        {
            grid.GetPosition(X - 1, Y),
            grid.GetPosition(X + 1, Y),
            grid.GetPosition(X, Y - 1),
            grid.GetPosition(X, Y + 1)
        };
    }
}

file record Grid
{
    private GridPosition[,] _grid;

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
                grid[y, x] = new GridPosition(isStart ? 0 : int.MaxValue, x, y, elevation, isStart, isEnd);
            }
        }
        
        return new Grid { _grid = grid};
    }

    public GridPosition GetPosition(int x, int y)
    {
        if (x < 0 || x >= _grid.GetLength(1) ||
            y < 0 || y >= _grid.GetLength(0))
            return null;
        return _grid[y, x];
    }

    public GridPosition GetStart()
    {
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j].IsStart)
                {
                    return _grid[i, j];
                }
            }
        }

        return null;
    }

    public GridPosition GetEnd()
    {
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j].IsEnd)
                {
                    return _grid[i, j];
                }
            }
        }

        return null;
    }

    public IEnumerable<GridPosition> GetGrid()
    {
        var list = new List<GridPosition>();
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                list.Add(_grid[i,j]);
            }
        }

        return list;
    }

    public string Visualize()
    {
        var list = "";
        for (var i = 0; i < _grid.GetLength(0); i++)
        {
            for (var j = 0; j < _grid.GetLength(1); j++)
            {
                list += _grid[i, j].Value;
            }

            list += Environment.NewLine;
        }

        return list;
    }
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