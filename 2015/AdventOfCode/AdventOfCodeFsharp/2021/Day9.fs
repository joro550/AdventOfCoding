module AdventOfCodeFsharp._2021.Day9

open System

type Thing(value:int, x:int, y:int) =
    member _.value ()= value
    member _.x ()= x
    member _.y ()= y
    member _.risk () = value + 1
    
type Map(things: Thing[][])=
    
    member _.getYLength ()= things.Length - 1
    member _.getXLength ()= things[0].Length - 1
    
    member this.getNeighbourArray (x:int,y:int)=
        if x = 0 && y = 0 then [| (x+1,y); (x,y+1)  |] // top left
        else if x = this.getXLength() && y = 0 then [| (x-1, y); (x, y+1)  |] // top right
        else if y = 0 then [| (x+1, y); (x, y+1); (x-1, y)  |] //top row
        
        else if x = 0 && y = this.getYLength() then [| (x, y-1); (x+1, y)  |] // bottom left
        else if x = this.getXLength() && y = this.getYLength() then [| (x-1, y); (x, y-1)  |] // bottom right
        else if y = this.getYLength() then [| (x, y-1); (x+1, y); (x-1, y); |] //bottom row

        else if x = this.getXLength() then [| (x, y-1); (x-1, y); (x, y+1);  |] // right side
        else if x = 0 then [| (x, y-1); (x+1, y); (x, y+1) |] //left side
        else [| (x, y-1); (x+1, y); (x, y+1); (x-1, y) |]
        
    member this.getLowestPoint(x:int, y:int) : Thing[]=
        let currentTile = things[y][x]
        
        let neighbours = this.getNeighbourArray(x,y)
                            |> Array.map (fun (a,b) -> things[b][a])        
                            |> Array.filter (fun elem -> elem.value() < currentTile.value())
                            
        if neighbours.Length = 0 then [| currentTile |]
//        else if neighbours.Length = 1 then neighbours
        else
            let mutable tiles :List<Thing> = []
            for n in neighbours do
                n
                    |> (fun elem -> this.getLowestPoint(elem.x(), elem.y()))
                    |> Array.toList
                    |> List.append tiles
                    |> ignore
            
            tiles |> List.toArray
        
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