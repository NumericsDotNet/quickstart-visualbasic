'=====================================================================
'
'  File: elementary-functions.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The Elementary class resides in the Numerics.NET
' namespace.
Imports Numerics.NET

' Illustrates the use of the elementary functions implemented
' by the Elementary class in the Numerics.NET.Curve namespace
' of Numerics.NET.
Module UsingElementaryFunctions

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' The special functions are implemented as static
        ' methods. Special functions of the same general
        ' category are grouped together in one class.
        '
        ' There are classes for: elementary functions,
        ' number theoretic functions, combinatorics,
        ' probability, hyperbolic functions, the gamma
        ' function and related functions, Bessel functions,
        ' Airy functions, and exponential integrals.
        '
        ' This QuickStart sample deals with elementary
        ' functions, implemented in the Elementary class.

        '
        ' Elementary functions
        '

        ' Evaluating Log(1+x) directly causes significant
        ' round-off error when x is close to 0. The
        ' Log1PlusX function allows high precision evaluation
        ' of this expression for values of x close to 0:
        Console.WriteLine("Logarithm of 1+1e-12")
        Console.WriteLine("  Math.Log: {0}",
                Math.Log(1 + 0.000000000001))
        Console.WriteLine("  Log1PlusX: {0}",
            Elementary.Log1PlusX(0.000000000001))

        ' In a similar way, Exp(x) - 1 has a variant,
        ' ExpXMinus1, for values of x close to 0:
        Console.WriteLine("Exponential of 1e-12 minus 1.")
        Console.WriteLine("  Math.Exp: {0}",
                Math.Exp(0.000000000001) - 1)
        Console.WriteLine("  ExpMinus1: {0}",
                Elementary.ExpMinus1(0.000000000001))

        ' The hypotenuse of two numbers that are very large
        ' may cause an overflow when not evaluated properly:
        Console.WriteLine("Hypotenuse:")
        Dim a As Double = 3.0E+200
        Dim b As Double = 4.0E+200
        Console.Write("  Simple method: ")
        Try
            Console.WriteLine(Math.Sqrt(a * a + b * b))
        Catch e As OverflowException
            Console.WriteLine("Overflow!")
        End Try
        Console.WriteLine("  Elementary.Hypot: {0}",
                Elementary.Hypot(a, b))

        ' Raising numbers to integer powers is much faster
        ' than raising numbers to real numbers. The
        ' overloaded Pow method implements this:
        Console.WriteLine("2.5^19 = {0}", Elementary.Pow(2.5, 19))
        ' You can raise numbers to negative integer powers
        ' as well:
        Console.WriteLine("2.5^-19 = {0}", Elementary.Pow(2.5, -19))

    End Sub

End Module
