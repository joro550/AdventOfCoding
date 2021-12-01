module AdventOfCodeFsharp._2021.Day1

open System

let increaseCalculator (contents: string)=
    contents.Split(Environment.NewLine)
        |> Seq.map (fun x -> x |> Int32.Parse)
        |> Seq.pairwise
        |> Seq.filter (fun (x,y) -> y > x)
        |> Seq.length
            
let getSlidingSum (lines : seq<int>) : seq<int> =
    let linesList = lines |> Seq.toList
    lines |> Seq.mapi (fun i _ -> linesList[i..i+2] |> List.sum)      
           
let windowIncreaseCalculator (contents: string)=
    contents.Split(Environment.NewLine)
        |> Seq.map (fun x -> x |> Int32.Parse)
        |> getSlidingSum
        |> Seq.pairwise
        |> Seq.filter (fun (x,y) -> y > x)
        |> Seq.length

            
   