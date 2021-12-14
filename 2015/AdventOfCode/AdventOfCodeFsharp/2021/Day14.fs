module AdventOfCodeFsharp._2021.Day14

open System
open System.Collections
open System.Collections.Generic
open System.Text
       
let getKeyValue(input:string)=
     input.Split("->")
        |> Array.map (fun e -> e.Trim())
        |> (fun e -> (e[0], e[0].Insert(1, e[1])))
    
let applyMapping(input:string, mappings:IDictionary<string, string>)=
    mappings[input]

let  getFormula(input:string) =
    input.ToCharArray()
        |> Array.map string
        |> Array.pairwise
        |> Array.map (fun (a,b) -> a+b)
        
let createStack(formula:string[], mappings:IDictionary<string, string>)=
    let stack = Queue<string>()
    formula
        |> Array.map (fun elem -> applyMapping(elem, mappings))
        |> Array.iteri (fun acc elem -> if acc = 0
                                        then stack.Enqueue(elem[0..elem.Length-2])
                                        else stack.Enqueue(elem[1..]))
    stack
    
let getSequence2(formula:Queue<string>, mappings:IDictionary<string, string>)=
    let stack = Queue<string>()
    formula.ToArray()
        |> Array.pairwise
        |> Array.map (fun (a, b) -> applyMapping(a + b, mappings))
        |> Array.iteri (fun acc elem -> if acc = 0
                                        then stack.Enqueue(elem[0..elem.Length-2])
                                        else stack.Enqueue(elem[1..]))
    stack
        
let getSequence(formula:string[], mappings:IDictionary<string, string>)=
    let thing = formula
                |> Array.map (fun elem -> applyMapping(elem, mappings))
                
    let firstCharacter = formula[0][0] |> string
    let toreturn=    (StringBuilder(firstCharacter), thing)
                        ||> Array.fold(fun acc elem -> acc.Append(elem[1..]))
                        |> fun elem -> elem.ToString()
    toreturn
        
let getSum(sequence:string) =
    sequence.ToCharArray()
        |> Array.groupBy id
        |> Array.map(fun (_, cs) -> Array.length cs)
        |> (fun elem -> (elem |> Array.max) - (elem |> Array.min))

let puzzle1(input:string, sequenceAmount:int)=
    let formula, mappings = input.Split(Environment.NewLine + Environment.NewLine)
                                |> (fun elem -> (getFormula(elem[0]), elem[1].Split(Environment.NewLine)
                                                                        |> Array.map getKeyValue
                                                                        |> dict))
    let mutable sequence = getSequence(formula, mappings)
    for i = 1 to sequenceAmount - 1 do
        sequence <- getSequence(getFormula(sequence), mappings)
    
    getSum(sequence)
    
    
let puzzle2(input:string, sequenceAmount:int)=
    let formula, mappings = input.Split(Environment.NewLine + Environment.NewLine)
                                |> (fun elem -> (getFormula(elem[0]), elem[1].Split(Environment.NewLine)
                                                                        |> Array.map getKeyValue
                                                                        |> dict))
    
    let mutable sequence = createStack(formula, mappings)
    for i = 1 to sequenceAmount - 1 do
        sequence <- getSequence2(sequence, mappings)
    
    let arr = sequence.ToArray()
                |> fun e -> (StringBuilder(), e) ||> Array.fold (fun acc -> acc.Append)
    
    getSum(arr.ToString())
