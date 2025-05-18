'=====================================================================
'
'  File: nd-integration.vb
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
Imports Numerics.NET.Algorithms

    ' Illustrates numerical integration in higher dimensions using
    ' classes in the Numerics.NET.Calculus namespace of Numerics.NET.
    Module NDIntegration

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Two-dimensional integration
        '

        ' The function we are integrating must be
        ' provided as a Func(Of Double, Double, Double) delegate.
        ' For more information about this delegate, see the
        ' FunctionDelegates QuickStart sample.
        '
        ' The functions used in this sample are defined at
        ' the end of this file.
        Dim f1 As Func(Of Double, Double, Double) = AddressOf Integrand1
        Dim f2 As Func(Of Double, Double, Double) = AddressOf Integrand2
        Dim f3 As Func(Of Double, Double, Double) = AddressOf Integrand3

        ' Variable to hold the result:
        Dim result As Double

        ' The AdaptiveIntegrator2D class is the most efficient
        ' 2D integrator in most cases. It uses an adaptive algorithm.

        ' Construct an instance of the integrator class:
        Dim integrator1 As New AdaptiveIntegrator2D()

        ' An example of setting the integrand and bounds through properties
        ' is given below. Here, we put the integrand and the bounds
        ' of the integration region directly in the call to Integrate,
        ' which performs the calculation:
        integrator1.Integrate(f1, 0, 1, 0, 1)
        Console.WriteLine("4 / (1 + 2x + 2y) on [0,1] * [0,1]")
        Console.WriteLine($"  Value:       {integrator1.Result:F15}")
        Console.WriteLine($"  Exact value: {Math.Log(3125.0 / 729.0):F15} = Ln(3125 / 729)")
        ' To see whether the algorithm ended normally,
        ' inspect the Status property:
        Console.WriteLine("  Status: {0}",
                integrator1.Status)
        Console.WriteLine("  Estimated error: {0}",
                integrator1.EstimatedError)
        Console.WriteLine("  Iterations: {0}",
                integrator1.IterationsNeeded)
        Console.WriteLine("  Function evaluations: {0}",
                integrator1.EvaluationsNeeded)

        ' Another integrator uses repeated 1-dimensional
        ' integration:
        Dim integrator2 As New Repeated1DIntegrator2D()

        ' You can set the order of integration, as well as
        ' the integration rules for the X and the Y direction:
        integrator2.InitialDirection = Repeated1DIntegratorDirection.X

        ' You can set the integrand and the bounds of the integration region
        ' by setting properties of the integrator object:
        integrator2.Integrand = f2
        integrator2.XLowerBound = 0.0
        integrator2.XUpperBound = 1.0
        integrator2.YLowerBound = 0.0
        integrator2.YUpperBound = 1.0

        result = integrator2.Integrate()
        Console.WriteLine("Pi^2 / 4 Sin(Pi x) Sin(Pi y)   on [0,1] * [0,1]")
        Console.WriteLine($"  Value:       {integrator2.Result:F15}")
        Console.WriteLine($"  Exact value: {1.0:F15}")
        ' To see whether the algorithm ended normally,
        ' inspect the Status property:
        Console.WriteLine("  Status: {0}",
                integrator2.Status)
        Console.WriteLine("  Estimated error: {0}",
                integrator2.EstimatedError)
        Console.WriteLine("  Iterations: {0}",
                integrator2.IterationsNeeded)
        Console.WriteLine("  Function evaluations: {0}",
                integrator2.EvaluationsNeeded)

        '
        ' Integration over arbitrary regions
        '

        ' The repeated 1D integrator can also be used to compute
        ' integrals over arbitrary regions. To do this, you need to
        ' supply function that return the lower bound and upper bound
        ' of the region as a function of x.

        ' Here, we integrate x^2 * y^2 over the unit disk.
        integrator2.LowerBoundFunction = AddressOf DiskLowerBound
        integrator2.UpperBoundFunction = AddressOf DiskUpperBound
        integrator2.XLowerBound = -1.0
        integrator2.XUpperBound = 1.0

        integrator2.Integrand = f3

        result = integrator2.Integrate()
        Console.WriteLine("x^2 * y^2 on the unit disk")
        Console.WriteLine($"  Value:       {integrator2.Result:F15}")
        Console.WriteLine($"  Exact value: {Math.PI / 24:F15} = Pi / 24")
        ' To see whether the algorithm ended normally,
        ' inspect the Status property:
        Console.WriteLine("  Status: {0}",
                integrator2.Status)
        Console.WriteLine("  Estimated error: {0}",
                integrator2.EstimatedError)
        Console.WriteLine("  Iterations: {0}",
                integrator2.IterationsNeeded)
        Console.WriteLine("  Function evaluations: {0}",
                integrator2.EvaluationsNeeded)

    End Sub

    ' <summary>
    ' Function to integrate over [0, 1] * [0, 1]
    ' </summary>
    Function Integrand1(x As Double, y As Double) As Double
        Return 4 / (1 + 2 * x + 2 * y)
    End Function

    ' <summary>
    ' Function to integrate over [0, 1] * [0, 1]
    ' </summary>
    Function Integrand2(x As Double, y As Double) As Double
        Return Math.PI * Math.PI / 4 * Math.Sin(Math.PI * x) * Math.Sin(Math.PI * y)
    End Function

    ' <summary>
    ' Lower bound of a unit disk as a function of x.
    ' </summary>
    Function DiskLowerBound(x As Double) As Double
        x = Math.Abs(x)
        If (x >= 1) Then
            Return 0
        End If
        Return -Math.Sqrt(1 - x * x)
    End Function

    ' <summary>
    ' Upper bound of a unit disk as a function of x.
    ' </summary>
    Function DiskUpperBound(x As Double) As Double
        x = Math.Abs(x)
        If (x >= 1) Then
            Return 0
        End If
        Return Math.Sqrt(1 - x * x)
    End Function

    ' <summary>
    ' Function to integrate over the unit disc.
    ' </summary>
    Function Integrand3(x As Double, y As Double) As Double
        Return x * x * y * y
    End Function

End Module
