using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode.Tests
{
    public static class FileReader
    {
        public static string GetResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(fileName);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            return reader.ReadToEnd();
        }

        public static string GetResource(string year, string day) 
            => GetResource($"AdventOfCode.Tests._{year}.Day{day}.PuzzleInput.txt");
    }
}