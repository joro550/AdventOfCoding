module AdventOfCodeFsharp.Tests._2021_Day2_attempt2

open AdventOfCodeFsharp._2021.Day2_attempt2
open AdventOfCodeFsharp.library
open Xunit

[<Fact>]
let ``Puzzle1`` () =
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day2.txt") |> Async.AwaitTask
        let horizontal, depth = executeInstructions(fileContent)
        Assert.Equal(1936494, horizontal * depth)
    }
