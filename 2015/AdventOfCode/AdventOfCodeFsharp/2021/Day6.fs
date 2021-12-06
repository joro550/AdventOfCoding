module AdventOfCodeFsharp._2021.Day6

open System
//
//type LanternFish(timer : int ) =
//    interface IComparable with
//        member this.CompareTo(obj : Object) : int =
//            let thing = LanternFish obj
//            if thing = this then 1 else 0
//        
//    new ()= LanternFish(6)
//    
//    member _.timer () = timer
//    
//    member _.addDay() : LanternFish[] =
//        let newTime = timer - 1
//        
//        if newTime < 0 then [| LanternFish(8); LanternFish() |]
//        else [| LanternFish(newTime) |]
//        
//    member this.CompareTo(obj) = this = obj
//        
//    member _.addDayList() : List<LanternFish> =
//        let newTime = timer - 1
//        
//        if newTime < 0 then [ LanternFish(8); LanternFish() ]
//        else [ LanternFish(newTime) ]
//        
//let createFish(input:string) : LanternFish[] =
//    input.Split(",")
//        |> Array.map int
//        |> Array.map LanternFish
//        
//let simulate(input:string, dayCount: int) : LanternFish[] =
//    let fish = createFish(input)
//    let mutable fishArray : Set<LanternFish> = fish |> Set.ofSeq
//    
//    for i = 1 to dayCount do
//        let mutable thing : Set<LanternFish> = Set.ofSeq []
//        
//        for f in fishArray do
//            for newFish in f.addDayList() do
//                thing <- thing.Add(newFish)
//            
//        fishArray <- thing
//            
//    fishArray |> Set.toArray