module AdventOfCodeFsharp._2021.Day1

open System

let increaseCalculator (contents: string)=
    let lines = contents.Split(Environment.NewLine)
                |> Seq.map (fun x -> x |> Int32.Parse)
                |> Seq.pairwise
                |> Seq.filter (fun (x,y) -> y > x)
                |> Seq.toArray
    lines.Length
    
type SlidingWindow(value : int) =
    let Value = value
    
    member _.value ()= Value
        
    static member fromLines(start:int, endIndex : int, lines : List<int>) : SlidingWindow =
        let length = lines.Length - 1
        let endRange =
            if length >= endIndex then endIndex else length
        SlidingWindow(List.sum lines[start..endRange])
           
           
let windowIncreaseCalculator (contents: string)=
    let lines = contents.Split(Environment.NewLine)
                |> Seq.map (fun x -> x |> Int32.Parse)
                |> Seq.toList
                
    let windows = 
        lines |> List.mapi (fun i _ -> SlidingWindow.fromLines(i, i+2, lines))
        |> Seq.pairwise
        |> Seq.filter (fun (x,y) -> y.value() > x.value())
        |> Seq.toArray
        
    windows.Length
            
   