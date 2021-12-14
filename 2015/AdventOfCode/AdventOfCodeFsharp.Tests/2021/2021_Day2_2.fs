module AdventOfCodeFsharp.Tests._2021_Day2_2

open Xunit
open AdventOfCodeFsharp._2021.Day2_2
open AdventOfCodeFsharp.library

[<Fact>]
let ``Puzzle1`` () =
    async {
        let! fileContent = loadFileAsync("AdventOfCodeFsharp._2021.Day2.txt")
        let ship = executeInstructions(fileContent)
        Assert.Equal(1997106066, ship.horizontal() * ship.depth())
    }

[<Fact>]
let ``Execute instructions`` () =
    let instructionString = "forward 5
down 5
forward 8
up 3
down 8
forward 2"
    let ship = executeInstructions(instructionString)
    Assert.Equal(900, ship.horizontal() * ship.depth())

[<Fact>]
let ``Given multiple instructions then instruction are in order`` ()=
    let instructionString = "forward 5
down 5
up 5"
    let instructions = getInstructions(instructionString)
    Assert.Equal(3, instructions.Length)

[<Fact>]
let ``When forward instruction is given then ForwardInstruction is returned`` ()=
    let instructionString = "forward 5"
    let instruction = getInstruction(instructionString)
    let forwardInstruction = Assert.IsType<ForwardInstruction>(instruction)
    Assert.Equal(5, forwardInstruction.amount())
    
[<Fact>]
let ``When down instruction is given then DownInstruction is returned`` ()=
    let instructionString = "down 5"
    let instruction = getInstruction(instructionString)
    let downInstruction = Assert.IsType<DownInstruction>(instruction)
    Assert.Equal(5, downInstruction.amount())
    
[<Fact>]
let ``When up instruction is given then UpInstruction is returned`` ()=
    let instructionString = "up 5"
    let instruction = getInstruction(instructionString)
    let upInstruction = Assert.IsType<UpInstruction>(instruction)
    Assert.Equal(5, upInstruction.amount())
    
[<Fact>]
let ``Given a forward instruction then amount gets adds to horizontal and increases depth`` ()=
    let ship = Ship(0,0,2)
    let instruction = ForwardInstruction(5)
    let newShip = instruction.execute(ship)
    Assert.Equal(5, newShip.horizontal())
    Assert.Equal(10, newShip.depth())
    
[<Fact>]
let ``Given a down instruction then amount gets adds to aim`` ()=
    let ship = Ship(0,0,0)
    let instruction = DownInstruction(5)
    let newShip = instruction.execute(ship)
    Assert.Equal(5, newShip.aim())
    
[<Fact>]
let ``Given a up instruction then amount gets subtracted from aim`` ()=
    let ship = Ship(0,0,0)
    let instruction = DownInstruction(5)
    let newShip = instruction.execute(ship)

    let instruction = UpInstruction(3)
    let newShip = instruction.execute(newShip)

    Assert.Equal(2, newShip.aim())