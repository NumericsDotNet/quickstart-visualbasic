'=====================================================================
'
'  File: piecewise-curves.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The piecewise curve classes reside in the
' Numerics.NET.Curves namespace.
Imports Numerics.NET.Curves
Imports Numerics.NET

    ' Illustrates the use of the PiecewiseConstantCurve and
    ' PiecewiseLinearCurve classes.
    Module PiecewiseCurves

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' A piecewise curve is a curve that has a different definition
        ' on subintervals of its domain.
        '
        ' This QuickStart Sample illustrates constant and linear piecewise
        ' curves, which - as the name suggest - are constant or linear
        ' on each interval.
        '
        ' For an example of cubic splines, see the CubicSplines QuickStart
        ' Sample.
        '

        '
        ' Piecewise constants
        '

        ' All piecewise curves inherit from the PiecewiseCurve class.
        ' Piecewise constant curves are implemented by the
        ' PiecewiseConstantCurve class. It has three constructors.

        ' The first constructor takes two double arrays as parameters.
        ' These contain the x and y values of the data points:
        Dim xValues As Double() = {1, 2, 3, 4, 5, 6}
        Dim yValues As Double() = {1, 3, 4, 3, 4, 2}
        Dim constant1 As New PiecewiseConstantCurve(xValues, yValues)

        ' The second constructor takes two Vector objects, containing the
        ' x and y-values of the data points:
        Dim xVector = Vector.Create(xValues)
        Dim yVector = Vector.Create(yValues)
        Dim constant2 As New PiecewiseConstantCurve(xVector, yVector)

        ' The third constructor only takes one parameter: an array of
        ' Point structures that represent the data point.
        Dim dataPoints = New Point() _
                {New Point(1, 1), New Point(2, 3), New Point(3, 4),
                 New Point(4, 3), New Point(5, 4), New Point(6, 2)}
        Dim constant3 As New PiecewiseConstantCurve(dataPoints)

        '
        ' Curve Parameters
        '

        ' The shape of any curve is determined by a set of parameters.
        ' These parameters can be retrieved and set through the
        ' Parameters collection. The number of parameters for a curve
        ' is given by this collection's Count property.
        '
        ' Piecewise constant curves have 2n parameters, where n is the number of
        ' data points. The first n parameters are the x-values. The next
        ' n parameters are the y-values.

        Console.WriteLine($"constant1.Parameters.Count = {constant1.Parameters.Count}")
        ' Parameters can easily be retrieved:
        Console.WriteLine($"constant1.Parameters(0) = {constant1.Parameters(0)}")
        ' Parameters can also be set:
        constant1.Parameters(0) = 1

        '
        ' Curve Methods
        '

        ' The ValueAt method returns the y value of the
        ' curve at the specified x value:
        Console.WriteLine($"constant1.ValueAt(2.4) = {constant1.ValueAt(2.4)}")

        ' The SlopeAt method returns the slope of the curve
        ' a the specified x value:
        Console.WriteLine($"constant1.SlopeAt(2.4) = {constant1.SlopeAt(2.4)}")
        ' The slope at the data points is Double.NaN if the value of the constant
        ' is different on either side of the data point:
        Console.WriteLine($"constant1.SlopeAt(2) = {constant1.SlopeAt(2)}")

        ' Piecewise constant curves do not have a defined derivative.
        ' The GetDerivative method returns a GeneralCurve:
        Dim derivative As Curve = constant1.GetDerivative()
        Console.WriteLine($"Type of derivative: {derivative.GetType().ToString()}")
        Console.WriteLine($"derivative(2.4) = {derivative.ValueAt(2.4)}")

        ' You can get a Line that is the tangent to a curve
        ' at a specified x value using the TangentAt method:
        Dim tangent = constant1.TangentAt(2.4)
        Console.WriteLine($"Slope of tangent line at 2.4 = {tangent.Parameters(1)}")

        ' The integral of a piecewise constant curve can be calculated exactly.
        Console.WriteLine("Integral of constant1 between 1.4 and 4.6 = {0}",
                constant1.Integral(1.4, 4.6))

        '
        ' Piecewise linear curves
        '

        ' Piecewise linear curves are used for linear interpolation
        ' between data points. They are implemented by the
        ' PiecewiseLinearCurve class. It has three constructors,
        ' similar to the constructors for the PiecewiseLinearCurve
        ' class..These constructors create the linear interpolating
        ' curve between the data points.

        ' The first constructor takes two double arrays as parameters.
        ' These contain the x and y values of the data points:
        Dim xValues2 As Double() = {1, 2, 3, 4, 5, 6}
        Dim yValues2 As Double() = {1, 3, 4, 3, 4, 2}
        Dim line1 = New PiecewiseLinearCurve(xValues2, yValues2)

        ' The second constructor takes two Vector objects, containing the
        ' x and y-values of the data points:
        Dim xVector2 = Vector.Create(xValues2)
        Dim yVector2 = Vector.Create(yValues2)
        Dim line2 = New PiecewiseLinearCurve(xVector2, yVector2)

        ' The third constructor only takes one parameter: an array of
        ' Point structures that represent the data point.
        Dim dataPoints2 As Point() = New Point() _
                {New Point(1, 1), New Point(2, 3), New Point(3, 4),
                 New Point(4, 3), New Point(5, 4), New Point(6, 2)}
        Dim line3 As PiecewiseLinearCurve = New PiecewiseLinearCurve(dataPoints)

        '
        ' Curve Parameters
        '

        ' Piecewise linear curves have 2n parameters, where n is the number of
        ' data points. The first n parameters are the x-values. The next
        ' n parameters are the y-values.

        Console.WriteLine($"line1.Parameters.Count = {line1.Parameters.Count}")
        ' Parameters can easily be retrieved:
        Console.WriteLine($"line1.Parameters(0) = {line1.Parameters(0)}")
        ' Parameters can also be set:
        line1.Parameters(0) = 1

        '
        ' Curve Methods
        '

        ' The ValueAt method returns the y value of the
        ' curve at the specified x value:
        Console.WriteLine($"line1.ValueAt(2.4) = {line1.ValueAt(2.4)}")

        ' The SlopeAt method returns the slope of the curve
        ' a the specified x value:
        Console.WriteLine($"line1.SlopeAt(2.4) = {line1.SlopeAt(2.4)}")
        ' The slope at the data points is Double.NaN if the slope of the line
        ' is different on either side of the data point:
        Console.WriteLine($"line1.SlopeAt(2) = {line1.SlopeAt(2)}")

        ' Piecewise line curves do not have a defined derivative.
        ' The GetDerivative method returns a GeneralCurve:
        derivative = line1.GetDerivative()
        Console.WriteLine($"Type of derivative: {derivative.GetType().ToString()}")
        Console.WriteLine($"derivative(2.4) = {derivative.ValueAt(2.4)}")

        ' You can get a Line that is the tangent to a curve
        ' at a specified x value using the TangentAt method:
        tangent = line1.TangentAt(2.4)
        Console.WriteLine($"Slope of tangent line at 2.4 = {tangent.Parameters(1)}")

        ' The integral of a piecewise line curve can be calculated exactly.
        Console.WriteLine("Integral of line1 between 1.4 and 4.6 = {0}",
                line1.Integral(1.4, 4.6))

    End Sub

End Module
