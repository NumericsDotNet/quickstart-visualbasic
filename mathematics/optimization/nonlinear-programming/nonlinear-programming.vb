'=====================================================================
'
'  File: nonlinear-programming.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The linear programming classes reside in their own namespace.
Imports Numerics.NET.Optimization
' Vectors and matrices are in the Numerics.NET.LinearAlgebra
' namespace
Imports Numerics.NET

    ' Illustrates solving nonlinear programming problems
    ' using the classes in the Numerics.NET.Optimization
    ' namespace of Numerics.NET.
    Module NonlinearProgramming

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample illustrates the two ways to create a Nonlinear Program.

        ' The first is in terms of matrices. The coefficients
        ' are supplied as a matrix. The cost vector, right-hand side
        ' and constraints on the variables are supplied as a vector.

        Console.WriteLine("Problem with only linear constraints:")

        ' The variables are the concentrations of each chemical compound:
        ' H, H2, H2O, N, N2, NH, NO, O, O2, OH

        ' The objective function is the free energy, which we want to minimize:
        Dim c = Vector.Create(
                -6.089, -17.164, -34.054, -5.914, -24.721,
                -14.986, -24.1, -10.708, -26.662, -22.179)
        Dim objectiveFunction As Func(Of Vector(Of Double), Double) =
                Function(x) x.DotProduct(c + Vector.Log(x) - Math.Log(x.Sum()))
        Dim objectiveGradient As Func(Of Vector(Of Double), Vector(Of Double), Vector(Of Double)) =
                Function(x, y)
                    Dim s As Double = x.Sum()
                    Dim y1 = c + Vector.Log(x) - Math.Log(s) + 1 - x / s
                    y1.CopyTo(y)
                    Return y
                End Function

        ' The constraints are the mass balance relationships for each element.
        ' The rows correspond to the elements H, N, and O.
        ' The columns are the index of the variable.
        ' The value is the number of times the element occurs in the
        ' compound corresponding to the variable:
        ' H, H2, H2O, N, N2, NH, NO, O, O2, OH
        ' All this is stored in a sparse matrix, so 0 values are omitted:
        Dim A = Matrix.CreateSparse(3, 10,
                New Integer() {0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2},
                New Integer() {0, 1, 2, 5, 9, 3, 4, 5, 6, 2, 6, 7, 8, 9},
                New Double() {1, 2, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1})
        ' The right-hand sides are the atomic weights of the elements
        ' in the mixture.
        Dim b = Vector.Create(2.0, 1.0, 1.0)

        ' The number of moles for each compound must be positive.
        Dim l = Vector.CreateConstant(10, 0.000001)
        Dim u = Vector.CreateConstant(10, Double.PositiveInfinity)

        ' We create the nonlinear program with the specified constraints:
        ' Because we have variable bounds, we use the constructor
        ' that lets us do this.
        Dim nlp1 As New NonlinearProgram(objectiveFunction, objectiveGradient, A, b, b, l, u)

        ' We could add more (linear or nonlinear) constraints here,
        ' but this is all we have in our problem.

        nlp1.InitialGuess = Vector.CreateConstant(10, 0.1)
        Dim solution = nlp1.Solve()
        Console.WriteLine($"Solution: {solution:F3}")
        Console.WriteLine($"Optimal value:   {nlp1.OptimalValue:F5}")
        Console.WriteLine($"# iterations: {nlp1.SolutionReport.IterationsNeeded}")
        Console.WriteLine()

        ' The second method is building the nonlinear program from scratch.

        Console.WriteLine("Problem with nonlinear constraints:")

        ' We start by creating a nonlinear program object. We supply
        ' the number of variables in the constructor.
        Dim nlp2 As New NonlinearProgram(2)

        nlp2.ObjectiveFunction = Function(x) Math.Exp(x(0)) * (4 * x(0) * x(0) + 2 * x(1) * x(1) + 4 * x(0) * x(1) + 2 * x(1) + 1)
        nlp2.ObjectiveGradient =
                Function(x, y)
                    Dim exp As Double = Math.Exp(x(0))
                    y(0) = exp * (4 * x(0) * x(0) + 2 * x(1) * x(1) + 4 * x(0) * x(1) + 8 * x(0) + 6 * x(1) + 1)
                    y(1) = exp * (4 * x(0) + 4 * x(1) + 2)
                    Return y
                End Function

        ' Add constraint x0*x1 - x0 -x1 <= -1.5
        nlp2.AddNonlinearConstraint(Function(x) x(0) * x(1) - x(0) - x(1) + 1.5, ConstraintType.LessThanOrEqual, 0.0,
                Function(x, y)
                    y(0) = x(1) - 1
                    y(1) = x(0) - 1
                    Return y
                End Function)

        ' Add constraint x0*x1 >= -10
        ' If the gradient is omitted, it is approximated using divided differences.
        nlp2.AddNonlinearConstraint(Function(x) x(0) * x(1), ConstraintType.GreaterThanOrEqual, -10.0)

        nlp2.InitialGuess = Vector.Create(-1.0, 1.0)

        solution = nlp2.Solve()
        Console.WriteLine($"Solution: {solution:F6}")
        Console.WriteLine($"Optimal value:   {nlp2.OptimalValue:F6}")
        Console.WriteLine($"# iterations: {nlp2.SolutionReport.IterationsNeeded}")

        ' We can also use automatic differentiation to compute the gradients.
        ' This makes our life a lot simpler:

        Dim nlp3 As New NonlinearProgram(2)

        nlp3.SymbolicObjectiveFunction = Function(x) Math.Exp(x(0)) * (4 * x(0) * x(0) + 2 * x(1) * x(1) + 4 * x(0) * x(1) + 2 * x(1) + 1)

        ' Add constraint x0*x1 - x0 -x1 <= -1.5
        nlp3.AddSymbolicConstraint(Function(x) x(0) * x(1) - x(0) - x(1) + 1.5, ConstraintType.LessThanOrEqual, 0.0)

        ' Add constraint x0*x1 >= -10
        nlp3.AddSymbolicConstraint(Function(x) x(0) * x(1), ConstraintType.GreaterThanOrEqual, -10.0)

        nlp3.InitialGuess = Vector.Create(-1.0, 1.0)

        solution = nlp3.Solve()
        Console.WriteLine($"Solution: {solution:F6}")
        Console.WriteLine($"Optimal value:   {nlp3.OptimalValue:F6}")
        Console.WriteLine($"# iterations: {nlp3.SolutionReport.IterationsNeeded}")

    End Sub

End Module
