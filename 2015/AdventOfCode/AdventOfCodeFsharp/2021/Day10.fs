module AdventOfCodeFsharp._2021.Day10

open System
open System.Collections.Generic

type Token =
   | None = -1
   | BraceLeft = 0
   | BraceRight = 1
   | BracketLeft = 2
   | BracketRight = 3
   | SquareBracketLeft = 4
   | SquareBracketRight = 5
   | LessThan = 6
   | MoreThan = 7
   
let charToToken(input:char)=
    match input with
        | '{' -> Token.BraceLeft
        | '}' -> Token.BraceRight
        | '(' -> Token.BracketLeft
        | ')' -> Token.BracketRight
        | '[' -> Token.SquareBracketLeft
        | ']' -> Token.SquareBracketRight
        | '<' -> Token.LessThan
        | '>' -> Token.MoreThan
        | _ -> failwith "todo"
        
let isOpeningChunk(token:Token) =
    match token with
        | Token.BraceLeft -> true
        | Token.BracketLeft -> true
        | Token.SquareBracketLeft -> true
        | Token.LessThan -> true
        | _ -> false
        
let isClosingChunk(token:Token)=
    match token with
        | Token.BraceRight -> true
        | Token.BracketRight -> true
        | Token.SquareBracketRight -> true
        | Token.MoreThan -> true
        | _ -> false
        
let getClosingTokenValue(token:Token)=
    match token with
        | Token.BraceLeft -> 3L
        | Token.BracketLeft -> 1L 
        | Token.SquareBracketLeft -> 2L 
        | Token.LessThan  -> 4L
        | _ -> failwith "invalid token"
    
let isClosingChunkForCurrentChunk(currentChunk:Token, closingChunk:Token)=
    match currentChunk with
        | Token.BraceLeft when closingChunk = Token.BraceRight -> true
        | Token.BracketLeft when closingChunk = Token.BracketRight -> true
        | Token.SquareBracketLeft when closingChunk = Token.SquareBracketRight -> true
        | Token.LessThan when closingChunk = Token.MoreThan -> true
        | _ -> false
    
let getTokenScore(token:Token) :int64=
    match token with
        | Token.BracketRight -> 3L
        | Token.SquareBracketRight -> 57L
        | Token.BraceRight -> 1197L
        | Token.MoreThan -> 25137L
        | _ -> failwith "token not known"
     
type TokenType (tokenQueue :Token[])=
   member this.getInvalidTokenScore() =
       let mutable result = 0L
       let mutable index = 0
       
       let stack : Stack<Token> = Stack<Token>()
       
       while result = 0 && index < tokenQueue.Length do
            if isOpeningChunk(tokenQueue[index]) then stack.Push(tokenQueue[index])
            else if isClosingChunk(tokenQueue[index]) then 
                let lastToken = stack.Pop()
                
                if isClosingChunkForCurrentChunk(lastToken, tokenQueue[index]) = false then
                    result <- getTokenScore(tokenQueue[index])
            index <- index + 1
            
       result
       
   member _.getMissingTokenScore()=
        let stack : Stack<Token> = Stack<Token>()
        
        for token in tokenQueue do
            if isOpeningChunk(token) then stack.Push(token)
            else if isClosingChunk(token) then stack.Pop() |> ignore
            
        let mutable result :Token = Token.None
        let mutable values :int64[] = [|0|]
        
        while stack.TryPop(&result) do
            let closingTokenValue = [| getClosingTokenValue(result) |]
            values <- closingTokenValue |> Array.append values
        
        values |> Array.reduce (fun acc elem -> (acc * 5L) + elem)        
        
let handleLine (input:string)=
    let queue = input.ToCharArray() |> Array.map charToToken
    TokenType(queue).getInvalidTokenScore()

let handleLines(input:string)=
    input.Split(Environment.NewLine)
            |> Array.map handleLine
            |> Array.sum
            
let handleLinesPuzzle2(input:string)=
    let scoreArray = input.Split(Environment.NewLine)
                        |> Array.map (fun line -> (line, handleLine(line)))
                        |> Array.filter (fun (_, score) -> score = 0)
                        |> Array.map fst
                        |> Array.map (fun line -> line.ToCharArray()
                                                        |> Array.map charToToken
                                                        |> (fun elem -> TokenType(elem).getMissingTokenScore()))
                        |> Array.sort
    let devision = (scoreArray.Length / 2) 
    scoreArray[devision]
                    
            
            
            

