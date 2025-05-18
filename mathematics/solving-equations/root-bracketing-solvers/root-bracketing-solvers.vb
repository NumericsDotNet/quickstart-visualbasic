'=====================================================================
'
'  File: root-bracketing-solvers.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The RootBracketingSolver and derived classes reside in the
' Numerics.NET.EquationSolvers namespace.
Imports Numerics.NET.EquationSolvers
' Function delegates reside in the Numerics.NET
' namespace.
Imports Numerics.NET.Algorithms
Imports Numerics.NET

    ' Illustrates the use of the root bracketing solvers
    ' in the Numerics.NET.EquationSolvers namespace of Numerics.NET.
    Module RootBracketingSolvers

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Root bracketing solvers are used to solve
        ' non-linear equations in one variable.
        '
        ' Root bracketing solvers start with an interval
        ' which is known to contain a root. This interval
        ' is made smaller and smaller in successive
        ' iterations until a certain tolerance is reached,
        ' or the maximum number of iterations has been
        ' exceeded.
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
        Dim f As Func(Of Double, Double) = AddressOf Math.Cos
        ' All root bracketing solvers inherit from
        ' RootBracketingSolver, an abstract base class.
        Dim solver As RootBracketingSolver

        '
        ' Bisection method
        '

        ' The bisection method halves the interval during
        ' each iteration. It is implemented by the
        ' BisectionSolver class.
        Console.WriteLine("BisectionSolver: cos(x) = 0 over [1,2]")
        solver = New BisectionSolver()
        solver.LowerBound = 1
        solver.UpperBound = 2
        solver.TargetFunction = f
        Dim result As Double = solver.Solve()
        ' The Status property indicates
        ' the result of running the algorithm.
        Console.WriteLine("  Result: {0}",
                solver.Status)
        ' The result is also available through the
        ' Result property.
        Console.WriteLine($"  Solution: {solver.Result}")
        ' You can find out the estimated error of the result
        ' through the EstimatedError property:
        Console.WriteLine("  Estimated error: {0}",
                solver.EstimatedError)
        Console.WriteLine("  # iterations: {0}",
                solver.IterationsNeeded)

        '
        ' Regula Falsi method
        '
        ' The Regula Falsi method optimizes the selection
        ' of the next interval. Unfortunately, the
        ' optimization breaks down in some cases.
        ' Here is an example:
        Console.WriteLine("RegulaFalsiSolver: cos(x) = 0 over [1,2]")
        solver = New RegulaFalsiSolver()
        solver.LowerBound = 1
        solver.UpperBound = 2
        solver.MaxIterations = 1000
        solver.TargetFunction = f
        result = solver.Solve()
        Console.WriteLine("  Result: {0}",
                solver.Status)
        Console.WriteLine($"  Solution: {solver.Result}")
        Console.WriteLine("  Estimated error: {0}",
                solver.EstimatedError)
        Console.WriteLine("  # iterations: {0}",
                solver.IterationsNeeded)

        ' However, for sin(x) = 0, everything is fine:
        Console.WriteLine("RegulaFalsiSolver: sin(x) = 0 over [-0.5,1]")
        solver = New RegulaFalsiSolver()
        solver.LowerBound = -0.5
        solver.UpperBound = 1
        solver.TargetFunction = AddressOf Math.Sin
        result = solver.Solve()
        Console.WriteLine("  Result: {0}",
                solver.Status)
        Console.WriteLine($"  Solution: {solver.Result}")
        Console.WriteLine("  Estimated error: {0}",
                solver.EstimatedError)
        Console.WriteLine("  # iterations: {0}",
                solver.IterationsNeeded)

        '
        ' Dekker-Brent method
        '
        ' The Dekker-Brent method combines the best of
        ' both worlds. It is the most robust and, on average,
        ' the fastest method.
        Console.WriteLine("DekkerBrentSolver: cos(x) = 0 over [1,2]")
        solver = New DekkerBrentSolver()
        solver.LowerBound = 1
        solver.UpperBound = 2
        solver.TargetFunction = f
        result = solver.Solve()
        Console.WriteLine("  Result: {0}",
                solver.Status)
        Console.WriteLine($"  Solution: {solver.Result}")
        Console.WriteLine("  Estimated error: {0}",
                solver.EstimatedError)
        Console.WriteLine("  # iterations: {0}",
                solver.IterationsNeeded)

        '
        ' Controlling the process
        '
        Console.WriteLine("Same with modified parameters:")
        ' You can set the maximum # of iterations:
        ' If the solution cannot be found in time, the
        ' Status will return a value of
        ' IterationStatus.IterationLimitExceeded
        solver.MaxIterations = 20
        ' You can specify how convergence is to be tested
        ' through the ConvergenceCriterion property:
        solver.ConvergenceCriterion =
                         ConvergenceCriterion.WithinRelativeTolerance
        ' And, of course, you can set the absolute or
        ' relative tolerance.
        solver.RelativeTolerance = 0.00001
        ' In this example, the absolute tolerance will be
        ' ignored.
        solver.AbsoluteTolerance = 1.0E-24
        solver.LowerBound = 157081
        solver.UpperBound = 157082
        solver.TargetFunction = f
        result = solver.Solve()
        Console.WriteLine("  Result: {0}",
                solver.Status)
        Console.WriteLine($"  Solution: {solver.Result}")
        ' The estimated error will be less than 0.157
        Console.WriteLine("  Estimated error: {0}",
                solver.EstimatedError)
        Console.WriteLine("  # iterations: {0}",
                solver.IterationsNeeded)

    End Sub

End Module
