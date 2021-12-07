module AdventOfCodeFsharp._2021.Day7

open System.Collections.Generic

type Cache ()=
    static let dictionary = new Dictionary<int, int64>()
    
    static member exists(key:int) =
        dictionary.ContainsKey(key)
        
    static member add(key:int,value:int64) =
        dictionary.Add(key, value)
        value
        
    static member get(input:int)=
        dictionary[input]

let getSubmarines (input:string) =
    input.Split(",")
        |> Array.map int
        
let getFuelCost( input: int[], position :int) =
     input
            |> Array.map (fun elem ->
                if elem > position then elem - position else position - elem)
            |> Array.sum

let getLeastAmountOfFuel (input:string) =
    let submarines = input |> getSubmarines
    let maxPosition = submarines |> Array.max
    let minPosition = submarines |> Array.min
    
    [for i = minPosition to maxPosition do getFuelCost(submarines, i)]
        |> List.reduce (fun acc elem -> if acc < elem then acc else elem)
    
let getArraySum(input: int) =
    if Cache.exists(input) = false then
        let sum = [| for i in 1 .. input -> i |]
                        |> Array.sum
        Cache.add(input, sum)
    else
    Cache.get(input)

let getFuelCost2(input: int[], position :int) =
     input
            |> Array.map (fun elem ->
                if elem > position then elem - position else position - elem)
            |> Array.map (fun elem -> elem |> getArraySum)
            |> Array.sum
    
let getLeastAmountOfFuel2 (input:string) =
    let submarines = input |> getSubmarines
    let maxPosition = submarines |> Array.max
    let minPosition = submarines |> Array.min
    
    [for i = minPosition to maxPosition do getFuelCost2(submarines, i)]
        |> List.reduce (fun acc elem -> if acc < elem then acc else elem)
        