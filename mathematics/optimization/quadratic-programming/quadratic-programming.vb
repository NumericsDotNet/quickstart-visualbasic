'=====================================================================
'
'  File: quadratic-programming.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The quadratic programming classes reside in their own namespace.
Imports Numerics.NET.Optimization
Imports Numerics.NET

    ' Illustrates solving quadratic programming problems
    ' using the classes in the Numerics.NET.Optimization
    ' namespace of Numerics.NET.
    Module QuadraticProgramming

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart illustrates the quadratic programming
        ' functionality by solving a portfolio optimization problem.

        ' Portfolio optimization is a common application of QP.
        ' For a collection of assets, the goal is to minimize
        ' the risk (variance of the return) while achieving
        ' a minimal return for a set maximum amount invested.

        ' The variables are the amounts invested in each asset.
        ' The quadratic term is the covariance matrix of the assets.
        ' THere is no linear term in this case.

        ' There are three ways to create a Quadratic Program.

        ' The first is in terms of matrices. The coefficients
        ' of the constraints and the quadratic terms are supplied
        ' as matrices. The cost vector, right-hand side and
        ' constraints on the variables are supplied as vectors.

        ' The linear term in the objective function:
        Dim c = Vector.CreateConstant(4, 0.0)
        ' The quaratic term in the objective function:
        Dim R = Matrix.CreateSymmetric(4,
                New Double() _
                {
                     0.08, -0.05, -0.05, -0.05,
                    -0.05, 0.16, -0.02, -0.02,
                    -0.05, -0.02, 0.35, 0.06,
                    -0.05, -0.02, 0.06, 0.35
                }, MatrixTriangle.Upper, MatrixElementOrder.ColumnMajor)
        ' The coefficients of the constraints:
        Dim A = Matrix.CreateFromArray(2, 4, New Double() _
            {
                1, 1, 1, 1,
                -0.05, 0.2, -0.15, -0.3
            }, MatrixElementOrder.RowMajor)
        ' The right-hand sides of the constraints:
        Dim b = Vector.Create(10000.0, -1000.0)

        ' We're now ready to call the constructor.
        ' The last parameter specifies the number of equality
        ' constraints.
        Dim qp1 As New QuadraticProgram(c, R, A, b, 0)

        ' Now we can call the Solve method to run the Revised
        ' Simplex algorithm:
        Dim x = qp1.Solve()
        Console.WriteLine($"Solution: {x:F1}")
        ' The optimal value is returned by the Extremum property:
        Console.WriteLine($"Optimal value:   {qp1.OptimalValue:F1}")

        ' The second way to create a Quadratic Program is by constructing
        ' it by hand. We start with an 'empty' quadratic program.
        Dim qp2 As New QuadraticProgram()

        ' Next, we add two variables: we specify the name, the cost,
        ' and optionally the lower and upper bound.
        qp2.AddVariable("X1", 0.0)
        qp2.AddVariable("X2", 0.0)
        qp2.AddVariable("X3", 0.0)
        qp2.AddVariable("X4", 0.0)

        ' Next, we add constraints. Constraints also have a name.
        ' We also specify the coefficients of the variables,
        ' the lower bound and the upper bound.
        Dim c1Values = Vector.Create(1.0, 1.0, 1.0, 1.0)
        qp2.AddLinearConstraint("C1", c1Values, ConstraintType.LessThanOrEqual, 10000)
        Dim c2Values = Vector.Create(0.05, -0.2, 0.15, 0.3)
        qp2.AddLinearConstraint("C2", c2Values, ConstraintType.GreaterThanOrEqual, 1000)
        ' If a constraint is a simple equality or inequality constraint,
        ' you can supply a QuadraticProgramConstraintType value and the
        ' right-hand side of the constraint.

        ' Quadratic terms must be set individually.
        ' Each combination appears at most once.
        qp2.SetQuadraticCoefficient("X1", "X1", 0.08)
        qp2.SetQuadraticCoefficient("X1", "X2", -0.05 * 2)
        qp2.SetQuadraticCoefficient("X1", "X3", -0.05 * 2)
        qp2.SetQuadraticCoefficient("X1", "X4", -0.05 * 2)
        qp2.SetQuadraticCoefficient("X2", "X2", 0.16)
        qp2.SetQuadraticCoefficient("X2", "X3", -0.02 * 2)
        qp2.SetQuadraticCoefficient("X2", "X4", -0.02 * 2)
        qp2.SetQuadraticCoefficient("X3", "X3", 0.35)
        qp2.SetQuadraticCoefficient("X3", "X4", 0.06 * 2)
        qp2.SetQuadraticCoefficient("X4", "X4", 0.35)

        ' We can now solve the quadratic program:
        x = qp2.Solve()
        Console.WriteLine($"Solution: {x:F1}")
        Console.WriteLine($"Optimal value:   {qp2.OptimalValue:F1}")

        ' Finally, we can create a quadratic program from an MPS file.
        ' The MPS format is a standard format.
        Dim qp3 = MpsReader.ReadQuadraticProgram("..\..\..\..\..\data\portfolio.qps")
        ' We can go straight to solving the quadratic program:
        x = qp3.Solve()
        Console.WriteLine($"Solution: {x:F1}")
        Console.WriteLine($"Optimal value:   {qp3.OptimalValue:F1}")

    End Sub

End Module
