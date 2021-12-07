module AdventOfCodeFsharp._2021.Day7

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
    
    let mutable fuel : List<int> = []
    for sub in submarines do
        fuel <- [getFuelCost(submarines, sub)] |> List.append fuel
    
    fuel |> List.reduce (fun acc elem -> if acc < elem then acc else elem)
    
    
let getArraySum(input: int) =
    [| for i in 1 .. input -> i |]
        |> Array.sum

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
        