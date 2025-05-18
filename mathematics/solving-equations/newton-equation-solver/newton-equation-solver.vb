'=====================================================================
'
'  File: newton-equation-solver.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The NewtonRaphsonSolver class resides in the
' Numerics.NET.EquationSolvers namespace.
Imports Numerics.NET.EquationSolvers
' Function delegates reside in the Numerics.NET
' namespace.
Imports Numerics.NET

    ' Illustrates the use of the Newton-Raphson equation solver
    ' in the Numerics.NET.EquationSolvers namespace of Numerics.NET.
    Module NewtonEquationSolver

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' The Newton-Raphson solver is used to solve
        ' non-linear equations in one variable.
        '
        ' The algorithm starts with one starting value,
        ' and uses the target function and its derivative
        ' to iteratively find a closer approximation to
        ' the root of the target function.
        '
        ' The properties and methods that give you control
        ' over the iteration are shared by all classes
        ' that implement iterative algorithms.

        '
        ' Target function
        '
        ' The function we are trying to solve must be
        ' provided as a Func(Of Double, Double). For more
        ' information about this delegate, see the
        ' Functions QuickStart sample.
        Dim f As Func(Of Double, Double) = AddressOf Math.Sin
        ' The Newton-Raphson method also requires knowledge
        ' of the derivative:
        Dim df As Func(Of Double, Double) = AddressOf Math.Cos
        ' Now let's create the NewtonRaphsonSolver object.
        Dim solver As NewtonRaphsonSolver = New NewtonRaphsonSolver
        ' Set the target function and its derivative:
        solver.TargetFunction = f
        solver.DerivativeOfTargetFunction = df
        ' Set the initial guess:
        solver.InitialGuess = 4
        ' These values can also be passed in a constructor:
        Dim solver2 As New NewtonRaphsonSolver(f, df, 4)

        Console.WriteLine("Newton-Raphson Solver: sin(x) = 0")
        Console.WriteLine("  Initial guess: 4")
        Dim result As Double = solver.Solve()
        ' The Status property indicates
        ' the result of running the algorithm.
        Console.WriteLine($"  Result: {solver.Status}")
        ' The result is also available through the
        ' Result property.
        Console.WriteLine($"  Solution: {solver.Result}")
        ' You can find out the estimated error of the result
        ' through the EstimatedError property:
        Console.WriteLine($"  Estimated error: {solver.EstimatedError}")
        Console.WriteLine($"  # iterations: {solver.IterationsNeeded}")

        '
        ' When you don't have the derivative...
        '
        ' You can still use this class if you don't have
        ' the derivative of the target function. In this
        ' case, use the static CreateDelegate method of the
        ' NumericalDifferentiation class (Numerics.NET.Calculus
        ' namespace) to create a Func(Of Double, Double)
        ' that represents the numerical derivative of the
        ' target function:
        f = AddressOf Special.BesselJ0
        solver.TargetFunction = f
        solver.DerivativeOfTargetFunction =
                FunctionMath.GetNumericalDifferentiator(f)
        solver.InitialGuess = 5
        Console.WriteLine("Zero of Bessel function near x=5:")
        result = solver.Solve()
        Console.WriteLine($"  Result: {solver.Status}")
        Console.WriteLine($"  Solution: {solver.Result}")
        Console.WriteLine($"  Estimated error: {solver.EstimatedError}")
        Console.WriteLine($"  # iterations: {solver.IterationsNeeded}")

        '
        ' Controlling the process
        '
        Console.WriteLine("Same with modified parameters:")
        ' You can set the maximum # of iterations:
        ' If the solution cannot be found in time, the
        ' Status will return a value of
        ' IterationStatus.IterationLimitExceeded
        solver.MaxIterations = 10
        ' You can specify how convergence is to be tested
        ' through the ConvergenceCriterion property:
        solver.ConvergenceCriterion =
                ConvergenceCriterion.WithinRelativeTolerance
        ' And, of course, you can set the absolute or
        ' relative tolerance.
        solver.RelativeTolerance = 0.00000000000001
        ' In this example, the absolute tolerance will be
        ' ignored.
        solver.AbsoluteTolerance = 0.0001
        solver.InitialGuess = 5
        result = solver.Solve()
        Console.WriteLine($"  Result: {solver.Status}")
        Console.WriteLine($"  Solution: {solver.Result}")
        ' The estimated error will be less than 5e-14
        Console.WriteLine($"  Estimated error: {solver.EstimatedError}")
        Console.WriteLine($"  # iterations: {solver.IterationsNeeded}")

    End Sub

End Module
