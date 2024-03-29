﻿module LabProg2019.Menu

open System
open Engine
open Gfx
open Maze

[< NoEquality; NoComparison >]
type state = {
    player : sprite
}

let W = 50
let H = 50

let main () =       
    let engine = new engine (W, H)
    let e0_position = float ((H - 3) / 2)
    let e1_position = e0_position + 2.
    let e2_position = e0_position + 4.
    let w = (W - 11) / 2

    let my_update (key : ConsoleKeyInfo) (screen : wronly_raster) (st : state) =
        // move pointer + check enter
        let i, dy =
            match key.KeyChar with 
            | 'w' -> false, -2.
            | 's' -> false, 2.
            | 'l' -> true, 0.
            | _   -> false, 0.

        let y_positon = st.player.y
        
        if (i) then
            if (y_positon = e0_position) then
                let condition = Game_mode1.main_game(W, H)
                End.End(W, H, condition)

            elif (y_positon = e1_position) then
                let condition = Game_mode2.main_game(W, H)
                End.End(W, H, condition)

            elif (y_positon = e2_position) then 
                Game_mode3.main_game(W, H)

        if ((y_positon = e0_position && (dy = 2.))) || ((y_positon = e1_position) || (y_positon = e2_position && (dy = -2.))) then
            st.player.move_by (0., dy)

        st, key.KeyChar = 'q'

    // create pointer and text
    let player = engine.create_and_register_sprite (image.point (pixel.filled Color.Red), w - 2, int e0_position, 0)
    ignore <| engine.create_and_register_sprite (image.text ("Game mode 1", Color.White), w, int e0_position, 0)
    ignore <| engine.create_and_register_sprite (image.text ("Game mode 2", Color.White), w, int e1_position, 0)
    ignore <| engine.create_and_register_sprite (image.text ("Game mode 3", Color.White), w, int e2_position, 0)
    ignore <| engine.create_and_register_sprite (image.text ("Press l to play", Color.Gray), (W - 15)/2, ((H - 3) / 2) + 8 , 0)
    ignore <| engine.create_and_register_sprite (image.text ("Press q to quit", Color.Gray), (W - 15)/2, ((H - 3) / 2) + 10 , 0)

    // initialize state
    let st0 = { 
        player = player
        }
    // start engine
    engine.loop_on_key my_update st0