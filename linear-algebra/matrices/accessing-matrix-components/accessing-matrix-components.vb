'=====================================================================
'
'  File: accessing-matrix-components.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET
' Matrix and Vector classes reside in the
' Numerics.NET.LinearAlgebra namespace.
Imports Numerics.NET.LinearAlgebra

' Illustrates accessing matrix components and iterating
' through the rows and columns of a matrix. Matrix classes
' reside in the Numerics.NET.LinearAlgebra namespace
' of Numerics.NET.
Module AccessingMatrixComponents

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' We'll work with this matrix:
        Dim m = Matrix.CreateFromArray(2, 3, New Double() {1, 2, 3, 4, 5, 6},
            MatrixElementOrder.ColumnMajor)

        '
        ' Individual components
        '

        ' The Matrix class has an indexer property that takes two arguments:
        ' the row and column index. Both are zero based.
        Console.WriteLine("m(1,1) = {0:F4}", m(1, 1))

        '
        ' Rows and columns
        '

        ' Indexed range access

        ' The indexer property is overloaded to allow for direct indexed access
        ' to complete or partial rows or columns.

        Dim row1 = m(0, New Range(1, 2))
        ' This prints "(3, 5)":
        Console.WriteLine($"row1 = {row1:F4}")

        ' The special range Range.All lets you access an entire row
        ' or column without having to specify any details about the range.
        Dim row2 = m(1, Range.All)
        ' This prints "(2, 4, 6)":
        Console.WriteLine($"row1 = {row2:F4}")
        Dim column1 = m(Range.All, 0)
        ' This prints "(1, 2)":
        Console.WriteLine($"column1 = {column1:F4}")

        ' We can assign to rows and columns, too:
        m(Range.All, 0) = row1
        ' This prints "((3, 3, 5) (5, 4, 6)"
        Console.WriteLine($"m = {m:F4}")

        ' GetRow and GetColumn provide an alternate mechanism
        ' for achieving the same result.

        ' Passing just one parameter retrieves the specified row or column:
        row1 = m.GetRow(1)
        ' This prints "(2, 4, 6)":
        Console.WriteLine($"row1 = {row1:F4}")
        column1 = m.GetColumn(0)
        ' This prints "(1, 2)":
        Console.WriteLine($"column1 = {column1:F4}")

        ' You can also pass a start and end index:
        row2 = m.GetRow(0, 1, 2)
        ' This prints "(3, 5)":
        Console.WriteLine($"row2 = {row2:F4}")

        ' We can assign to rows and columns, too, using CopyTo:
        row2.CopyTo(m.GetColumn(0))
        ' This prints "((3, 3, 5) (5, 4, 6)"
        Console.WriteLine($"m = {m:F4}")

        ' Enumeration

        ' The Rows and Columns methods allow you to enumerate over
        ' the rows and columns of a matrix.

        ' For example: this calculates the sum of the absolute values
        ' of the components of the matrix m:
        Dim sum As Double = 0
        Dim column As Vector(Of Double)
        For Each column In m.Columns
            sum += column.OneNorm()
        Next

        '
        ' Accessing diagonals
        '

        ' Diagonals are retrieved using the GetDiagonal method:
        Dim mainDiagonal = m.GetDiagonal()
        ' An optional parameter specifies which diagonal:
        '   n < 0 means subdiagonal
        '   n > 0 means nth superdiagonal:
        Dim superDiagonal = m.GetDiagonal(1)

        '
        ' Accessing submatrices
        '

        ' Indexed range access

        ' A fourth overload of the indexer property lets you
        ' extract a part of a matrix. Both parameters are Range
        ' structures:
        Dim a = Matrix.Create(Of Double)(10, 10)
        ' Extract the 2nd to the 5th row of m:
        Dim a1 = a(New Range(1, 4), Range.All)
        ' Extract the odd columns:
        Dim a2 = a(Range.All, New Range(1, 9, 2))
        ' Extract the 4x4 leading submatrix of m:
        Dim a3 = a(New Range(0, 3), New Range(0, 3))

        ' You can also assign to submatrices:
        Dim identity5 = DenseMatrix(Of Double).GetIdentity(5)
        a(New Range(0, 4), New Range(5, 9)) = identity5
        a(New Range(5, 9), New Range(0, 4)) = identity5

        ' The same results can be achieved with the GetSubmatrix method.

        ' Extract the 2nd to the 5th row of m.
        ' Start and end columns are supplied manually.
        Dim a4 = a.GetSubmatrix(1, 4, 0, 9)
        ' Extract the odd columns:
        ' Here we need to supply the transpose parameter.
        Dim a5 = a.GetSubmatrix(0, 9, 1, 1, 9, 2,
             TransposeOperation.None)
        ' Extract the 4x4 leading submatrix of m.
        ' And let's get its transpose, just because we can.
        ' We need to specify the row and column stride:
        Dim a6 = a.GetSubmatrix(0, 3, 1, 0, 3, 1,
                TransposeOperation.Transpose)

        ' You can still assign to submatrices, using the
        ' CopyTo method:
        identity5.CopyTo(a.GetSubmatrix(0, 4, 5, 9))
        identity5.CopyTo(a.GetSubmatrix(5, 9, 0, 4))

    End Sub

End Module
