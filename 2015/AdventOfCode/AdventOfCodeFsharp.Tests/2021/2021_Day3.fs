module AdventOfCodeFsharp.Tests._2021_Day3

open AdventOfCodeFsharp._2021.Day3
open Xunit
open AdventOfCodeFsharp.library


[<Fact>]
let ``Puzzle1`` () =
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day3.txt") |> Async.AwaitTask
        let gamma = getGammaValue(fileContent)
        let epsilon = getEpsilonRate(fileContent)
        
        Assert.Equal(1025636, gamma * epsilon)
    }
    
[<Fact>]
let ``Puzzle2`` () =
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day3.txt") |> Async.AwaitTask
        let oxygen = getOxygenRating(fileContent)
        let co2 = getco2Rating(fileContent)
        
        Assert.Equal(793873, oxygen * co2)
    }
    
[<Fact>]
let ``Given example then co2 is returned`` ()=
    let value = "00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010"
    let oxygenRating = getco2Rating(value)
    Assert.Equal(10, oxygenRating);
    
[<Fact>]
let ``Given example then oxygen rating is returned`` ()=
    let value = "00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010"
    let oxygenRating = getOxygenRating(value)
    Assert.Equal(23, oxygenRating);


let ``Given an string then array of ints is returned`` ()=
    let value = "00100"
    let arrayValue = stringToIntArray(value)
    Assert.Equal(arrayValue[0], 0)
    Assert.Equal(arrayValue[1], 0)
    Assert.Equal(arrayValue[2], 1)
    Assert.Equal(arrayValue[3], 0)
    Assert.Equal(arrayValue[4], 0)
     
[<Fact>]
let ``Given example then string is returned`` ()=
    let value = "00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010"
    let answer = getGammaValue(value)
    Assert.Equal(22, answer)
    
    
[<Fact>]
let ``Given example then epsilon is returned`` ()=
    let value = "00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010"
    let answer = getEpsilonRate(value)
    Assert.Equal(9, answer)