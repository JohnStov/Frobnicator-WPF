namespace Frobnicator.WPF

open System
open Elmish
open Elmish.WPF

module Types =

    type Model = { value : int }

    type Message =
        | Increment

module State =
    open Types

    let init () = { value = 0 }, Cmd.Empty

    let update msg model =
        match msg with
        | Increment -> { model with value = model.value + 1 }, Cmd.Empty

module App =
    open State
    open Types
    open Frobnicator.Views

    let view model dispatch = 
        [ "Value"       |> Binding.oneWay (fun model -> model.value)
          "ButtonClick" |> Binding.cmd (fun model msg -> Increment) ]
            

    [<EntryPoint; STAThread>]
    let main argv = 
        Program.mkProgram init update view
        |> Program.withConsoleTrace
        |> Program.runWindow(MainWindow())
    

    