'=====================================================================
'
'  File: linear-programming.vb
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

' Illustrates solving linear programming problems
' using the classes in the Numerics.NET.Optimization
' namespace of Numerics.NET.
Module LinearProgramming

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart illustrates the three ways to create a Linear Program.

        ' The first is in terms of matrices. The coefficients
        ' are supplied as a matrix. The cost vector, right-hand side
        ' and constraints on the variables are supplied as a vector.

        ' The cost vector:
        Dim c = Vector.Create(-1.0, -3.0, 0.0, 0.0, 0.0, 0.0)
        ' The coefficients of the constraints:
        Dim A = Matrix.CreateFromArray(4, 6, New Double() _
            {
                1, 1, 1, 0, 0, 0,
                1, 1, 0, -1, 0, 0,
                1, 0, 0, 0, 1, 0,
                0, 1, 0, 0, 0, 1
            }, MatrixElementOrder.RowMajor)
        ' The right-hand sides of the constraints:
        Dim b = Vector.Create(1.5, 0.5, 1.0, 1.0)

        ' We're now ready to call the constructor.
        ' The last parameter specifies the number of equality
        ' constraints.
        Dim lp1 As New LinearProgram(c, A, b, 4)

        ' Now we can call the Solve method to run the Revised
        ' Simplex algorithm:
        Dim x = lp1.Solve()
        ' The GetDualSolution method returns the dual solution:
        Dim y = lp1.GetDualSolution()
        Console.WriteLine($"Primal: {x:F1}")
        Console.WriteLine($"Dual:   {y:F1}")
        ' The optimal value is returned by the Extremum property:
        Console.WriteLine($"Optimal value:   {lp1.OptimalValue:F1}")

        ' The second way to create a Linear Program is by constructing
        ' it by hand. We start with an 'empty' linear program.
        Dim lp2 As New LinearProgram()

        ' Next, we add two variables: we specify the name, the cost,
        ' and optionally the lower and upper bound.
        lp2.AddVariable("X1", -1.0, 0, 1)
        lp2.AddVariable("X2", -3.0, 0, 1)

        ' Next, we add constraints. Constraints also have a name.
        ' We also specify the coefficients of the variables,
        ' the lower bound and the upper bound.
        lp2.AddLinearConstraint("C1", Vector.Create(1.0, 1.0), 0.5, 1.5)
        ' If a constraint is a simple equality or inequality constraint,
        ' you can supply a LinearProgramConstraintType value and the
        ' right-hand side of the constraint.

        ' We can now solve the linear program:
        x = lp2.Solve()
        y = lp2.GetDualSolution()
        Console.WriteLine($"Primal: {x:F1}")
        Console.WriteLine($"Dual:   {y:F1}")
        Console.WriteLine($"Optimal value:   {lp2.OptimalValue:F1}")

        ' Finally, we can create a linear program from an MPS file.
        ' The MPS format is a standard format.
        Dim lp3 = MpsReader.Read("..\..\..\..\..\data\sample.mps")
        ' We can go straight to solving the linear program:
        x = lp3.Solve()
        y = lp3.GetDualSolution()
        Console.WriteLine($"Primal: {x:F1}")
        Console.WriteLine($"Dual:   {y:F1}")
        Console.WriteLine($"Optimal value:   {lp3.OptimalValue:F1}")

    End Sub

End Module
