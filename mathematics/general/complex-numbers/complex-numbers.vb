'=====================================================================
'
'  File: complex-numbers.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The Complex(Of T) class resides in the Numerics.NET namespace.
Imports Numerics.NET

    ' Illustrates the use of the Complex(Of Double) class in the
    ' Numerics.NET.
    ' </summary>
    Module ComplexNumbers

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Complex(Of Double) constants:
        '
        Console.WriteLine($"Complex(Of Double).Zero = {Complex(Of Double).Zero}")
        Console.WriteLine($"Complex(Of Double).One = {Complex(Of Double).One}")
        ' The imaginary unit is given by Complex(Of Double).I:
        Console.WriteLine($"Complex(Of Double).I = {Complex(Of Double).I}")
        Console.WriteLine()

        '
        ' Construct some complex numbers
        '
        ' Real and imaginary parts:
        '   a = 2 + 4i
        Dim a As New Complex(Of Double)(2, 4)
        Console.WriteLine($"a = {a}")
        '   b = 1 - 3i
        Dim b As New Complex(Of Double)(1, -3)
        Console.WriteLine($"b = {b.ToString()}")
        ' From a real number:
        '   c = -3 + 0i
        Dim c As New Complex(Of Double)(-3)
        Console.WriteLine($"c = {c.ToString()}")
        ' Polar form:
        '   d = 2 (cos(Pi/3) + i sin(Pi/3))
        Dim d = Complex(Of Double).FromPolar(2, Constants.Pi / 3)
        ' To print this number, use the overloaded ToString
        ' method and specify the format string for the real
        ' and imaginary parts:
        Console.WriteLine($"d = {d.ToString("F4")}")
        Console.WriteLine()

        '
        ' Parts of complex numbers
        '
        Console.WriteLine($"Parts of a = {a}:")
        Console.WriteLine($"Real part of a = {a.Re}")
        Console.WriteLine($"Imaginary part of a = {a.Im}")
        Console.WriteLine($"Modulus of a = {a.Magnitude}")
        Console.WriteLine($"Argument of a = {a.Phase}")
        Console.WriteLine()

        '
        ' Basic arithmetic:
        '
        Console.WriteLine("Basic arithmetic:")
        Dim e As Complex(Of Double)
        e = -a
        Console.WriteLine($"-a = {e}")
        e = a + b
        Console.WriteLine($"a + b = {e}")
        e = a - b
        Console.WriteLine($"a - b = {e}")
        e = a * b
        Console.WriteLine($"a * b = {e}")
        e = a / b
        Console.WriteLine($"a / b = {e}")
        e = a.Conjugate
        Console.WriteLine($"Conjugate(a) = {e}")
        Console.WriteLine()

        '
        ' Functions of complex numbers
        '
        ' Most of these have corresponding Shared methods
        ' in the System.Math class, but are extended to complex
        ' arguments.
        Console.WriteLine("Functions of complex numbers:")

        ' Exponentials and logarithms
        Console.WriteLine("Exponentials and logarithms:")
        e = Complex(Of Double).Exp(a)
        Console.WriteLine($"Exp(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Log(a)
        Console.WriteLine($"Log(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Log10(a)
        Console.WriteLine($"Log10(a) = {e.ToString("F4")}")
        ' You can get a point on the unit circle by calling
        ' the ExpI method:
        e = Complex(Of Double).ExpI(2 * Constants.Pi / 3)
        Console.WriteLine($"ExpI(2*Pi/3) = {e.ToString("F4")}")
        ' The RootOfUnity method also returns points on the
        ' unit circle. The above is equivalent to the second
        ' root of z^6 = 1:
        e = Complex(Of Double).RootOfUnity(6, 2)
        Console.WriteLine($"RootOfUnity(6, 2) = {e.ToString("F4")}")

        ' The Pow method is overloaded for integer, double,
        ' and complex argument:
        e = Complex(Of Double).Pow(a, 3)
        Console.WriteLine($"Pow(a,3) = {e.ToString("F4")}")
        e = Complex(Of Double).Pow(a, 1.5)
        Console.WriteLine($"Pow(a,1.5) = {e.ToString("F4")}")
        e = Complex(Of Double).Pow(a, b)
        Console.WriteLine($"Pow(a,b) = {e.ToString("F4")}")

        ' Square root
        e = Complex(Of Double).Sqrt(a)
        Console.WriteLine($"Sqrt(a) = {e.ToString("F4")}")
        ' The Sqrt method is overloaded. Here's the square
        ' root of a negative double:
        e = Complex(Of Double).Sqrt(-4)
        Console.WriteLine($"Sqrt(-4) = {e.ToString("F4")}")
        Console.WriteLine()

        '
        ' Trigonometric functions:
        '
        Console.WriteLine("Trigonometric function:")
        e = Complex(Of Double).Sin(a)
        Console.WriteLine($"Sin(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Cos(a)
        Console.WriteLine($"Cos(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Tan(a)
        Console.WriteLine($"Tan(a) = {e.ToString("F4")}")

        ' GetInverse Trigonometric functions:
        e = Complex(Of Double).Asin(a)
        Console.WriteLine($"Asin(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Acos(a)
        Console.WriteLine($"Acos(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Atan(a)
        Console.WriteLine($"Atan(a) = {e.ToString("F4")}")

        ' Asin and Acos have overloads with real argument
        ' not restricted to [-1,1]:
        e = Complex(Of Double).Asin(2)
        Console.WriteLine($"Asin(2) = {e.ToString("F4")}")
        e = Complex(Of Double).Acos(2)
        Console.WriteLine($"Acos(2) = {e.ToString("F4")}")
        Console.WriteLine()

        '
        ' Hyperbolic and inverse hyperbolic functions:
        '
        Console.WriteLine("Hyperbolic function:")
        e = Complex(Of Double).Sinh(a)
        Console.WriteLine($"Sinh(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Cosh(a)
        Console.WriteLine($"Cosh(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Tanh(a)
        Console.WriteLine($"Tanh(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Asinh(a)
        Console.WriteLine($"Asinh(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Acosh(a)
        Console.WriteLine($"Acosh(a) = {e.ToString("F4")}")
        e = Complex(Of Double).Atanh(a)
        Console.WriteLine($"Atanh(a) = {e.ToString("F4")}")
        Console.WriteLine()

    End Sub

End Module
