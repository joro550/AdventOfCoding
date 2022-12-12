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
        var nodes = grid.GetGrid().ToList();

        var start = nodes.First(x => x.IsStart);
        var end = nodes.First(x => x.IsEnd);
        start.fCost = 0;
        start.gCost = 0;
        start.hCost = 0;

        var openList = new List<GridPosition> { start };
        var closedList = new List<GridPosition>();

        while (openList.Any())
        {
            var current = openList.OrderBy(x => x.fCost).First();
            openList.Remove(current);
            closedList.Add(current);
            
            if (current == end)
                return end;

            foreach (var neighbour in current.GetNeighbours(grid).Where(x => x != null))
            {
                if(closedList.Contains(neighbour))
                    continue;
                
                var maximumElevationIncrease = current.Value + 1;
                var minimumElevationIncrease = current.Value - 1;
                
                if(neighbour.Value > maximumElevationIncrease ||
                   neighbour.Value < minimumElevationIncrease)
                    continue;

                var gNew = current.gCost + 1;
                var hNew = CalculateH(current.X + neighbour.X, current.Y + neighbour.Y, end);
                var fNew = gNew + hNew;
                
                if(fNew > neighbour.fCost)
                    continue;

                neighbour.gCost = gNew;
                neighbour.fCost = fNew;
                neighbour.hCost = hNew;
                neighbour.Parent = current;
                
                if(!openList.Contains(neighbour))
                    openList.Add(neighbour);
            }
        }

        return end;
    }
    static double CalculateH(int x, int y, GridPosition dest) 
        => Math.Sqrt((x - dest.X) * (x - dest.X) + (y - dest.Y) * (y - dest.Y));

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
    public int gCost { get; set; } = int.MaxValue;
    public double hCost { get; set; } = double.MaxValue;
    public double fCost { get; set; } = double.MaxValue;

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
                grid[y, x] = new GridPosition(x, y, elevation, isStart, isEnd);
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