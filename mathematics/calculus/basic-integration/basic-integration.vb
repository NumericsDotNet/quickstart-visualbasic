'=====================================================================
'
'  File: basic-integration.vb
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

' Illustrates the use of the Newton-Raphson equation solver
' in the Numerics.NET.EquationSolvers namespace of Numerics.NET.
Module BasicIntegration

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Numerical integration algorithms fall into two
        ' main categories: adaptive and non-adaptive.
        ' This QuickStart Sample illustrates the use of
        ' the non-adaptive numerical integrators.
        '
        ' All numerical integration classes derive from
        ' NumericalIntegrator. This abstract base class
        ' defines properties and methods that are shared
        ' by all numerical integration classes.

        '
        ' The integrand
        '
        ' The function we are integrating must be
        ' provided as a Func(Of Double, Double). For more
        ' information about this delegate, see the
        ' Functions QuickStart sample.
        Dim f As Func(Of Double, Double) = AddressOf Math.Sin
        ' Variable to hold the result:
        Dim result As Double

        '
        ' SimpsonIntegrator
        '

        ' The simplest numerical integration algorithm
        ' is Simpson's rule.
        Dim simpson As New SimpsonIntegrator()
        ' You can set the relative or absolute tolerance
        ' to which to evaluate the integral.
        simpson.RelativeTolerance = 0.00001
        ' You can select the type of tolerance using the
        ' ConvergenceCriterion property:
        simpson.ConvergenceCriterion = ConvergenceCriterion.WithinRelativeTolerance
        ' The Integrate method performs the actual
        ' integration:
        result = simpson.Integrate(f, 0, 5)
        Console.WriteLine("sin(x) on [0,2]")
        Console.WriteLine("Simpson integrator:")
        ' The result is also available in the Result
        ' property:
        Console.WriteLine($"  Value: {simpson.Result}")
        ' To see whether the algorithm ended normally,
        ' inspect the Status property:
        Console.WriteLine($"  Status: {simpson.Status}")
        ' You can find out the estimated error of the result
        ' through the EstimatedError property:
        Console.WriteLine($"  Estimated error: {simpson.EstimatedError}")
        ' The number of iterations to achieve the result
        ' is available through the IterationsNeeded property.
        Console.WriteLine($"  Iterations: {simpson.IterationsNeeded}")
        ' The number of function evaluations is available
        ' through the EvaluationsNeeded property.
        Console.WriteLine($"  Function evaluations: {simpson.EvaluationsNeeded}")

        '
        ' Gauss-Kronrod Integration
        '

        ' Gauss-Kronrod integrators also use a fixed point
        ' scheme, but with certain optimizations in the
        ' choice of points where the integrand is evaluated.

        ' The NonAdaptiveGaussKronrodIntegrator uses a
        ' succession of 10, 21, 43, and 87 point rules
        ' to approximate the integral.
        Dim nagk As New NonAdaptiveGaussKronrodIntegrator()
        nagk.Integrate(f, 0, 5)
        Console.WriteLine("Non-adaptive Gauss-Kronrod rule:")
        Console.WriteLine($"  Value: {nagk.Result}")
        Console.WriteLine($"  Status: {nagk.Status}")
        Console.WriteLine($"  Estimated error: {nagk.EstimatedError}")
        Console.WriteLine($"  Iterations: {nagk.IterationsNeeded}")
        Console.WriteLine($"  Function evaluations: {nagk.EvaluationsNeeded}")

        '
        ' Romberg Integration
        '

        ' Romberg integration combines Simpson's Rule
        ' with a scheme to accelerate convergence.
        ' This algorithm is useful for smooth integrands.
        Dim romberg As New RombergIntegrator()
        result = romberg.Integrate(f, 0, 5)
        Console.WriteLine("Romberg integration:")
        Console.WriteLine($"  Value: {romberg.Result}")
        Console.WriteLine($"  Status: {romberg.Status}")
        Console.WriteLine($"  Estimated error: {romberg.EstimatedError}")
        Console.WriteLine($"  Iterations: {romberg.IterationsNeeded}")
        Console.WriteLine($"  Function evaluations: {romberg.EvaluationsNeeded}")

        ' However, it breaks down if the integration
        ' algorithm contains singularities or
        ' discontinuities.
        f = AddressOf HardIntegrand
        result = romberg.Integrate(f, 0, 1)
        Console.WriteLine("Romberg on hard integrand:")
        Console.WriteLine($"  Value: {romberg.Result}")
        Console.WriteLine("  Actual value: 100")
        Console.WriteLine($"  Status: {romberg.Status}")
        Console.WriteLine($"  Estimated error: {romberg.EstimatedError}")
        Console.WriteLine($"  Iterations: {romberg.IterationsNeeded}")
        Console.WriteLine($"  Function evaluations: {romberg.EvaluationsNeeded}")

    End Sub

    ' Function that will cause difficulties to the
    ' simplistic integration algorithms.
    Private Function HardIntegrand(x As Double) As Double
        ' This is put in because some integration rules
        ' evaluate the function at x=0.
        If (x <= 0) Then
            Return 0
        End If
        Return Math.Pow(x, -0.9) * Math.Log(1 / x)
    End Function

End Module
