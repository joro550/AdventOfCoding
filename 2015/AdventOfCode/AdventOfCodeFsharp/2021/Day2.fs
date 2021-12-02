module AdventOfCodeFsharp._2021.Day2

open System

type Ship(horizontal:int, depth:int) =
    member _.horizontal ()= horizontal
    member _.depth ()= depth
    
type Instruction(amount:int) =
    abstract member execute : Ship -> Ship
    
    member _.amount ()= amount
    
    default _.execute(ship: Ship) : Ship =
        ship
        
type ForwardInstruction(amount:int) =
    inherit Instruction(amount)
    
    override _.execute(ship: Ship) : Ship =
        Ship(ship.horizontal() + amount, ship.depth())
        
type UpInstruction(amount:int) =
    inherit Instruction(amount)
    
    override _.execute(ship: Ship) : Ship =
        Ship(ship.horizontal(), ship.depth() - amount)
        
type DownInstruction(amount:int) =
    inherit Instruction(amount)

    override _.execute(ship: Ship) : Ship =
        Ship(ship.horizontal(), ship.depth() + amount)
    
let getInstruction(input:string) : Instruction =
    let parts = input.Split(" ")
    let value = parts[1] |> Int32.Parse
    
    match parts[0] with
        | "forward" -> ForwardInstruction(value)   
        | "down" -> DownInstruction(value)   
        | "up" -> UpInstruction(value)
        | _ -> failwith "todo"
        
let getInstructions (input:string) : List<Instruction> =
    input.Split(Environment.NewLine)
        |> Array.map (fun elem -> elem |> getInstruction)
        |> Array.toList
        
let executeInstructions (input :string) =
    let mutable ship = Ship(0,0)
    input
        |> getInstructions
        |> List.map (fun elem -> ship <- ship |> elem.execute)
        |> ignore
    ship