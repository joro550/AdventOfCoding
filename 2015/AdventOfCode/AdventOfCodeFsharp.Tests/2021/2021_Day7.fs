module AdventOfCodeFsharp.Tests._2021.Day7

open Xunit
open AdventOfCodeFsharp._2021.Day7
open AdventOfCodeFsharp.library

[<Fact>]
let ``Given input then submarines are returned`` ()=
    let input = "16,1,2,0,4,2,7,1,2,14"
    let submarines = getSubmarines(input)
    Assert.Equal(16, submarines[0])
    Assert.Equal(1, submarines[1])
    Assert.Equal(2, submarines[2])
    Assert.Equal(0, submarines[3])
    Assert.Equal(4, submarines[4])
    
    
[<Fact>]
let ``Given position 2 then fuel expended is 37`` ()=
    let input = "16,1,2,0,4,2,7,1,2,14"
    let fuelCost = getSubmarines(input)
                        |> (fun elem -> getFuelCost(elem, 2))
    Assert.Equal(37, fuelCost)
    
    
[<Fact>]
let ``Get Fuel 2 Given position 5 then fuel expended is 168`` ()=
    let input = "16,1,2,0,4,2,7,1,2,14"
    let fuelCost = getSubmarines(input)
                        |> (fun elem -> getFuelCost2(elem, 5))
    Assert.Equal(168L, fuelCost)
    
[<Fact>]
let ``Example 1`` ()=
    let input = "16,1,2,0,4,2,7,1,2,14"
    let leastAmountOfFuel = getLeastAmountOfFuel(input)
    Assert.Equal(37, leastAmountOfFuel)
    
    
[<Fact>]
let ``Example 2`` ()=
    let input = "16,1,2,0,4,2,7,1,2,14"
    let leastAmountOfFuel = getLeastAmountOfFuel2(input)
    Assert.Equal(168L, leastAmountOfFuel)
    
[<Fact>]
let ``Puzzle 1`` ()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day7.txt") |> Async.AwaitTask
        let leastAmountOfFuel = getLeastAmountOfFuel(fileContent)
        Assert.Equal(340056, leastAmountOfFuel)
    }
    
[<Fact>]
let ``Puzzle 2`` ()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day7.txt") |> Async.AwaitTask
        let leastAmountOfFuel = getLeastAmountOfFuel2(fileContent)
        Assert.Equal(96592275L, leastAmountOfFuel)
    }
    