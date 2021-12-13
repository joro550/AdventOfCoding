namespace AdventOfCode.Lib;

public interface ISolution<out T>
{
    T RunPuzzle1(string input);
    T RunPuzzle2(string input);
}