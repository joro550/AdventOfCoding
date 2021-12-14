module AdventOfCodeFsharp._2021.Day14

open System
open System.Collections.Generic
       
let getKeyValue(input:string)=
    let mapping = input.Split("->") |> Array.map (fun e -> e.Trim())
    (mapping[0], mapping[1])
    
let applyMapping(input:string, mappings:IDictionary<string, string>)=
    input[0].ToString() + mappings[input] + input[1].ToString()

let  getFormula(input:string) =
    input.ToCharArray()
        |> Array.map string
        |> Array.pairwise
        |> Array.map (fun (a,b) -> a+b)
        
let getSequence(formula:string[], mappings:IDictionary<string, string>)=
    formula
        |> Array.reduce(fun acc elem -> if acc.Length = 2 then applyMapping(acc, mappings) + applyMapping(elem, mappings)[1..]
                                        else acc + applyMapping(elem, mappings)[1..])
        
let puzzle1(input:string, sequenceAmount:int)=
    let formula, mappings = input.Split(Environment.NewLine + Environment.NewLine)
                                |> (fun elem -> (getFormula(elem[0]),
                                                 elem[1].Split(Environment.NewLine)
                                                    |> Array.map getKeyValue
                                                    |> dict))
                                
    let mutable sequence = getSequence(formula, mappings)
    for i = 1 to sequenceAmount - 1 do
        sequence <- getSequence(getFormula(sequence), mappings)
    
    sequence.ToCharArray()
        |> Array.groupBy id
        |> Array.map(fun (_, cs) -> Array.length cs)
        |> (fun elem -> (elem |> Array.max) - (elem |> Array.min))
