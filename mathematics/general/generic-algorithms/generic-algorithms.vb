'=====================================================================
'
'  File: generic-algorithms.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' Basic generic types live in Numerics.NET.Generics.
Imports Numerics.NET.Generic
' We'll also need the big number types.
Imports Numerics.NET

    ' Illustrates writing generic algorithms that can be
    ' applied to different operand types using the types in the
    ' Numerics.NET.Generic namespace.
    Module GenericAlgorithms

        Sub main()
            ' We will implement a simple Newton-Raphson solver class.
            ' The code for the solver is below.

            ' Here we will call the generic solver with three
            ' different operand types: BigFloat, BigRational and Double.

            ' First, let's compute pi to 100 digits
            ' by solving the equation sin(x) == 0 with
            ' an initual guess of 3.
            Console.WriteLine("Computing pi by solving sin(x) = 0 with x0 = 3 using BigFloat.")
            ' Create the solver object.
            Dim bigFloatSolver As New Solver(Of BigFloat)
            ' Set the function to solve, and its derivative.
            ' These functions are defined below.
            bigFloatSolver.TargetFunction = AddressOf fBigFloat
            bigFloatSolver.DerivativeOfTargetFunction = AddressOf dfBigFloat
            ' Now solve to within a tolerance of 10^-100.
            Dim pi As BigFloat = bigFloatSolver.Solve(3, BigFloat.Pow(10, -100))
            ' Print the results...
            Console.WriteLine($"Computed value: {pi:F100}")
            ' and verify:
            Console.WriteLine("Known value:    {0:F100}",
                BigFloat.GetPi(AccuracyGoal.Absolute(100)))
            Console.WriteLine()

            ' Next, we will use rational numbers to compute
            ' an approximation to the square root of 2.
            Console.WriteLine("Computing sqrt(2) by solving x^2 = 2 using BigRational.")
            ' Create the solver...
            Dim bigRationalSolver As New Solver(Of BigRational)()
            ' Set properties...
            bigRationalSolver.TargetFunction = AddressOf fBigRational
            bigRationalSolver.DerivativeOfTargetFunction = AddressOf dfBigRational
            ' Compute the solution...
            Dim sqrt2 As BigRational = bigRationalSolver.Solve(1, BigRational.Pow(10, -100))
            ' And print the result.
            Console.WriteLine($"Rational approximation: {sqrt2}")
            ' To verify, we convert the BigRational to a BigFloat:
            Console.WriteLine("As real number: {0:F100}",
                New BigFloat(sqrt2, AccuracyGoal.Absolute(100), RoundingMode.TowardsNearest))
            Console.WriteLine("Known value:    {0:F100}",
                BigFloat.Sqrt(2, AccuracyGoal.Absolute(100), RoundingMode.TowardsNearest))
            Console.WriteLine()

            ' Finally, we compute the Lambert W function at x = 3.
            Console.WriteLine("Computing Lambert's W at x = 3 using Double.")
            ' Create the solver...
            Dim doubleSolver As New Solver(Of Double)()
            ' Set properties...
            doubleSolver.TargetFunction = AddressOf fDouble
            doubleSolver.DerivativeOfTargetFunction = AddressOf dfDouble
            ' Compute the solution...
            Dim W3 As Double = doubleSolver.Solve(1.0, 0.000000000000001)
            ' And print the result.
            Console.WriteLine($"Solution:    {W3}")
            Console.WriteLine($"Known value: {Elementary.LambertW(3.0)}")

            ' Finally, we use generic functions:
            Console.WriteLine("To 100 digits (using BigFloat):")
            bigFloatSolver.TargetFunction = AddressOf fGeneric(Of BigFloat)
            bigFloatSolver.DerivativeOfTargetFunction = AddressOf dfGeneric(Of BigFloat)
            Dim bigW3 As BigFloat = bigFloatSolver.Solve(1, BigFloat.Pow(10, -100))
            Console.WriteLine($"Solution:    {bigW3:F100}")

        End Sub

        ' Functions for solving sin(x) = 0
        Function fBigFloat(x As BigFloat) As BigFloat
            Return BigFloat.Sin(x)
        End Function
        Function dfBigFloat(x As BigFloat) As BigFloat
            Return BigFloat.Cos(x)
        End Function

        ' Functions for solving x^2 - 2 = 0
        Function fBigRational(x As BigRational) As BigRational
            Return x * x - 2
        End Function
        Function dfBigRational(x As BigRational) As BigRational
            Return 2 * x
        End Function

        ' Functions for solving x*exp(x) = 3 (i.e. W(3))
        Function fDouble(x As Double) As Double
            Return x * Math.Exp(x) - 3
        End Function
        Function dfDouble(x As Double) As Double
            Return Math.Exp(x) * (1 + x)
        End Function

        ' Generic versions of the above
        Function fGeneric(Of T)(x As T) As T
            Return Operations(Of T).Subtract(
                Operations(Of T).Multiply(x, Operations(Of T).Exp(x)),
                Operations(Of T).FromInt32(3))
        End Function
        Function dfGeneric(Of T)(x As T) As T
            Return Operations(Of T).Multiply(
                Operations(Of T).Exp(x),
                Operations(Of T).Add(x, Operations(Of T).One))
        End Function

    End Module

' Class that contains the generic Newton-Raphson algorithm.
Class Solver(Of T)

    ' Member fields:
    Dim f, df As Func(Of T, T)
    Dim maxIters As Integer = 100

    ' The function to solve:
    Public Property TargetFunction() As Func(Of T, T)
        Get
            Return f
        End Get
        Set(value As Func(Of T, T))
            f = value
        End Set
    End Property

    ' The derivative of the function to solve.
    Public Property DerivativeOfTargetFunction() As Func(Of T, T)
        Get
            Return df
        End Get
        Set(value As Func(Of T, T))
            df = value
        End Set
    End Property

    ' The maximum number of iterations.
    Public Property MaxIterations() As Integer
        Get
            Return maxIters
        End Get
        Set(value As Integer)
            maxIters = value
        End Set
    End Property

    ' The core algorithm.
    ' Arithmetic operations are replaced by calls to
    ' methods on the arithmetic object (ops).
    Public Function Solve(initialGuess As T, tolerance As T) As T

        Dim iterations As Integer = 0

        Dim x As T = initialGuess
        Dim dx As T = Operations(Of T).Zero
        Do
            iterations = iterations + 1
            ' Compute the denominator of the correction term.
            Dim dfx As T = df(x)
            ' Relational operators map to the Compare method.
            ' We also use the value of zero for the operand type.
            ' if (dfx == 0)
            If Operations(Of T).EqualTo(dfx, Operations(Of T).Zero) Then
                ' Change value by 2x tolerance.
                ' When multiplying by a power of two, it's more efficient
                ' to use the ScaleByPowerOfTwo method.
                dx = Operations(Of T).ScaleByPowerOfTwo(tolerance, 1)
            Else
                ' dx = f(x) / df(x)
                dx = Operations(Of T).Divide(f(x), dfx)
            End If
            ' x -= dx
            x = Operations(Of T).Subtract(x, dx)

            ' if |dx|^2<tolerance
            ' Convergence is quadratic (in most cases), so we should be good here:
            If Operations(Of T).LessThan(Operations(Of T).Multiply(dx, dx), tolerance) Then
                Return x
            End If
        Loop While (iterations < MaxIterations)
        Return x
    End Function

End Class
