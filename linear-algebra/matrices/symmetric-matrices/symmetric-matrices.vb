'=====================================================================
'
'  File: symmetric-matrices.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The SymmetricMatrix class resides in the Numerics.NET.LinearAlgebra
' namespace.
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra

    ' Illustrates the use of the SymmetricMatrix class in the
    ' Numerics.NET.LinearAlgebra namespace of Numerics.NET.
    Module SymmetricMatrices

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Symmetric matrices are matrices whose elements
        ' are symmetrical around the main diagonal.
        ' Symmetric matrices are always square, and are
        ' equal to their own transpose.

        '
        ' Constructing symmetric matrices
        '

        ' Constructing symmetric matrices is similar to
        ' constructing general matrices. See the
        ' BasicMatrices QuickStart samples for a more
        ' complete discussion.

        ' Symmetric matrices are always square. You don't
        ' have to specify both the number of rows and the
        ' number of columns.
        '
        ' The following creates a 5x5 symmetric matrix:
        Dim s1 As SymmetricMatrix(Of Double) = Matrix.CreateSymmetric(Of Double)(5)
        ' Symmetric matrices access and modify only the
        ' elements on and either above or below the
        ' main diagonal. When initializing a
        ' symmetric matrix in a constructor, you must
        ' specify a triangleMode parameter that specifies
        ' whether to use the upper or lower triangle:
        Dim components As Double() = New Double() _
               {11, 12, 13, 14, 15,
                21, 22, 23, 24, 25,
                31, 32, 33, 34, 35,
                41, 42, 43, 44, 45,
                51, 52, 53, 54, 55}
        Dim s2 As SymmetricMatrix(Of Double) = Matrix.CreateSymmetric(
                5, components, MatrixTriangle.Upper, MatrixElementOrder.ColumnMajor)
        Console.WriteLine($"s2 = {s2:F0}")

        ' You can also create a symmetric matrix by
        ' multiplying any matrix by its transpose:
        Dim m = Matrix.CreateFromArray(3, 4, New Double() _
             {1, 2, 3,
              2, 3, 4,
              3, 4, 5,
              4, 5, 6}, MatrixElementOrder.ColumnMajor)
        Console.WriteLine($"m = {m:F0}")
        ' This calculates transpose(m) times m:
        Dim s3 As SymmetricMatrix(Of Double) =
                SymmetricMatrix(Of Double).FromOuterProduct(m)
        Console.WriteLine($"s3 = {s3:F0}")
        ' An optional 'side' parameter lets you specify
        ' whether the left or right operand of the
        ' multiplication is the transposed matrix.
        ' This calculates m times transpose(m):
        Dim s4 As SymmetricMatrix(Of Double) =
                SymmetricMatrix(Of Double).FromOuterProduct(m,
                    MatrixOperationSide.Right)
        Console.WriteLine($"s4 = {s4:F0}")

        '
        ' SymmetricMatrix methods
        '

        ' The GetEigenvalues method returns a vector
        ' containing the eigenvalues.
        Dim l = s4.GetEigenvalues()
        Console.WriteLine($"Eigenvalues: {l:F4}")

        ' The ApplyMatrixFunction calculates a function
        ' of the entire matrix. For example, to calculate
        ' the 'sine' of a matrix:
        Dim sinS As SymmetricMatrix(Of Double) = s4.ApplyMatrixFunction(AddressOf Math.Sin)
        Console.WriteLine($"sin(s4): {sinS:F4}")

        ' Symmetric matrices don't have any specific
        ' properties.

        ' Symmetric matrices don't have any specific
        ' properties.

        ' You can get and set matrix elements:
        s1(1, 3) = 55
        Console.WriteLine("s1(1, 3) = {0}", s1(1, 3))
        ' And the change will automatically be reflected
        ' in the symmetric element:
        Console.WriteLine("s1(3, 1) = {0}", s1(3, 1))

        '
        ' Row and column views
        '

        ' The GetRow and GetColumn methods are
        ' available.
        Dim row = s2.GetRow(1)
        Console.WriteLine($"row 1 of s2 = {row}")
        Dim column = s2.GetColumn(1, 2, 3)
        Console.WriteLine("2nd column of s2 from row 3 to ")
        Console.WriteLine($"  row 4 = {column}")

    End Sub

End Module
