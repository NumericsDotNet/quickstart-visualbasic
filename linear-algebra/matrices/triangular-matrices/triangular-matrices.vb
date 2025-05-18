'=====================================================================
'
'  File: triangular-matrices.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The TriangularMatrix class resides in the Numerics.NET.LinearAlgebra
' namespace.
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra

    ' Illustrates the use of the TriangularMatrix class in the
    ' Numerics.NET.LinearAlgebra namespace of Numerics.NET.
    Module TriangularMatrices

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Triangular matrices are matrices whose elements
        ' above or below the diagonal are all zero. The
        ' former is called lower triangular, the latter
        ' lower triangular. In addition, triangular matrices
        ' can have all 1's on the diagonal.

        '
        ' Constructing triangular matrices
        '

        ' Constructing triangular matrices is similar to
        ' constructing general matrices. See the
        ' BasicMatrices QuickStart samples for a more
        ' complete discussion.
        '
        ' All constructors take a MatrixTriangle
        ' value as their first parameter. This indicates
        ' whether an upper or lower triangular matrix
        ' should be created. The following creates a
        ' 5x5 lower triangular matrix:
        Dim t1 = Matrix.CreateLowerTriangular(Of Double)(5, 5)
        ' You can also specify whether the diagonal
        ' consists of all 1's using a unitDiagonal parameter:
        Dim t2 = Matrix.CreateLowerTriangular(Of Double)(
                5, 5, MatrixDiagonal.UnitDiagonal)
        ' Triangular matrices access and modify only the
        ' elements that are non-zero. If the diagonal
        ' mode is UnitDiagonal, the diagonal elements
        ' are not used, since they are all equal to 1.
        Dim elements As Double() = New Double() _
            {
                11, 12, 13, 14, 15,
                21, 22, 23, 24, 25,
                31, 32, 33, 34, 35,
                41, 42, 43, 44, 45,
                51, 52, 53, 54, 55}
        ' The following creates a matrix using the
        ' upper triangular part of the above.
        Dim t3 = Matrix.CreateUpperTriangular(
                5, 5,
                elements, MatrixElementOrder.RowMajor)
        Console.WriteLine($"t3 = {t3:F4}")
        ' Same as above, but unit diagonal:
        Dim t4 = Matrix.CreateUpperTriangular(
                5, 5, elements,
                MatrixDiagonal.UnitDiagonal,
                MatrixElementOrder.RowMajor, True)
        Console.WriteLine($"t4 = {t4:F4}")

        '
        ' Extracting triangular matrices
        '
        ' You may want to use part of a dense matrix
        ' as a triangular matrix. The static
        ' ExtractUpperTriangleand ExtractLowerTriangle
        ' methods perform this task.
        Dim m = Matrix.CreateFromArray(5, 5, elements, MatrixElementOrder.ColumnMajor)
        Console.WriteLine($"m = {m:F4}")
        ' Both methods are overloaded. The simplest
        ' returns a triangular matrix of the same dimension:
        Dim t5 As TriangularMatrix(Of Double) =
                Matrix.ExtractLowerTriangle(m)
        Console.WriteLine($"t5 = {t5:F4}")
        ' You can also specify if the matrix is unit diagonal:
        Dim t6 As TriangularMatrix(Of Double) =
                Matrix.ExtractUpperTriangle(
                    m, MatrixDiagonal.UnitDiagonal)
        Console.WriteLine($"t6 = {t6:F4}")
        ' Or the dimensions of the matrix if they don't
        ' match the original:
        Dim t7 As TriangularMatrix(Of Double) =
                Matrix.ExtractUpperTriangle(
                    m, 3, 3, MatrixDiagonal.UnitDiagonal)
        Console.WriteLine($"t7 = {t7:F4}")
        Console.WriteLine()

        '
        ' TriangularMatrix properties
        '

        ' The IsLowerTriangular and IsUpperTriangular return
        ' a boolean value:
        Console.WriteLine("t4 is lower triangular? - {0}",
                t4.IsLowerTriangular)
        Console.WriteLine("t4 is upper triangular? - {0}",
                t4.IsUpperTriangular)
        ' The IsUnitDiagonal property indicates whether the
        ' matrix has all 1's on its diagonal:
        Console.WriteLine("t3 is unit diagonal? - {0}",
                t3.IsUnitDiagonal)
        Console.WriteLine("t4 is unit diagonal? - {0}",
                t4.IsUnitDiagonal)
        Console.WriteLine()
        ' You can get and set matrix elements:
        t3(1, 3) = 55
        Console.WriteLine("t3(1, 3) = {0}", t3(1, 3))
        ' But trying to set an element that is zero or
        ' is on the diagonal for a unit diagonal matrix
        ' causes an exception to be thrown:
        Try
            t3(3, 1) = 100
        Catch e As ComponentReadOnlyException
            Console.WriteLine("Error accessing element: {0:F4}",
                    e.Message)
        End Try

        '
        ' Row and column views
        '

        ' The GetRowView and GetColumnView methods are
        ' available.
        Dim row = t3.GetRow(1)
        Console.WriteLine($"row 1 of t3 = {row:F4}")
        Dim column = t4.GetColumn(2, 1, 3)
        Console.WriteLine("column 3 of t4 from row 2 to row 4:")
        Console.WriteLine($"  {column:F4}")

    End Sub

End Module
