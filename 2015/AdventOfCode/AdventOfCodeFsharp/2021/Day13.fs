module AdventOfCodeFsharp._2021.Day13

open System

type Point(x:int, y:int)=
    member _.x()=x
    member _.y()=y
    
    static member fromString(input :string[])=
        input
            |> Array.map (fun e -> e.Split(",") |> (fun p -> Point(p[0] |> int, p[1] |> int)))
        
        
        
        
type PointMap(points: Point[])=
    member _.points()= points
    member _.getMaxX()= points |> Array.maxBy (fun e -> e.x()) |> (fun e -> e.x())
    member _.getMaxY()= points |> Array.maxBy (fun e -> e.y()) |> (fun e -> e.y())
    
    static member fromString(input:string)=
        let points = input.Split(Environment.NewLine + Environment.NewLine)
                    |> (fun e-> e[0].Split(Environment.NewLine) |> Point.fromString)
        PointMap(points)
        
    member _.getDistinctPointCount()= points |> Array.distinct |> (fun elem -> elem.Length)
    
    member _.getPoint(x:int, y:int)=
        points |> Array.filter (fun elem -> elem.x() = x && elem.y() = y)
        
    member this.foldVertically(point:int)=
        let toFoldTo = point - 1
        
        let newPoints = points
                            |> Array.filter (fun e -> e.y() > point)
                            |> Array.map(fun e -> Point(e.x(), e.y() - (e.y() - toFoldTo)))
                            
        let remainingPoints = points |> Array.filter (fun e -> e.y() < point)
        PointMap(newPoints |> Array.append remainingPoints)
        
