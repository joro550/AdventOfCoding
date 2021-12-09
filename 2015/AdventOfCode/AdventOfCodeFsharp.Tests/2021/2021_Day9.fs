module AdventOfCodeFsharp.Tests._2021._2021_Day9

open AdventOfCodeFsharp._2021.Day9
open AdventOfCodeFsharp.library
open Xunit

[<Fact>]
let ``Given grid then neighbours can be retrieved`` ()=
    let grid = "2199943210
3987894921
9856789892
8767896789
9899965678"

    let result = parseSteamTubes(grid)
    let lowestPoints = result.getLowestPoints()
                        |> Array.map (fun elem -> elem.risk())
                        |> Array.sum
    Assert.Equal(15, lowestPoints);


[<Fact>]
let ``Puzzle1``()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day9.txt") |> Async.AwaitTask
        
        let result = parseSteamTubes(fileContent)
        let lowestPoints = result.getLowestPoints()
                        |> Array.map (fun elem -> elem.risk())
                        |> Array.sum
        Assert.Equal(1581, lowestPoints)
    }