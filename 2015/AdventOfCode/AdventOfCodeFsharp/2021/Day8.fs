module AdventOfCodeFsharp._2021.Day8

open System
open System.Collections.Generic
open System.Linq

type Number (number:string, value:int)=
    static member Zero = 6
    static member One = 2
    static member Two = 5
    static member Three = 5
    static member Four = 4
    static member Five = 5
    static member Six = 6
    static member Seven = 3
    static member Eight = 7
    static member Nine = 6
    
    member _.number()= number
    member _.value() = value

let comp(num1: int, num2:int)=
    if num1 = num2 then 1 else 0
    
let simpleNumbers(elem:int)=
    [| comp(elem, Number.One)
       comp(elem, Number.Four)
       comp(elem, Number.Seven)
       comp(elem, Number.Eight)|]

let countSimpleNumbers (input:string) =
    input.Split("|")[1]
        |> (fun elem -> elem.Split(" "))
        |> Array.map (fun elem -> elem.Trim().Length)
        |> Array.map simpleNumbers
        |> Array.reduce (fun acc elem -> elem |> Array.map2 (+) acc)
let countFromInput(input:string)=
    input.Split(Environment.NewLine)
        |> Array.map countSimpleNumbers
        |> Array.reduce (fun acc elem -> elem |> Array.map2 (+) acc)
        |> Array.sum
        
type NumberMap (numbers: Number[]) =
    static member count(input:string, comp:string) =
        let compCharArray= input.ToCharArray()
        comp.ToCharArray()
                |> Array.map (fun elem -> if  compCharArray |> Array.contains elem  then 1 else 0)
                |> Array.sum
                
    static member firstElementToString(input: char[]) : string= 
        input |> (fun elem -> elem[0]) |> string
                
    static member buildMap(input:string)=
        let values = input.Split(" ")
        let one = values |> Array.filter (fun elem -> elem.Length = Number.One) |> (fun elem -> elem[0])
        let four = values |> Array.filter (fun elem -> elem.Length = Number.Four) |> (fun elem -> elem[0])
        let seven = values |> Array.filter (fun elem -> elem.Length = Number.Seven) |> (fun elem -> elem[0])
        let eight = values |> Array.filter (fun elem -> elem.Length = Number.Eight) |> (fun elem -> elem[0])
        let sixNineAndZero = values |> Array.filter (fun elem -> elem.Length = Number.Zero)
        let twoThreeOrFive =  values |> Array.filter (fun elem -> elem.Length = Number.Two)
        
        let two = twoThreeOrFive
                    |> Array.filter (fun elem -> NumberMap.count(elem, one) = 1 && NumberMap.count(elem, four) = 2)
                    |> (fun elem -> elem[0])
                    
        let three = twoThreeOrFive
                    |> Array.filter (fun elem -> NumberMap.count(elem, one) = 2)
                    |> (fun elem -> elem[0])   
        
        let five = twoThreeOrFive
                    |> Array.filter (fun elem -> NumberMap.count(elem, one) = 1 && NumberMap.count(elem, four) = 3)
                    |> (fun elem -> elem[0])
        
        let six = sixNineAndZero
                    |> Array.filter (fun elem -> NumberMap.count(elem, seven) = 2)
                    |> (fun elem -> elem[0])
                    
        let nine = sixNineAndZero
                    |> Array.filter (fun elem -> NumberMap.count(elem, four) = 4 && NumberMap.count(elem, three) = 5)
                    |> (fun elem -> elem[0])
                    
        let zero = sixNineAndZero
                    |> Array.filter (fun elem -> elem <> six && elem <> nine)
                    |> (fun elem -> elem[0])
                    
        NumberMap([| Number(zero, 0);
                     Number(one, 1);
                     Number(two, 2);
                     Number(three, 3);
                     Number(four, 4);
                     Number(five, 5);
                     Number(six, 6);
                     Number(seven, 7);
                     Number(eight, 8);
                     Number(nine, 9); |])
        
        member _.toNumber(input:string) : int =
            let potentialNumbers = numbers |> Array.filter(fun elem -> elem.number().Length = input.Length)
            if potentialNumbers.Length = 1 then
                potentialNumbers[0].value()
            else
                let filtered = potentialNumbers |> Array.filter(fun elem -> NumberMap.count(input, elem.number()) = input.Length)
                filtered[0].value()
            
                
let count(input:string) =
    let thing =input.Split("|")
                |> (fun elem -> (NumberMap.buildMap(elem[0])), elem[1].Trim().Split(" "))
                |> (fun (map, elem) -> elem |> Array.map map.toNumber)
                |> Array.map string
                |> String.concat ""
                |> int64
    thing
    
let countFromInput2(input:string) =
    let value = input.Split(Environment.NewLine)
                |> Array.map count
    value |> Array.sum
            