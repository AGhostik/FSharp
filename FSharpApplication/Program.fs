//Дмитрий Сошников, Функциональное программирование на F# 
//http://www.soshnikov.com/fsharp
//

open System.Numerics

let rec rpt n f x =
    if n=0 then x
    else f (rpt (n-1) f x)

let mandelf (c:Complex) (z:Complex) = z*z+c

let isMandel c = Complex.Abs(rpt 20 (mandelf c) (Complex.Zero)) < 1.0;;

let scale (x:float, y:float) (u,v) n = float(n-u)/float(v-u)*(y-x)+x

open System.Drawing
open System.Windows.Forms

let form =
    let image = new Bitmap(400, 400)
    let lscale = scale (-1.2, 1.2) (0, image.Height-1)
    for i = 0 to (image.Height - 1) do
        for j = 0 to (image.Width - 1) do
            let t = Complex ((lscale i), (lscale j)) in
            image.SetPixel(i, j, if isMandel t then Color.Black else Color.White)
    let temp = new Form()
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp.Show()
    temp
    
[<EntryPoint>]
let main argv = 
    Application.Run(form)
    0 // return an integer exit code