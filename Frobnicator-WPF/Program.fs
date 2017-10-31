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

    let init () = { value = 0 }

    let update msg model =
        match msg with
        | Increment -> { model with value = model.value + 1 }

module App =
    open State
    open Types
    open Frobnicator_WPF.Views

    let view _ _ = []

    [<EntryPoint; STAThread>]
    let main argv = 
        Program.mkSimple init update view
        |> Program.runWindow(MainWindow())
    

    