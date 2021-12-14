module Tests

open AdventOfCodeFsharp._2021.Day1
open AdventOfCodeFsharp.library
open Xunit

[<Fact>]
let ``Puzzle 1`` () =
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day1.txt")
        let result = increaseCalculator(fileContent)
        Assert.Equal(1288, result)
    }
    
[<Fact>]
let ``Puzzle 2`` () =
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day1.txt")
        let result = windowIncreaseCalculator(fileContent)
        Assert.Equal(1311, result)
    }
    
[<Fact>]
let ``When windows increase then result increases`` ()=
    let values = "199
200
208
210
200
207
240
269
260
263"
    let result = windowIncreaseCalculator(values)
    Assert.Equal(5, result)
    
[<Fact>]
let ``When number has increased then result is number of times its increased`` ()=
    let values = "100
200"
    let result = increaseCalculator(values);
    Assert.Equal(1, result)
    
[<Fact>]
let ``When number has increased multiple times then result is number of times its increased`` ()=
    let values = "100
200
300
400
200
300"
    let result = increaseCalculator(values);
    Assert.Equal(4, result)
        
[<Fact>]
let ``When number has decreased then result does not count the decrease`` ()=
    let values = "100
99"
    let result = increaseCalculator(values);
    Assert.Equal(0, result)