'=====================================================================
'
'  File: optimization-in-1d.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The optimization classes resides in the
' Numerics.NET.EquationSolvers namespace.
Imports Numerics.NET.Optimization
' Function delegates reside in the Numerics.NET
' namespace.
Imports Numerics.NET

' Illustrates the use of the Brent and Golden Section optimizers
' in the Numerics.NET.Optimization namespace of Numerics.NET.
Module OptimizationIn1D

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Several algorithms exist for optimizing functions
        ' in one variable. The most common one is
        ' Brent's algorithm.

        '
        ' Brent's algorithm
        '

        ' Now let's create the BrentOptimizer object.
        Dim optimizer As BrentOptimizer = New BrentOptimizer

        ' Set the objective function:
        optimizer.ObjectiveFunction = AddressOf TestFunction1
        ' Optimizers can find either a minimum or a maximum.
        ' Which of the two is specified by the ExtremumType
        ' property
        optimizer.ExtremumType = ExtremumType.Minimum

        ' The first phase is to find an interval that contains
        ' a local minimum. This is done by the FindBracket method.
        optimizer.FindBracket(0, 6)
        ' You can verify that an interval was found from the
        ' IsBracketValid property:
        If (Not optimizer.IsBracketValid) Then
            Throw New Exception("An interval containing a minimum was not found.")
        End If

        ' Finally, we can run the optimizer by calling the FindExtremum method:
        optimizer.FindExtremum()

        Console.WriteLine("Function 1: x^3 - 2x - 5")
        ' The Status property indicates
        ' the result of running the algorithm.
        Console.WriteLine($"  Result: {optimizer.Status}")
        ' The result is available through the
        ' Extremum property.
        Console.WriteLine($"  Minimum: {optimizer.Extremum}")
        Dim exactResult = Math.Sqrt(2 / 3.0)
        Dim result = optimizer.Extremum
        Console.WriteLine($"  Exact minimum: {exactResult}")

        ' You can find out the estimated error of the result
        ' through the EstimatedError property:
        Console.WriteLine($"  Estimated error: {optimizer.EstimatedError}")
        Console.WriteLine($"  Actual error: {Math.Abs(result - exactResult)}")
        Console.WriteLine($"  # iterations: {optimizer.IterationsNeeded}")

        Console.WriteLine("Function 2: 1/Exp(x*x - 0.7*x +0.2)")
        ' You can also perform these calculations more directly
        ' using the FindMinimum or FindMaximum methods. This implicitly
        ' calls the FindBracket method.
        result = optimizer.FindMaximum(AddressOf TestFunction2, 0)
        Console.WriteLine($"  Maximum: {result}")
        Console.WriteLine($"  Actual maximum: {0.35}")
        Console.WriteLine($"  Estimated error: {optimizer.EstimatedError}")
        Console.WriteLine($"  Actual error: {result - 0.35}")
        Console.WriteLine($"  # iterations: {optimizer.IterationsNeeded}")

        '
        ' Golden section search
        '

        ' A slower but simpler algorithm for finding an extremum
        ' is the golden section search. It is implemented by the
        ' GoldenSectionMinimizer class:
        Dim optimizer2 As New GoldenSectionOptimizer

        Console.WriteLine("Using Golden Section optimizer:")
        result = optimizer2.FindMaximum(AddressOf TestFunction2, 0)
        Console.WriteLine($"  Maximum: {result}")
        Console.WriteLine($"  Actual maximum: {0.35}")
        Console.WriteLine($"  Estimated error: {optimizer2.EstimatedError}")
        Console.WriteLine($"  Actual error: {result - 0.35}")
        Console.WriteLine($"  # iterations: {optimizer2.IterationsNeeded}")

    End Sub

    ' Minimum at x = Sqrt(2/3) = 0.816496580927726
    Function TestFunction1(x As Double) As Double
        Return x * x * x - 2 * x - 5
    End Function

    ' Maximum at x = 0.35
    Function TestFunction2(x As Double) As Double
        Return 1 / Math.Exp(x * x - 0.7 * x + 0.2)
    End Function

End Module
