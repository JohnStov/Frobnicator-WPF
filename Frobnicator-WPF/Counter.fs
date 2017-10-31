namespace Frobnicator.WPF

open Elmish
open Elmish.WPF

module Counter = 

    type Model = { value : int }

    let init () = { value = 0 }, Cmd.none

    type Msg =
        | Increment
        | Incremented of int

    let update msg model =
        match msg with
        | Increment -> { model with value = model.value + 1 }, Cmd.ofMsg (Incremented model.value)
        | Incremented _ -> model, Cmd.none

    let viewBinding : ViewBindings<Model, Msg> =
        [ "Value"       |> Binding.oneWay (fun model -> model.value)
          "ButtonClick" |> Binding.cmd (fun model msg -> Increment) ]
