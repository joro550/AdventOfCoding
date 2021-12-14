module AdventOfCodeFsharp.library

open System.IO
open System.Reflection

let loadFileAsync (fileName: string) =
        async {
            let assembly = Assembly.GetExecutingAssembly();
            let stream = assembly.GetManifestResourceStream(fileName)
            let reader = new StreamReader(stream)
            return! reader.ReadToEndAsync() |> Async.AwaitTask
        }
        
        
        