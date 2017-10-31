namespace Frobnicator.WPF

open System
open Elmish
open Elmish.WPF

module Counter = 

    type Model = { value : int }

    let init () = { value = 0 }

    type Message =
        | Increment

    let update msg model =
        match msg with
        | Increment -> { model with value = model.value + 1 }

    let viewBinding : ViewBindings<Model, Message> =
        [ "Value"       |> Binding.oneWay (fun model -> model.value)
          "ButtonClick" |> Binding.cmd (fun model msg -> Increment) ]


module State =
    open Counter

    type Model = 
        { top : Counter.Model; bottom : Counter.Model}

    type Message = 
        | Top of Counter.Message
        | Bottom of Counter.Message
        | Reset

    let init () = 
        { top = Counter.init () ; bottom = Counter.init ()}

    
    let update msg model =
        match msg with
        | Top msg' -> 
            let counter' = Counter.update msg' model.top
            {model with top = counter'}
        | Bottom msg' -> 
            let counter' = Counter.update msg' model.bottom
            {model with bottom = counter'}
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
        Program.mkSimple init update view
        |> Program.withConsoleTrace
        |> Program.runWindow(MainWindow())
    