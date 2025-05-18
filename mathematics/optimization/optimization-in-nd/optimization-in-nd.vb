'=====================================================================
'
'  File: optimization-in-nd.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The optimization classes reside in the
' Numerics.NET.Optimization namespace.
Imports Numerics.NET.Optimization
' Vectors and Function delegates reside in the Numerics.NET
' namespace.
Imports Numerics.NET

' Illustrates unconstrained optimization in multiple dimensions
' using classes in the Numerics.NET.Optimization
' namespace of Numerics.NET.
Module OptimizationInND

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Objective function
        '

        ' The objective function must be supplied as a
        ' Func(Of Vector, Double) delegate. This is a method
        ' that takes one Vector argument and returns a real number.
        ' See the end of this sample for definitions of the methods
        ' that are referenced here.
        Dim f As Func(Of Vector(Of Double), Double) = AddressOf fRosenbrock

        ' The gradient of the objective function can be supplied either
        ' as a MultivariateVectorFunction delegate, or a
        ' MultivariateVectorFunction delegate. The former takes
        ' one vector argument and returns a vector. The latter
        ' takes a second vector argument, which is an existing
        ' vector that is used to return the result.
        Dim g As Func(Of Vector(Of Double), Vector(Of Double), Vector(Of Double)) = AddressOf gRosenbrock

        ' The initial values are supplied as a vector:
        Dim initialGuess = Vector.Create(-1.2, 1)
        ' The actual solution is (1, 1).

        '
        ' Quasi-Newton methods: BFGS and DFP
        '

        ' For most purposes, the quasi-Newton methods give
        ' excellent results. There are two variations: DFP and
        ' BFGS. The latter gives slightly better results.

        ' Which method is used, is specified by a constructor
        ' parameter of type QuasiNewtonMethod:
        Dim bfgs As New QuasiNewtonOptimizer(QuasiNewtonMethod.Bfgs)

        bfgs.InitialGuess = initialGuess
        bfgs.ExtremumType = ExtremumType.Minimum

        ' Set the ObjectiveFunction:
        bfgs.ObjectiveFunction = f
        ' Set either the GradientFunction or FastGradientFunction:
        bfgs.FastGradientFunction = g
        ' The FindExtremum method does all the hard work:
        bfgs.FindExtremum()

        Console.WriteLine("BFGS Method:")
        Console.WriteLine($"  Solution: {bfgs.Extremum}")
        Console.WriteLine($"  Estimated error: {bfgs.EstimatedError}")
        Console.WriteLine($"  # iterations: {bfgs.IterationsNeeded}")
        ' Optimizers return the number of function evaluations
        ' and the number of gradient evaluations needed:
        Console.WriteLine($"  # function evaluations: {bfgs.EvaluationsNeeded}")
        Console.WriteLine($"  # gradient evaluations: {bfgs.GradientEvaluationsNeeded}")

        ' You can use Automatic Differentiation to compute the gradient.
        ' To do so, set the SymbolicObjectiveFunction to a lambda expression
        ' for the objective function:
        bfgs = New QuasiNewtonOptimizer(QuasiNewtonMethod.Bfgs)

        bfgs.InitialGuess = initialGuess
        bfgs.ExtremumType = ExtremumType.Minimum

        bfgs.SymbolicObjectiveFunction = Function(x) Math.Pow(1 - x(0), 2) + 105 * Math.Pow(x(1) - x(0) * x(0), 2)

        bfgs.FindExtremum()

        Console.WriteLine("BFGS using Automatic Differentiation:")
        Console.WriteLine($"  Solution: {bfgs.Extremum}")
        Console.WriteLine($"  Estimated error: {bfgs.EstimatedError}")
        Console.WriteLine($"  # iterations: {bfgs.IterationsNeeded}")
        Console.WriteLine($"  # function evaluations: {bfgs.EvaluationsNeeded}")
        Console.WriteLine($"  # gradient evaluations: {bfgs.GradientEvaluationsNeeded}")

        '
        ' Conjugate Gradient methods
        '

        ' Conjugate gradient methods exist in three variants:
        ' Fletcher-Reeves, Polak-Ribiere, and positive Polak-Ribiere.

        ' Which method is used, is specified by a constructor
        ' parameter of type ConjugateGradientMethod:
        Dim cg As New ConjugateGradientOptimizer(ConjugateGradientMethod.PositivePolakRibiere)
        ' Everything else works as before:
        cg.ObjectiveFunction = f
        cg.FastGradientFunction = g
        cg.InitialGuess = initialGuess
        cg.FindExtremum()

        Console.WriteLine("Conjugate Gradient Method:")
        Console.WriteLine($"  Solution: {cg.Extremum}")
        Console.WriteLine($"  Estimated error: {cg.EstimatedError}")
        Console.WriteLine($"  # iterations: {cg.IterationsNeeded}")
        Console.WriteLine($"  # function evaluations: {cg.EvaluationsNeeded}")
        Console.WriteLine($"  # gradient evaluations: {cg.GradientEvaluationsNeeded}")

        '
        ' Powell's method
        '

        ' Powell's method is a conjugate gradient method that
        ' does not require the derivative of the objective function.
        ' It is implemented by the PowellOptimizer class:
        Dim pw As New PowellOptimizer
        pw.InitialGuess = initialGuess
        ' Powell's method does not use derivatives:
        pw.ObjectiveFunction = f
        pw.FindExtremum()

        Console.WriteLine("Powell's Method:")
        Console.WriteLine($"  Solution: {pw.Extremum}")
        Console.WriteLine($"  Estimated error: {pw.EstimatedError}")
        Console.WriteLine($"  # iterations: {pw.IterationsNeeded}")
        Console.WriteLine($"  # function evaluations: {pw.EvaluationsNeeded}")
        Console.WriteLine($"  # gradient evaluations: {pw.GradientEvaluationsNeeded}")

        '
        ' Nelder-Mead method
        '

        ' Also called the downhill simplex method, the method of Nelder
        ' and Mead is useful for functions that are not tractable
        ' by other methods. For example, other methods
        ' may fail if the objective function is not continuous.
        ' Otherwise it is much slower than other methods.

        ' The method is implemented by the NelderMeadOptimizer class:
        Dim nm As New NelderMeadOptimizer

        ' The class has three special properties, that help determine
        ' the progress of the algorithm. These parameters have
        ' default values and need not be set explicitly.
        nm.ContractionFactor = 0.5
        nm.ExpansionFactor = 2
        nm.ReflectionFactor = -2

        ' Everything else is the same.
        nm.SolutionTest.Tolerance = 0.000000000000001
        nm.InitialGuess = initialGuess
        ' The method does not use derivatives:
        nm.ObjectiveFunction = f
        nm.FindExtremum()

        Console.WriteLine("Nelder-Mead Method:")
        Console.WriteLine($"  Solution: {nm.Extremum}")
        Console.WriteLine($"  Estimated error: {nm.EstimatedError}")
        Console.WriteLine($"  # iterations: {nm.IterationsNeeded}")
        Console.WriteLine($"  # function evaluations: {nm.EvaluationsNeeded}")

    End Sub

    ' The famous Rosenbrock test function.
    Function fRosenbrock(x As Vector(Of Double)) As Double
        Dim p As Double = (1 - x(0))
        Dim q As Double = x(1) - x(0) * x(0)
        Return p * p + 105 * q * q
    End Function

    ' Gradient of the Rosenbrock test function.
    Function gRosenbrock(x As Vector(Of Double), f As Vector(Of Double)) As Vector(Of Double)
        ' Always assume that the second argument may be null:
        If (f Is Nothing) Then
            f = Vector.Create(Of Double)(2)
        End If
        Dim p As Double = (1 - x(0))
        Dim q As Double = x(1) - x(0) * x(0)
        f(0) = -2 * p - 420 * x(0) * q
        f(1) = 210 * q
        Return f
    End Function

End Module
