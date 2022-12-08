using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Tests;

namespace AdventOfCode._2022.Day8;

public class TreeGrid
{
    private readonly int[,] _grid;

    private TreeGrid(int[,] grid) 
        => _grid = grid;

    public static TreeGrid Parse(string input)
    {
        var lines = input.SplitByNewLine();
        var height = lines.Length;
        var width = lines[0].Length;
        
        var grid = new int[width, height]; 

        for (var y = 0; y < height; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                grid[y, x] = int.Parse(line[x].ToString());
            }
        }

        return new TreeGrid(grid);
    }

    public int CountVisibleTrees()
    {
        var count = 0;
        
        for (var height = 0; height < _grid.GetLength(0); height++)
        {
            for (var width = 0; width < _grid.GetLength(1); width++)
            {
                if (IsTreeVisible(width, height)) 
                    count++;
            }
        }
        return count;
    }
    
    

    public long GetBestScenicScore()
    {
        var scenicScore = 0L;
        
        for (var height = 0; height < _grid.GetLength(0); height++)
        {
            for (var width = 0; width < _grid.GetLength(1); width++)
            {
                var newScenicScore = GetScenicScore(width, height);
                if (newScenicScore > scenicScore)
                    scenicScore = newScenicScore;
            }
        }
        return scenicScore;
    }
    
    private long GetScenicScore(int x, int y)
    {
        var ourValue = _grid[y, x];
        var upCount = CountTreesWeCanSee(ourValue, LookingUpColumn(x, y));
        var downCount = CountTreesWeCanSee(ourValue, LookingDownColumn(x, y));
        var upRow = CountTreesWeCanSee(ourValue, LookingUpRow(x, y));
        var downRow = CountTreesWeCanSee(ourValue, LookingDownRow(x, y));
        

        return upCount * downCount * upRow * downRow;
    }

    private static long CountTreesWeCanSee(int ourValue, IEnumerable<int> lookingUpColumn)
    {
        var count = 0L;
        foreach (var neighbours in lookingUpColumn)
        {
            if (neighbours >= ourValue)
            {
                count++;
                break;
            }

            count++;
        }

        return count;
    }

    private bool IsTreeVisible(int x, int y)
    {
        if (x == 0 || y == 0 || x == _grid.GetLength(1) || y == _grid.GetLength(0))
            return true;

        var ourValue = _grid[y, x];

        var upColumn = LookingUpColumn(x, y);
        if (upColumn.All(t => t < ourValue))
            return true;

        var downColumn = LookingDownColumn(x, y);
        if (downColumn.All(t => t < ourValue))
            return true;
        
        var upRow = LookingUpRow(x, y);
        if (upRow.All(t => t < ourValue))
            return true;
        
        var downRow = LookingDownRow(x, y);
        if (downRow.All(t => t < ourValue))
            return true;

        return false;
    }

    private IEnumerable<int> LookingDownColumn(int x, int y)
    {
        var neighbourList = new List<int>();
        for (var i = y+1; i < _grid.GetLength(0); i++)
        {
            neighbourList.Add(_grid[i,x]);
        }

        return neighbourList.ToArray();
    }
    
    private IEnumerable<int> LookingUpColumn(int x, int y)
    {
        var neighbourList = new List<int>();
        for (var i = y-1; i >= 0; i--)
        {
            neighbourList.Add(_grid[i,x]);
        }

        return neighbourList.ToArray();
    }

    private IEnumerable<int> LookingDownRow(int x, int y)
    {
        var neighbourList = new List<int>();
        for (int i = x+1; i < _grid.GetLength(1); i++)
        {
            neighbourList.Add(_grid[y,i]);
        }

        return neighbourList.ToArray();
    }
    
    private IEnumerable<int> LookingUpRow(int x, int y)
    {
        var neighbourList = new List<int>();
        for (var i = x-1; i >= 0; i--)
        {
            neighbourList.Add(_grid[y,i]);
        }

        return neighbourList.ToArray();
    }
}