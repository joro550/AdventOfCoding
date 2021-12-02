module AdventOfCodeFsharp._2021.Day2_attempt2

open System
    
let getInstruction(input:string)  =
    let parts = input.Split(" ")
    let value = parts[1] |> Int32.Parse
    
    match parts[0] with
        | "forward" -> (value, 0)   
        | "down" -> (0, value)  
        | "up" -> (0, -value)
        | _ -> failwith "todo"
        
let executeInstructions (input :string) =
   input.Split(Environment.NewLine)
        |> Array.map (fun elem -> elem |> getInstruction)
        |> Array.reduce (fun (a,b) (c,d) -> (a+c, b+d)) 