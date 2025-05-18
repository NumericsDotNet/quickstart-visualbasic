'=====================================================================
'
'  File: advanced-polynomials.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The Complex(Of Double) structure resides in the Numerics.NET namespace.
Imports Numerics.NET
' The Polynomial class resides in the Numerics.NET.Curves namespace.
Imports Numerics.NET.Curves

Module AdvancedPolynomials

    ' Illustrates the more advanced uses of the Polynomial class
    ' in the Numerics.NET.Curve namespace of Numerics.NET.
    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Basic operations on polynomials are covered in the
        ' BasicPolynomials QuickStart Sample. This QuickStart
        ' Sample focuses on more advanced topics, including
        ' finding complex roots, calculating least-squares
        ' polynomials, and polynomial arithmetic.

        ' Index variable.
        Dim index As Int32

        '
        ' Complex(Of Double) numbers and polynomials
        '

        Dim myPolynomial As Polynomial = New Polynomial(New Double() {-2, 0, 1, 1})

        ' The Polynomial class supports complex numbers
        ' as arguments for polynomials. It does not support
        ' polynomials with complex coefficients.
        '
        ' For more about complex numbers, see the
        ' ComplexNumbers QuickStart Sample.
        Dim z1 As New Complex(Of Double)(1, 2)

        ' Polynomial provides variants of ValueAt and
        ' SlopeAt for complex arguments:
        Console.WriteLine("polynomial.ComplexValueAt({0}) = {1}",
                z1, myPolynomial.ComplexValueAt(z1))
        Console.WriteLine("polynomial.ComplexSlopeAt({0}) = {1}",
                z1, myPolynomial.ComplexSlopeAt(z1))

        '
        ' Real and complex roots
        '
        ' Our polynomial has only one real root:
        Dim roots = myPolynomial.FindRoots()
        Console.WriteLine("Number of roots of polynomial1: {0}",
                roots.Length)
        Console.WriteLine($"Value of root 1 = {roots(0)}")
        ' The FindComplexRoots method returns all three
        ' roots, two of which are complex:
        Dim complexRoots = myPolynomial.FindComplexRoots()
        Console.WriteLine("Number of complex roots: {0}",
                complexRoots.Length)
        Console.WriteLine("Value of root 1 = {0}",
                complexRoots(0))
        Console.WriteLine("Value of root 2 = {0}",
                complexRoots(1))
        Console.WriteLine("Value of root 3 = {0}",
                complexRoots(2))

        '
        ' Least squares polynomials
        '

        ' Let's approximate 7 points on the unit circle
        ' by a fourth degree polynomial in the least squares
        ' sense.
        ' First, we create two arrays containing the x and
        ' y values of our data points:
        Dim xValues As Double() = New Double(6) {}
        Dim yValues As Double() = New Double(6) {}
        Dim angle As Double = 0
        For index = 0 To 6
            xValues(index) = Math.Cos(angle)
            yValues(index) = -Math.Sin(angle)
            angle = angle + Constants.Pi / 6
        Next
        ' Now we can find the least squares polynomial
        ' by calling the ststic LeastSquaresFit method.
        ' The last parameter is the degree of the desired
        ' polynomial.
        Dim lsqPolynomial As Polynomial =
                polynomial.LeastSquaresFit(xValues, yValues, 4)
        ' Note that, as expected, the odd coefficients
        ' are close to zero.
        Console.WriteLine("Least squares fit: {0}",
                lsqPolynomial.ToString())

        '
        ' Polynomial arithmetic
        '
        ' We can add, subtract, multiply and divide
        ' polynomials using overloaded operators:
        Dim a As polynomial = New polynomial(New Double() {4, -2, 2})
        Dim b As polynomial = New polynomial(New Double() {-3, 1})
        Dim c As Polynomial

        Console.WriteLine($"a = {a.ToString()}")
        Console.WriteLine($"b = {b.ToString()}")
        c = polynomial.Add(a, b)
        Console.WriteLine($"Add(a, b) = {c.ToString()}")
        c = polynomial.Subtract(a, b)
        Console.WriteLine($"Subtract(a, b) = {c.ToString()}")
        c = polynomial.Multiply(a, b)
        Console.WriteLine($"Multiply(a, b) = {c.ToString()}")
        c = polynomial.Divide(a, b)
        Console.WriteLine($"Divide(a, b) = {c.ToString()}")
        c = polynomial.Modulus(a, b)
        Console.WriteLine($"Remainder(a, b) = {c.ToString()}")
        ' You can also calculate quotient and remainder
        ' at the same time by calling the overloaded Divide
        ' method:
        Dim d As Polynomial = Nothing
        c = Polynomial.Divide(a, b, d)
        Console.WriteLine("Using Divide method:")
        Console.WriteLine($"  a / b = {c.ToString()}")
        Console.WriteLine($"  a % b = {d.ToString()}")


    End Sub

End Module
