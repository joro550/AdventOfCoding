module AdventOfCodeFsharp.Tests._2021_Day6
//
//open Xunit
//open AdventOfCodeFsharp._2021.Day6
//open AdventOfCodeFsharp.library
//
//[<Fact>]
//let ``After a day timer is decreased`` ()=
//    let fish = LanternFish()
//    let newFish = fish.addDay()
//    Assert.Equal(5, newFish[0].timer())
//    
//[<Fact>]
//let ``When timer reaches zero new fish is added`` ()=
//    let fish = LanternFish(0)
//    let newFish = fish.addDay()
//    Assert.Equal(8, newFish[0].timer())
//    Assert.Equal(6, newFish[1].timer())
//    
//[<Fact>]
//let ``Given input then lantern fish get created`` ()=
//    let input = "3,4,3,1,2"
//    let fish = createFish(input)
//    Assert.Equal(5, fish.Length)
//    Assert.Equal(3, fish[0].timer())
//    Assert.Equal(4, fish[1].timer())
//    Assert.Equal(3, fish[2].timer())
//    Assert.Equal(1, fish[3].timer())
//    Assert.Equal(2, fish[4].timer())
//
//[<Fact>]
//let ``Example1`` ()=
//    let input = "3,4,3,1,2"
//    let result = simulate(input, 18)
//    Assert.Equal(26, result.Length)
//    
//
//[<Fact>]
//let ``Puzzle1`` () =
//    async {
//        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day6.txt") |> Async.AwaitTask
//        let result = simulate(fileContent, 80)
//        
//        Assert.Equal(1025636, result.Length)
//    }
//    
//    