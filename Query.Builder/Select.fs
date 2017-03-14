namespace Query.Builder

(*
module Select =
    let add a b = a + b
*)

module Select =
    let select = "SELECT"

    let column q c = q + "," + c

    let table q t = q + " " + t

    let sum list = List.reduce<string> (+) list

    let commaSeparate lst = List.map (fun s -> s + ",") lst

    let reduceAdd lst = List.reduce (fun s r -> s + "" + r) lst
