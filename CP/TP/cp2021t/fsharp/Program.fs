// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Nat
open LTree

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    //printfn "Hello world %s" message
    printfn "Bubble Sort [5,3,2,9,11] = %A" (bSort [5;3;2;9;11])
    printfn "Merge Sort [5,3,2,9,11] = %A" (mSort [5;3;2;9;11])
    //printfn "Fact 10 = %d" (LTree.dfac 5)
    0 // return an integer exit code