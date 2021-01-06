namespace Docs

// Copy of the reactjs.org todo list.

module App =
    open Elmish
    open Elmish.React
    open Fable.React
    open Fable.React.Props
    open Fss

    // Colors
    let blue = CSSColor.Hex "0d6efd"
    let darkBlue = CSSColor.Hex "01398D"

    // Font
    let textFont = FontFamily.Custom "Roboto"
    let container =
        fss
            [
                Display.Flex
                FlexDirection.Column
                Padding.Value(rem 0., rem 1.5)
                textFont
            ]
    let header = fss [ Color' blue ]
    let todoStyle =
        let fadeInAnimation =
            keyframes
                [
                    frame 0 
                        [
                            Opacity' 0.
                            Transform.TranslateY(px 20)
                        ]
                    frame 100 
                        [
                            Opacity' 1.
                            Transform.TranslateY(px 0)
                        ]
                ]
        let indexCounter = counterStyle []
        fss
            [
                CounterIncrement' indexCounter
                FontSize' (px 20)
                AnimationName' fadeInAnimation
                AnimationDuration' (sec 0.4)
                AnimationTimingFunction.Ease
                ListStyleType.None
                Before
                    [
                        Color.Hex "48f"
                        Content.Counter(indexCounter,". ")
                    ]
            ]
    let formStyle =
        [
            Display.InlineBlock
            Padding.Value(px 10, px 15)
            FontSize' (px 18);
            BorderRadius' (px 0)
        ]
    let buttonStyle =
        fss
            [
                yield! formStyle
                Border.None
                BackgroundColor' blue
                Color.white
                Width' (em 10.)
                Hover
                    [
                        Cursor.Pointer
                        BackgroundColor' darkBlue
                    ]
            ]
    let inputStyle =
        fss
            [
                yield! formStyle
                BorderRadius' (px 0)
                BorderWidth.Thin
                MarginRight' (px 25)
                Width' (px 400)
            ]

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
        div [ ClassName container ]
            [
                h2 [ ClassName header ] [ str "TODO" ]
                ul [] <| List.map (fun todo -> li [ ClassName todoStyle] [ str todo ]) model.Todos
                div []
                    [
                        input
                            [
                                ClassName inputStyle
                                Placeholder "What needs to be done?"
                                Value model.Input
                                OnChange (fun e -> e.Value |> SetInput |> dispatch)
                            ]
                        button
                            [
                                ClassName buttonStyle
                                OnClick (fun _ -> model.Input |> AddTodo |> dispatch)
                            ]
                            [ str $"Add #{List.length model.Todos}" ]
                    ]
            ]

    Program.mkSimple init update render
    |> Program.withReactSynchronous "elmish-app"
    |> Program.run
