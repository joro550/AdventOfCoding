using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode2020.Tests
{
    public class FileReader
    {
        public string GetResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream(fileName);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            return reader.ReadToEnd();
        }
    }
}