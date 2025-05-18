'=====================================================================
'
'  File: band-matrices.vb
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
' The BandMatrix class resides in the Numerics.NET.LinearAlgebra
' namespace.
Imports Numerics.NET.LinearAlgebra

Module BandMatrices

    ' Illustrates the use of the BandMatrix class in the
    ' Numerics.NET.LinearAlgebra namespace of Numerics.NET.
    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Band matrices are matrices whose elements
        ' are nonzero only in a diagonal band around
        ' the main diagonal.
        '
        ' General band matrices, upper and lower band
        ' matrices, and symmetric band matrices are all
        ' represented by a single class: BandMatrix.

        '
        ' Constructing band matrices
        '

        ' Constructing band matrices is similar to
        ' constructing general matrices. See the
        ' BasicMatrices QuickStart samples for a more
        ' complete discussion.

        ' The following creates a 7x5 band matrix with
        ' upper bandwidth 1 and lower bandwidth 2:
        Dim b1 = Matrix.CreateBanded(Of Double)(7, 5, 2, 1)

        ' Once the upper and lower bandwidth are set,
        ' it cannot be changed. Elements that are outside
        ' the band cannot be set.

        ' A second constructor lets you create upper
        ' or lower band matrices. The following constructs
        ' an 11x11 upper band matrix with unit diagonal
        ' and three non-zero upper diagonals.
        Dim b2 = Matrix.CreateUpperBanded(Of Double)(11, 11, 3, MatrixDiagonal.UnitDiagonal)

        ' To create a symmetric band matrix, you only need
        ' the size and the bandwidth. The following creates
        ' a 6x6 symmetric tri-diagonal matrix:
        Dim b3 = Matrix.CreateSymmetricBanded(Of Double)(7, 1)

        ' We can assign values to the components by using
        ' the GetDiagonal method.
        b3.GetDiagonal(0).SetValue(2)
        b3.GetDiagonal(1).SetValue(-1)

        ' Extracting band matrices

        ' Another way to construct a band matrix is by
        ' extracting them from an existing matrix.
        Dim m = Matrix.CreateFromArray(3, 4, New Double() _
                {
                 1, 2, 3,
                 2, 3, 4,
                 3, 4, 5,
                 4, 5, 7
                }, MatrixElementOrder.ColumnMajor)
        ' To get the lower band part of m with bandwidth 2:
        Dim b4 = BandMatrix(Of Double).Extract(m, 2, 0)

        '
        ' BandMatrix properties
        '

        ' A number of properties are available to determine
        ' whether a BandMatrix has a special structure:
        Console.WriteLine($"b2 is upper? {b2.IsUpperTriangular}")
        Console.WriteLine($"b2 is lower? {b2.IsUpperTriangular}")
        Console.WriteLine($"b2 is unit diagonal? {b2.IsUnitDiagonal}")
        Console.WriteLine($"b2 is symmetrical? {b2.IsSymmetrical}")

        '
        ' BandMatrix methods
        '

        ' You can get and set matrix elements:
        b3(2, 3) = 55
        Console.WriteLine("b3(2, 3) = {0:F0}", b3(2, 3))
        ' And the change will automatically be reflected
        ' in the symmetric element:
        Console.WriteLine("b3(3, 2) = {0:F0}", b3(3, 2))

        '
        ' Row and column views
        '

        ' The GetRow and GetColumn methods are
        ' available.
        Dim row = b2.GetRow(1)
        Console.WriteLine($"row 1 of b2 = {row:F0}")
        Dim column = b2.GetColumn(2, 3, 4)
        Console.WriteLine("column 3 of b2 from row 4 to ")
        Console.WriteLine($"  row 5 = {column:F0}")

        '
        ' Band matrix decompositions
        '

        ' Specialized classes exist to represent the
        ' LU decomposition of a general band matrix
        ' and the Cholesky decomposition of a
        ' symmetric band matrix.

        ' Because of pivoting, the upper band matrix of
        ' the LU decomposition has larger bandwidth.
        ' You need to allocate extra space to be able to
        ' overwrite a matrix with its LU decomposition.

        ' The following creates a 7x5 band matrix with
        ' upper bandwidth 1 and lower bandwidth 2.
        Dim b5 = Matrix.CreateBanded(Of Double)(7, 7, 2, 1, True)
        b5.GetDiagonal(0).SetValue(2.0)
        b5.GetDiagonal(-2).SetValue(-1.0)
        b5.GetDiagonal(1).SetValue(-1.0)

        ' Other than that, the API is the same as
        ' other decomposition classes.
        Dim blu As LUDecomposition(Of Double) = b5.GetLUDecomposition(True)
        Dim solution = blu.Solve(Vector.CreateConstant(b5.ColumnCount, 1.0))
        Console.WriteLine($"Solution of b5*x = ones: {solution:F4}")

    End Sub

End Module
