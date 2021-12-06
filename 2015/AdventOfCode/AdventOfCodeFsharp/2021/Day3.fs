module AdventOfCodeFsharp._2021.Day3

open System

let stringToIntArray (input: string) =
    input.ToCharArray()
        |> Array.map (fun elem -> elem.ToString() |> Int32.Parse)
                
let getGammaValue (input:string)=
    let length = input.Split(Environment.NewLine).Length /2
    let value = input.Split(Environment.NewLine)
                    |> Array.map (fun elem -> elem |> stringToIntArray)
                    |> Array.reduce (fun acc elem -> elem |> Array.map2 (+) acc)
                    |> Array.map (fun elem -> if elem > length then "1" else "0")
                    |> String.concat ""
        
    Convert.ToInt32(value, 2)
    
    
let getEpsilonRate (input:string)=
    let length = input.Split(Environment.NewLine).Length /2
    let value = input.Split(Environment.NewLine)
                    |> Array.map (fun elem -> elem |> stringToIntArray)
                    |> Array.reduce (fun acc elem -> elem |> Array.map2 (+) acc)
                    |> Array.map (fun elem -> if elem > length then "0" else "1")
                    |> String.concat ""
        
    Convert.ToInt32(value, 2)
    
let getMostPopularNumber (input:string[])=
    let length = if input.Length % 2 > 0 then input.Length/2 + 1 else input.Length /2
    input
        |> Array.map (fun elem -> elem |> stringToIntArray)
        |> Array.reduce (fun acc elem -> elem |> Array.map2 (+) acc)
        |> Array.map (fun elem -> if elem >= length then "1" else "0")
        
let getLeastPopularNumber (input:string[])=
    let length = if input.Length % 2 > 0 then input.Length/2 + 1 else input.Length /2
    let thing = input
                    |> Array.map (fun elem -> elem |> stringToIntArray)
                    |> Array.reduce (fun acc elem -> elem |> Array.map2 (+) acc)
    Array.map (fun elem -> if elem >= length then "0" else "1") thing

let getOxygenRating (input:string)=
    let mutable i =0
    let mutable values = input.Split(Environment.NewLine)
        
    while values.Length > 1 do
        let mostPopular = getMostPopularNumber(values)
        values <- values |> Array.filter (fun elem -> elem[i].ToString() = mostPopular[i])
        i <- i + 1

    Convert.ToInt32(values[0],2)
    
let getco2Rating (input:string)=
    let mutable i =0
    let mutable values = input.Split(Environment.NewLine)

    while values.Length > 1 do
        let mostPopular = getLeastPopularNumber(values)
        values <- values |> Array.filter (fun elem -> elem[i].ToString() = mostPopular[i])
        i <- i + 1

    Convert.ToInt32(values[0],2)