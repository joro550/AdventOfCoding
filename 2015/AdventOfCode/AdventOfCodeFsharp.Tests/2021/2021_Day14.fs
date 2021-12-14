module AdventOfCodeFsharp.Tests._2021._2021_Day14

open Xunit
open AdventOfCodeFsharp.library
open AdventOfCodeFsharp._2021.Day14
    
[<Fact>]
let ``Given formula then correct array is returned`` ()=
    let formula = "NNCB"
    let result = getFormula(formula) 
    Assert.Equal("NN", result[0])
    Assert.Equal("NC", result[1])
    Assert.Equal("CB", result[2])

[<Fact>]
let ``Example 1``()=
    let input = "NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C"
    Assert.Equal(1588, puzzle1(input, 10))
    
[<Fact>]
let ``Example 2``()=
    let input = "NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C"
    Assert.Equal(1588, puzzle2(input, 10))
    
[<Fact>]
let ``Puzzle 1``()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day14.txt")
        let result = puzzle1(fileContent, 10)
        Assert.Equal(3306, result)
    }
    
[<Fact>]
let ``Puzzle 2``()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day14.txt")
        let result = puzzle2(fileContent, 21)
        Assert.Equal(1588, result)
    }
