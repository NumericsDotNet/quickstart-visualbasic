'=====================================================================
'
'  File: matrix-vector-operations.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The DenseMatrix and DoubleVector classes resides in the
' Numerics.NET.LinearAlgebra namespace.
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra

' Illustrates operations on DenseMatrix objects and combined
' operations on DoubleVector and DenseMatrix objects from the
' Numerics.NET.LinearAlgebra namespace of Numerics.NET.
Module MatrixVectorOperations

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' For details on the basic workings of Vector
        ' objects, including constructing, copying and
        ' cloning vectors, see the BasicVectors QuickStart
        ' Sample.
        '
        ' For details on the basic workings of DenseMatrix
        ' objects, including constructing, copying and
        ' cloning vectors, see the BasicVectors QuickStart
        ' Sample.
        '
        ' Let's create some vectors to work with.
        Dim v1 = Vector.Create(Of Double)(1, 2, 3, 4, 5)
        Dim v2 = Vector.Create(Of Double)(1, -2, 3, -4, 5)
        Console.WriteLine($"v1 = {v1:F4}")
        Console.WriteLine($"v2 = {v2:F4}")
        ' These will hold results.
        Dim v As Vector(Of Double)

        ' Also, here are a couple of matrices.
        ' We start out with a 5x5 identity matrix:
        Dim m1 = DenseMatrix(Of Double).GetIdentity(5)
        ' Now we use the GetDiagonal method and combine it
        ' with the SetValue method of the Vector class to
        ' set some of the off-diagonal elements:
        m1.GetDiagonal(1).SetValue(2)
        m1.GetDiagonal(2).SetValue(3)
        m1.GetDiagonal(-1).SetValue(4)
        Console.WriteLine($"m1 = {m1:F4}")
        ' We define our second matrix by hand:
        Dim m2 = Matrix.CreateFromArray(5, 5, New Double() _
                {
                1, 2, 3, 4, 5,
                1, 3, 5, 7, 9,
                1, 4, 9, 16, 25,
                1, 8, 27, 64, 125,
                1, -1, 1, -1, 1
                }, MatrixElementOrder.ColumnMajor)
        Console.WriteLine($"m2 = {m2:F4}")
        ' This one holds the results:
        Dim m As DenseMatrix(Of Double)
        Console.WriteLine()

        '
        ' Matrix arithmetic
        '

        ' The DenseMatrix class defines Shared methods for
        ' addition, subtraction, and multiplication of
        ' matrices. Overloaded operators are available
        ' in languages that support them.
        ' Addition:
        Console.WriteLine("Matrix arithmetic:")
        m = Matrix.Add(m1, m2)
        Console.WriteLine($"m1 + m2 = {m:F4}")
        ' Subtraction:
        m = Matrix.Subtract(m1, m2)
        Console.WriteLine($"m1 - m2 = {m:F4}")
        ' Multiplication is the true matrix product:
        m = Matrix.Multiply(m1, m2)
        Console.WriteLine($"m1 * m2 = {m:F4}")
        Console.WriteLine()

        '
        ' Matrix-Vector products
        '

        ' The DenseMatrix class defines Shared Add, Subtract, and
        ' Multiply methods for vectors and matrices.
        ' Overloaded operators are available in languages
        ' that support them.
        Console.WriteLine("Matrix-vector products:")
        v = Matrix.Multiply(m1, v1)
        Console.WriteLine($"m1 v1 = {v:F4}")
        ' You can also multiply a vector by a matrix on the right.
        ' This is equivalent to multiplying on the left by the
        ' transpose of the matrix:
        v = Matrix.Multiply(m1, TransposeOperation.Transpose, v1)
        Console.WriteLine($"v1 m1 = {v:F4}")

        ' Now for some methods of the DoubleVector class that
        ' involve matrices:
        ' Add a product of a matrix and a vector:
        v.AddProductInPlace(m1, v1)
        Console.WriteLine($"v + m1 v1 = {v:F4}")
        ' Or add a scaled product. Use a negative factor
        ' to subtract:
        v.AddScaledProductInPlace(-2, m1, v2)
        Console.WriteLine($"v - 2 m1 v2 = {v:F4}")
        ' You can also use static Subtract methods:
        v.SubtractProductInPlace(m1, v1)
        Console.WriteLine($"v - m1 v1 = {v:F4}")
        Console.WriteLine()

        '
        ' Matrix norms
        '
        Console.WriteLine("Matrix norms")
        ' Matrix norms are not as easily defined as
        ' vector norms. Three matrix norms are available.
        ' 1. The one-norm through the OneNorm property:
        Dim a As Double = m2.OneNorm()
        Console.WriteLine($"OneNorm of m2 = {a}")
        ' 2. The infinity norm through the
        '    InfinityNorm property:
        a = m2.InfinityNorm()
        Console.WriteLine($"InfinityNorm of m2 = {a}")
        ' 3. The Frobenius norm is often used because it
        '    is easy to calculate.
        a = m2.FrobeniusNorm()
        Console.WriteLine($"FrobeniusNorm of m2 = {a}")
        Console.WriteLine()

        ' The trace of a matrix is the sum of its diagonal
        ' elements. It is returned by the Trace property:
        a = m2.Trace
        Console.WriteLine($"Trace(m2) = {a}")

        ' The Transpose method returns the transpose of a
        ' matrix. This transposed matrix shares element storage
        ' with the original matrix. Use the CloneData method
        ' to give the transpose its own data storage.
        m = m2.Transpose()
        Console.WriteLine($"Transpose(m2) = {m:F4}")

    End Sub

End Module
