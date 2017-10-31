namespace Frobnicator.WPF

open System
open Elmish
open Elmish.WPF
open Counter

module State =
    type Model = 
        { top : Counter.Model; bottom : Counter.Model}

    type Msg = 
        | Top of Counter.Msg
        | Bottom of Counter.Msg
        | Reset

    let init () = 
        let top, topCmd = Counter.init ()
        let bottom, bottomCmd = Counter.init ()
        { top = top ; bottom = bottom}, Cmd.batch [Cmd.map Top topCmd
                                                   Cmd.map Bottom bottomCmd]

    let update msg model =
        match msg with
        | Top msg' when msg' = Incremented 5 ->
            let counter, msg = Counter.init()
            {model with top = counter}, Cmd.none
        | Top msg' -> 
            let counter', cmd = Counter.update msg' model.top
            {model with top = counter'}, Cmd.map Top cmd
        | Bottom msg' -> 
            let counter', cmd = Counter.update msg' model.bottom
            {model with bottom = counter'}, Cmd.map Bottom cmd
        | Reset ->
            init () 

module App =
    open State
    open Frobnicator.Views

    let view _ _ = 
        ["Reset" |> Binding.cmd (fun model msg -> Reset) 
         "Top" |> Binding.model (fun model -> model.top) Counter.viewBinding Top
         "Bottom" |> Binding.model (fun model -> model.bottom) Counter.viewBinding Bottom
        ]

    [<EntryPoint; STAThread>]
    let main argv = 
        Program.mkProgram init update view
        |> Program.withConsoleTrace
        |> Program.runWindow(MainWindow())
    