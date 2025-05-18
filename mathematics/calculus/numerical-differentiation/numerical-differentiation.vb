'=====================================================================
'
'  File: numerical-differentiation.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The numerical integration classes reside in the
' Numerics.NET.Calculus namespace.
Imports Numerics.NET.Calculus
' Function delegates reside in the Numerics.NET
' namespace.
Imports Numerics.NET

    ' Illustrates numerical differentiation using the
    ' NumericalDifferentiation class in the Numerics.NET.Calculus
    ' namespace of Numerics.NET.
    Module NumericalDifferentiationSample

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Numerical differentiation is a fairly simple
        ' procedure. Its accuracy is inherently limited
        ' because of unavoidable round-off error.
        '
        ' All calculations are performed by Shared methods
        ' of the NumericalDifferentiation class.
        Dim result As Double
        Dim estimatedError As Double

        '
        ' Standard numerical differentiation.
        '

        ' Central differences are the standard way of
        ' approximating the result of a function.
        ' For this to work, it must be possible to
        ' evaluate the target function on both sides of
        ' the point where the numerical result is
        ' requested.
        '
        ' The target function must be provided as a
        ' Func(Of Double, Double). For more information about
        ' this delegate, see the Functions
        ' QuickStart Sample.
        Dim fCentral As Func(Of Double, Double) = AddressOf Math.Cos
        Console.WriteLine("Central differences:")
        ' The actual calculation is performed by the
        ' CentralDerivative method.
        result = FunctionMath.CentralDerivative(fCentral, 1)
        ' All FunctionMath members are extension methods,
        ' so you can call them as instance methods on the delegate:
        result = fCentral.CentralDerivative(1)
        ' This is the syntax we'll use in the remainder of this sample.
        Console.WriteLine($"  Result = {result}")
        Console.WriteLine($"  Actual = {-Math.Sin(1)}")
        ' This method is overloaded. It has an optional
        ' out parameter that returns an estimate for the
        ' error in the result.
        result = fCentral.CentralDerivative(1, estimatedError)
        Console.WriteLine($"  Estimated error = {estimatedError}")

        '
        ' Forward and backward differences.
        '

        ' Some functions are not defined everywhere.
        ' If the result is required on a boundary
        ' of the domain where it is defined, the central
        ' differences method breaks down. This also happens
        ' if the function has a discontinuity close to the
        ' differentiation point.
        '
        ' In these cases, either forward or backward
        ' differences may be used instead.
        '
        ' The FForward function at the end of this file
        ' is an example of a function that may require
        ' forward differences. It is undefined for
        ' x < -2.
        Dim FForward As Func(Of Double, Double) = AddressOf FnForward

        ' Calculating the derivative using central
        ' differences returns NaN (Not a Number):
        result = FForward.CentralDerivative(-2, estimatedError)
        Console.WriteLine("Central differences can break down:")
        Console.WriteLine($"  Derivative = {result}")
        Console.WriteLine($"  Estimated error = {estimatedError}")
        ' Using the ForwardDerivative method does work:
        Console.WriteLine("Using forward differences:")
        result = FForward.ForwardDerivative(-2, estimatedError)
        Console.WriteLine($"  Derivative = {result}")
        Console.WriteLine($"  Estimated error = {estimatedError}")

        ' The FBackward function at the end of this file
        ' is an example of a function that requires
        ' backward differences for differentiation at
        ' x = 2.
        Dim fBackward As Func(Of Double, Double) = AddressOf FnBackward
        Console.WriteLine("Using backward differences:")
        result = fBackward.BackwardDerivative(2, estimatedError)
        Console.WriteLine($"  Derivative = {result}")
        Console.WriteLine($"  Estimated error = {estimatedError}")

        '
        ' Derivative function
        '

        ' In some cases, it may be useful to have the
        ' derivative of a function in the form of a
        ' Func(Of Double, Double), so it can be passed as
        ' an argument to other methods. This is very
        ' easy to do.
        Console.WriteLine("Using delegates:")
        ' For central differences:
        Dim dfCentral As Func(Of Double, Double) = fCentral.GetNumericalDifferentiator()
        Console.WriteLine($"Central: f'(1) = {dfCentral(1)}")

        ' For forward differences:
        Dim dfForward As Func(Of Double, Double) = FForward.GetForwardDifferentiator()
        Console.WriteLine($"Forward: f'(-2) = {dfForward(-2)}")

        ' For backward differences:
        Dim dfBackward As Func(Of Double, Double) = fBackward.GetBackwardDifferentiator()
        Console.WriteLine($"Backward: f'(1) = {dfBackward(1)}")

    End Sub

    ' Function that requires the forward differences
    ' for numerical differentiation.
    Private Function FnForward(x As Double) As Double
        Return (x + 2) * (x + 2) * Math.Sqrt(x + 2)
    End Function

    ' Function that requires the backward differences
    ' for numerical differentiation.
    Private Function FnBackward(x As Double) As Double
        If (x > 0) Then
            Return 1
        Else
            Return Math.Sin(x)
        End If
    End Function

End Module
