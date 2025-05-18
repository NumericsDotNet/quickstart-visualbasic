'=====================================================================
'
'  File: linear-equations.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The DenseMatrix and LUDecomposition classes reside in the
' Numerics.NET.LinearAlgebra namespace.
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra

    ' Illustrates solving systems of simultaneous linear
    ' equations using the DenseMatrix and LUDecomposition classes
    ' in the Numerics.NET.LinearAlgebra namespace of Numerics.NET.
    Module LinearEquations

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' A system of simultaneous linear equations is
        ' defined by a square matrix A and a right-hand
        ' side B, which can be a vector or a matrix.
        '
        ' You can use any matrix type for the matrix A.
        ' The optimal algorithm is automatically selected.

        ' Let's start with a general matrix:
        Dim m = Matrix.CreateFromArray(4, 4, New Double() _
                {1, 1, 1, 1,
                 1, 2, 3, 4,
                 1, 4, 9, 16,
                 1, 2, 1, 2}, MatrixElementOrder.ColumnMajor)
        Dim b1 = Vector.Create(New Double() {1, 3, 6, 3})
        Dim b2 = Matrix.CreateFromArray(4, 2, New Double() _
                {1, 3, 6, 3,
                 2, 3, 5, 8}, MatrixElementOrder.ColumnMajor)
        Console.WriteLine($"m = {m:F4}")

        '
        ' The Solve method
        '

        ' The following solves m x = b1. The second
        ' parameter specifies whether to overwrite the
        ' right-hand side with the result.
        Dim x1 = m.Solve(b1, False)
        Console.WriteLine($"x1 = {x1:F4}")
        ' If the overwrite parameter is omitted, the
        ' right-hand-side is overwritten with the solution:
        m.Solve(b1)
        Console.WriteLine($"b1 = {b1:F4}")
        ' You can solve for multiple right hand side
        ' vectors by passing them in a DenseMatrix:
        Dim x2 = m.Solve(b2, False)
        Console.WriteLine($"x2 = {x2:F4}")

        '
        ' Related Methods
        '

        ' You can verify whether a matrix is singular
        ' using the IsSingular method:
        Console.WriteLine("IsSingular(m) = {0:F4}",
                m.IsSingular())
        ' The inverse matrix is returned by the GetInverse
        ' method:
        Console.WriteLine($"GetInverse(m) = {m.GetInverse():F4}")
        ' The determinant is also available:
        Console.WriteLine($"Det(m) = {m.GetDeterminant():F4}")
        ' The condition number is an estimate for the
        ' loss of precision in solving the equations
        Console.WriteLine($"Cond(m) = {m.EstimateConditionNumber():F4}")
        Console.WriteLine()

        '
        ' The LUDecomposition class
        '

        ' If multiple operations need to be performed
        ' on the same matrix, it is more efficient to use
        ' the LUDecomposition class. This class does the
        ' bulk of the calculations only once.
        Console.WriteLine("Using LU Decomposition:")
        ' The constructor takes an optional second argument
        ' indicating whether to overwrite the original
        ' matrix with its decomposition:
        Dim lu = m.GetLUDecomposition(False)
        ' All methods mentioned earlier are still available:
        x2 = lu.Solve(b2, False)
        Console.WriteLine($"x2 = {x2:F4}")
        Console.WriteLine("IsSingular(m) = {0:F4}",
                lu.IsSingular())
        Console.WriteLine($"GetInverse(m) = {lu.GetInverse():F4}")
        Console.WriteLine($"Det(m) = {lu.GetDeterminant():F4}")
        Console.WriteLine($"Cond(m) = {lu.EstimateConditionNumber():F4}")
        ' In addition, you have access to the
        ' components, L and U of the decomposition.
        ' L is lower unit-triangular:
        Console.WriteLine($"  L = {lu.LowerTriangularFactor:F4}")
        ' U is upper triangular:
        Console.WriteLine($"  U = {lu.UpperTriangularFactor:F4}")

    End Sub

End Module
