'=====================================================================
'
'  File: iterative-sparse-solvers.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET.Data.Text
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra
Imports Numerics.NET.LinearAlgebra.IterativeSolvers
Imports Numerics.NET.LinearAlgebra.IterativeSolvers.Preconditioners

' Illustrates the use of iterative sparse solvers for efficiently
' solving large, sparse systems of linear equations using the
' iterative sparse solver and preconditioner classes from the
' Numerics.NET.LinearAlgebra.IterativeSolvers namespace of Numerics.NET.
Module IterativeSparseSolvers

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample illustrates how to solve sparse linear systems
        ' using iterative solvers.

        ' IterativeSparseSolver is the base class for all
        ' iterative solver classes:
        Dim solver As IterativeSparseSolver(Of Double)

        '
        ' Non-symmetric systems
        '

        Console.WriteLine("Non-symmetric systems")

        ' We load a sparse matrix and right-hand side from a data file:
        Dim A = CType(MatrixMarketFile.ReadMatrix(Of Double)(
                "..\..\..\..\..\data\sherman3.mtx"), SparseMatrix(Of Double))
        Dim b = MatrixMarketFile.ReadVector(Of Double)(
                "..\..\..\..\..\data\sherman3_rhs1.mtx")

        Console.WriteLine("Solve Ax = b")
        Console.WriteLine("A is {0}x{1} with {2} nonzeros.", A.RowCount, A.ColumnCount, A.NonzeroCount)

        ' Some solvers are suitable for symmetric matrices only.
        ' Our matrix is not symmetric, so we need a solver that
        ' can handle this:

        ' #Region DOCIterativeSparseSolvers1
        solver = New BiConjugateGradientSolver(Of Double)(A)

        solver.Solve(b)
        Console.WriteLine($"Solved in {solver.IterationsNeeded} iterations.")
        Console.WriteLine($"Estimated error: {solver.SolutionReport.Error}")
        ' #End Region

        ' Using a preconditioner can improve convergence. You can use
        ' one of the predefined preconditioners, or supply your own.

        ' With incomplete LU preconditioner
        solver.Preconditioner = New IncompleteLUPreconditioner(Of Double)(A)
        solver.Solve(b)
        Console.WriteLine($"Solved in {solver.IterationsNeeded} iterations.")
        Console.WriteLine($"Estimated error: {solver.EstimatedError}")

        '
        ' Symmetrical systems
        '

        Console.WriteLine("Symmetric systems")

        ' In this example we solve the Laplace equation on a rectangular grid
        ' with Dirichlet boundary conditions.

        ' We create 100 divisions in each direction, giving us 99 interior points
        ' in each direction:
        Const nx = 99
        Const ny = 99

        ' The boundary conditions are just some arbitrary functions.
        Dim left = Vector.CreateFromFunction(ny, Function(i) (i / (nx + 1.0)) ^ 2)
        Dim right = Vector.CreateFromFunction(ny, Function(i) 1 - (i / (nx + 1.0)))
        Dim top = Vector.CreateFromFunction(nx, Function(i) Elementary.SinPi(5 * (i / (nx + 1.0))))
        Dim bottom = Vector.CreateFromFunction(nx, Function(i) Elementary.CosPi(5 * (i / (nx + 1.0))))

        ' We discretize the Laplace operator using the 5 point stencil.
        Dim laplacian = Matrix.CreateSparse(Of Double)(nx * ny, nx * ny, 5 * nx * ny)
        Dim rhs = Vector.Create(Of Double)(nx * ny)
        For j As Integer = 0 To ny - 1
            For i As Integer = 0 To nx - 1
                Dim ix As Integer = j * nx + i
                If (j > 0) Then laplacian(ix, ix - nx) = 0.25
                If (i > 0) Then laplacian(ix, ix - 1) = 0.25
                laplacian(ix, ix) = -1.0
                If (i + 1 < nx) Then laplacian(ix, ix + 1) = 0.25
                If (j + 1 < ny) Then laplacian(ix, ix + nx) = 0.25
            Next
        Next
        ' We build up the right-hand sides using the boundary conditions:
        For i As Integer = 0 To nx - 1
            rhs(i) = -0.25 * top(i)
            rhs(nx * (ny - 1) + i) = -0.25 * bottom(i)
        Next
        For j As Integer = 0 To ny - 1
            rhs(j * nx) -= 0.25 * left(j)
            rhs(j * nx + nx - 1) -= 0.25 * right(j)
        Next

        Console.WriteLine("A is {0}x{1} with {2} nonzeros.", laplacian.RowCount, laplacian.ColumnCount, laplacian.NonzeroCount)

        ' Finally, we create an iterative solver suitable for
        ' symmetric systems...
        solver = New QuasiMinimalResidualSolver(Of Double)(laplacian)
        ' and solve using the right-hand side we just calculated:
        solver.Solve(rhs)

        Console.WriteLine($"Solved in {solver.IterationsNeeded} iterations.")
        Console.WriteLine($"Estimated error: {solver.EstimatedError}")

    End Sub

End Module
