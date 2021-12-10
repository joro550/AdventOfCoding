module AdventOfCodeFsharp._2021.Day9

open System
open System.Collections.Generic

type Thing(value:int, x:int, y:int) =
    member _.value ()= value
    member _.x ()= x
    member _.y ()= y
    member _.risk () = value + 1
    
type Postion(x,y) =
    member _.x ()=x
    member _.y ()=y
    
type Map(things: Thing[][])=
    let mutable _cache = new Dictionary<Postion, Thing[]>()
    
    member _.getYLength ()= things.Length - 1
    member _.getXLength ()= things[0].Length - 1
    
    member this.getNeighbourArray (x:int,y:int)=
        let right = (x+1, y)
        let left = (x-1, y)
        let up = (x,y-1)
        let down = (x, y+1)
        
        if x = 0 && y = 0 then [| right; down |] // top left
        else if x = this.getXLength() && y = 0 then [| down; left;  |] // top right
        else if x = 0 && y = this.getYLength() then [| up; right; |] // bottom left
        else if x = this.getXLength() && y = this.getYLength() then [| up; left; |] // bottom right

        else if y = 0 then [| right; down; left;  |] //top row
        else if y = this.getYLength() then [| up; right; left; |] //bottom row

        else if x = this.getXLength() then [| up; down; left; |] // right side
        else if x = 0 then [| up; right; down |] //left side
        else [| up; right; down; left |]
        
    member this.getLowestPoint(x:int, y:int) : Thing[]=
        if _cache.ContainsKey(Postion(x, y)) then _cache[Postion(x, y)]
        else
            
        let currentTile = things[y][x]
        let neighbours = this.getNeighbourArray(x,y)
                            |> Array.map (fun (a,b) -> things[b][a])        
                            |> Array.filter (fun elem -> elem.value() < currentTile.value())
                            
        if neighbours.Length = 0 then
            _cache.Add((Postion(x, y)), [| currentTile |])
            [| currentTile |]
//        else if neighbours.Length = 1 then neighbours
        else
            let thing = neighbours
                        |> Array.map (fun elem -> this.getLowestPoint(elem.x(), elem.y()))
                        |> Array.reduce (fun acc elem -> elem |> Array.append acc)
                        |> Array.distinct
                        
            _cache.Add((Postion(x, y)), thing)
            thing
                
    member this.getLowestPoints() : Thing[]=
        things
            |> Array.mapi (fun y elem -> elem |> Array.mapi (fun x _ -> this.getLowestPoint(x,y)))
            |> Array.reduce (fun acc elem -> elem |> Array.append acc)
            |> Array.reduce (fun acc elem -> elem |> Array.append acc)
            |> Array.distinct

let parseSteamTubes(input:string) =
    let lines = input.Split(Environment.NewLine)
                 |> Array.mapi(fun y line ->
                     line.ToCharArray()
                        |> Array.map (fun elem -> elem.ToString() |> int)
                        |> Array.mapi (fun x value -> Thing(value, x, y)))
    
    Map(lines)