module AdventOfCodeFsharp.Tests._2021._2021_Day10

open Xunit
open AdventOfCodeFsharp._2021.Day10;
open AdventOfCodeFsharp.library;


[<Theory>]
[<InlineData("[({(<(())[]>[[{[]{<()<>>")>]
[<InlineData("[(()[<>])]({[<{<<[]>>(")>]
[<InlineData("(((({<>}<{<{<>}{[]{[]{}")>]
let ``Given incomplete line then zero is returned`` (input: string)=
    let result = handleLine(input)
    Assert.Equal(0L, result)
    
[<Fact>]
let ``Given invalid line then correct score is given`` ()=
    let line = "{([(<{}[<>[]}>{[]{[(<()>"
    let result = handleLine(line)
    Assert.Equal(1197L, result)
    
[<Fact>]
let ``Example1`` ()=
    let line = "[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]"
    let result = handleLines(line)
    Assert.Equal(26397L, result)
    
[<Fact>]
let ``Puzzle1`` ()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day10.txt") |> Async.AwaitTask
        let count = handleLines(fileContent)
        Assert.Equal(369105L, count)
    }
    
    
[<Fact>]
let ``Puzzle2`` ()=
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day10.txt") |> Async.AwaitTask
        let count = handleLinesPuzzle2(fileContent)
        Assert.Equal(3999363569L, count)
    }