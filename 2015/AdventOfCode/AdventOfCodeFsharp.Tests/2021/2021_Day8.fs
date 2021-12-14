module AdventOfCodeFsharp.Tests._2021._2021_Day8

open Xunit
open AdventOfCodeFsharp._2021.Day8
open AdventOfCodeFsharp.library

[<Fact>]
let ``Thing`` ()=
    let input = "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |
fdgacbe cefdb cefbgd gcbe"
    let thing = countSimpleNumbers(input)
    Assert.Equal(1, thing[1])
    Assert.Equal(1, thing[3])

[<Fact>]
let ``example2`` ()=
    let input = "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
    let thing = countFromInput2(input)
    Assert.Equal(61229L, thing)
    
[<Fact>]
let ``Puzzle1`` ()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day8.txt")
        let count = countFromInput(fileContent)
        Assert.Equal(272, count)
    }
    
[<Fact>]
let ``Puzzle2`` ()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day8.txt")
        let count = countFromInput2(fileContent)
        Assert.Equal(1007675L, count)
    }
    