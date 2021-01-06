namespace Docs

// Copy of the reactjs.org todo list.

module App =
    open Elmish
    open Elmish.React
    open Fable.React
    open Fable.React.Props
    open Fss

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
                Todos = model.Todos @ [todo]
                Input = "" }

    let render (model: Model) (dispatch: Msg -> unit) =
        div []
            [
                h2 [] [ str "TODO" ]
                ul [] <| List.map (fun todo -> li [] [ str todo ]) model.Todos
                div []
                    [
                        input
                            [
                                Placeholder "What needs to be done?"
                                Value model.Input
                                OnChange (fun e -> e.Value |> SetInput |> dispatch)
                            ]
                        button
                            [
                                OnClick (fun _ -> model.Input |> AddTodo |> dispatch)
                            ]
                            [ str $"Add #{List.length model.Todos}" ]
                    ]
            ]

    Program.mkSimple init update render
    |> Program.withReactSynchronous "elmish-app"
    |> Program.run
