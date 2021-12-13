module AdventOfCodeFsharp.library

open System.IO
open System.Reflection

let loadFileAsync (fileName: string) =
        let assembly = Assembly.GetExecutingAssembly();
        let stream = assembly.GetManifestResourceStream(fileName)
        let reader = new StreamReader(stream)
        reader.ReadToEndAsync() 
        
        
        