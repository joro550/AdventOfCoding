module AdventOfCodeFsharp.Tests._2021._2021_Day13

open Xunit
open AdventOfCodeFsharp._2021.Day13

[<Fact>]
let ``Given points then they are parsed correctly``()=
    let input = "123,124"
    let points = PointMap.fromString(input).points()
    
    Assert.Equal(123, points[0].x())
    Assert.Equal(124, points[0].y())
    
[<Fact>]
let ``Example``()=
    let input = "6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7"
    let map = PointMap.fromString(input)
    let map = map.foldVertically(7)
    Assert.NotEmpty(map.getPoint(0,0))
    Assert.NotEmpty(map.getPoint(2,0))
    Assert.NotEmpty(map.getPoint(3,0))
    