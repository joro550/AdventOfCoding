module AdventOfCodeFsharp._2021.Day1

open System

let increaseCalculator (contents: string)=
    let lines = contents.Split(Environment.NewLine) |> Array.toList
    let intValues = lines |> List.map (fun x -> x |> Int32.Parse)
    
    let mutable count = 0;
    let mutable currentLine = lines[0] |> int;
    for line in intValues[1..] do
        if line > currentLine then count <- count + 1
        currentLine <- line
        
    count
    
type SlidingWindow(value : int) =
    let Value = value
    
    member _.value ()= Value
        
    static member fromLines(start:int, endIndex : int, lines : List<int>) : SlidingWindow =
        let length = lines.Length - 1
        if length >= endIndex then new SlidingWindow(List.sum lines[start..endIndex])
        else new SlidingWindow(List.sum lines[start..])
    
let windowIncreaseCalculator (contents: string)=
    let lines = contents.Split(Environment.NewLine) |> Array.toList
    let intLines = List.map (fun x -> x |> Int32.Parse) lines
    let thing = lines |> List.mapi (fun i _ -> SlidingWindow.fromLines(i, i+2, intLines))
        
    let mutable result = 0
    let mutable window = thing[0]
    
    for w in thing[1..] do
        let nextWindow = w;
        if nextWindow.value() > window.value() then result <- result + 1
        window <- nextWindow
        
    result
           
            
   