'=====================================================================
'
'  File: chebyshev-expansions.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The ChebyshevSeries class resides in the
' Numerics.NET.Curves namespace.
Imports Numerics.NET.Curves
' The Constants class and Func(Of Double, Double) delegate reside in the
' Numerics.NET namespace.
Imports Numerics.NET

Module ChebyshevExpansions

    ' Illustrates the use of the ChebyshevSeries class
    ' in the Numerics.NET.Curve namespace of Numerics.NET.
    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Chebyshev polynomials form an alternative basis
        ' for polynomials. A Chebyshev expansion is a
        ' polynomial expressed as a sum of Chebyshev
        ' polynomials.
        '
        ' Using the ChebyshevSeries class instead of
        ' Polynomial can have two major advantages:
        '   1. They are numerically more stable. Higher
        '      accuracy is maintained even for large problems.
        '   2. When approximating other functions with
        '      polynomials, the coefficients in the
        '      Chebyshev expansion will tend to decrease
        '      in size, where those of the normal polynomial
        '      approximation will tend to oscillate wildly.

        ' Index variable.
        Dim index As Int32

        '
        ' Constructing Chebyshev expansions
        '

        ' Chebyshev expansions are defined over an interval.
        ' The first constructor requires you to specify the
        ' boundaries of the interval, and the coefficients
        ' of the expansion.
        Dim coefficients = New Double() {1, 0.5, -0.3, 0.1}
        Dim chebyshev1 As New ChebyshevSeries(coefficients, 0, 2)
        ' If you omit the boundaries, they are assumed to be
        ' -1 and +1:
        Dim chebyshev2 As New ChebyshevSeries(coefficients)

        '
        ' Chebyshev approximations
        '

        ' A third way to construct a Chebyshev series is as an
        ' approximation to an arbitrary function. For more
        ' about the Func(Of Double, Double) delegate, see the
        ' FunctionDelegates QuickStart Sample.
        '
        ' Chebyshev expansions allow us to obtain an
        ' excellent approximation at minimal cost.
        '
        ' The following creates a Chebyshev approximation
        ' of degree 7 to Cos(x) over the interval (0, 2):
        Dim cos As Func(Of Double, Double) = AddressOf Math.Cos
        Dim approximation1 =
                ChebyshevSeries.GetInterpolatingPolynomial(cos, 0, 2, 7)
        Console.WriteLine("Chebyshev approximation of cos(x):")
        For index = 0 To 7
            Console.WriteLine("  c{0} = {1}", index,
                    approximation1.Parameters(index))
        Next

        ' The largest errors are approximately at the
        ' zeroes of the Chebyshev polynomial of degree 8:
        For index = 0 To 8
            Dim zero As Double =
                    1 + Math.Cos(index * Constants.Pi / 8)
            Dim err As Double =
                    approximation1.ValueAt(zero) - Math.Cos(zero)
            Console.WriteLine(" Error {0} = {1}", index, err)
        Next

        '
        ' Least squares approximations
        '

        ' We will now calculate the least squares polynomial
        ' of degree 7 through 33 points.
        ' First, calculate the points:
        Dim xValues = New Double(32) {}
        Dim yValues = New Double(32) {}
        For index = 0 To 32
            Dim angle As Double = index * Constants.Pi / 32
            xValues(index) = 1 + Math.Cos(angle)
            yValues(index) = Math.Cos(xValues(index))
        Next
        ' Next, define a ChebyshevBasis object for the
        ' approximation we want: interval (0,2) and degree
        ' is 7.
        Dim basis As New ChebyshevBasis(0, 2, 7)
        ' Now we can calculate the least squares fit:
        Dim approximation2 =
                CType(basis.LeastSquaresFit(xValues, yValues, xValues.Length),
                ChebyshevSeries)
        ' We can see it is close to the original
        ' approximation we found earlier:
        For index = 0 To 7
            Console.WriteLine("  c{0} = {1}", index,
                    approximation2.Parameters(index))
        Next


    End Sub

End Module
