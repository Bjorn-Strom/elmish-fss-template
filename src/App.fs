namespace Docs

// Copy of the reactjs.org todo list.

module App =
    open Elmish
    open Elmish.React
    open Fable.React
    open Fable.React.Props

    type Model = {
        Input: string
        Todos: string list
    }

    type Msg =
        | SetInput of string
        | AddTodo of string

    let init() = {
        Input = ""
        Todos = []
    }

    let update (msg: Msg) (model: Model): Model =
        match msg with
        | SetInput input -> { model with Input = input }
        | AddTodo todo ->
            { model with
                Todos = todo::model.Todos
                Input = "" }

    let render (model: Model) (dispatch: Msg -> unit) =
        div []
            [
                h3 [] [ str "TODO" ]
                str "What needs to be done"
                ul [] <| List.map (fun todo -> li [] [ str todo ]) model.Todos
                input
                    [
                        Value model.Input
                        OnChange (fun e -> e.Value |> SetInput |> dispatch)
                    ]
                button [ OnClick (fun _ -> model.Input |> AddTodo |> dispatch) ]
                    [ str $"Add #{List.length model.Todos}" ]
            ]

    Program.mkSimple init update render
    |> Program.withReactSynchronous "elmish-app"
    |> Program.run
