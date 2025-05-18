'=====================================================================
'
'  File: basic-matrices.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The DenseMatrix class resides in the Numerics.NET.LinearAlgebra namespace.
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra

' Illustrates the use of the DenseMatrix class in the
' Numerics.NET.LinearAlgebra namespace of Numerics.NET.
Module BasicMatrices

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Constructing matrices
        '

        ' Option #1: specify number of rows and columns.
        ' The following constructs a matrix with 3 rows
        ' and 5 columns:
        Dim m1 = Matrix.Create(Of Double)(3, 5)
        Console.WriteLine($"m1 = {m1:F4}")
        ' Option #2: specify a rank 2 Double array.
        ' By default, elements are taken in column-major
        ' order. Therefore, the following creates a matrix
        ' with 3 rows and 4 columns:
        Dim m2 = Matrix.Create(New Double(,) _
             {
              {1, 2, 3},
              {2, 3, 4},
              {3, 4, 5},
              {4, 5, 6}
             })
        Console.WriteLine($"m2 = {m2:F4}")
        ' Option #3: Specify component array, and number
        ' of rows and columns. The elements are listed
        ' in column-major order. The following matrix
        ' is identical to m2:
        Dim elements As Double() = New Double() _
                {1, 2, 3, 2, 3, 4, 3, 4, 5, 4, 5, 6}
        Dim m3 = Matrix.CreateFromArray(3, 4, elements, MatrixElementOrder.ColumnMajor)
        Console.WriteLine($"m3 = {m3:F4}")
        ' Option #4: same as above, but specify element
        ' order. The following matrix is identical to m4:
        Dim m4 = Matrix.CreateFromArray(4, 3,
                elements, MatrixElementOrder.RowMajor)
        Console.WriteLine($"m4 = {m4:F4}")
        ' Option #5: same as #3, but specify whether to copy
        ' the matrix components, or use the specified array
        ' as internal storage.
        Dim m5 = Matrix.CreateFromArray(3, 4, elements, MatrixElementOrder.ColumnMajor, True)
        ' Option #6: same as #5, but specify whether to copy
        ' the matrix components, or use the specified array
        ' as internal storage.
        Dim m6 = Matrix.CreateFromArray(4, 3,
                elements, MatrixElementOrder.RowMajor, True)
        ' In addition, you can also create an identity
        ' matrix by calling the static GetIdentity method.
        ' The following constructs a 4x4 identity matrix:
        Dim m7 = DenseMatrix(Of Double).GetIdentity(4)
        Console.WriteLine($"m7 = {m7:F4}")

        '
        ' DenseMatrix properties
        '

        ' The RowCount and ColumnCount properties give the
        ' number of rows and columns, respectively:
        Console.WriteLine($"m1.RowCount = {m1.RowCount}")
        Console.WriteLine($"m1.ColumnCount = {m1.ColumnCount}")
        ' The GetComponents method returns a one-dimensional
        ' Double array that contains the components of the
        ' vector. By default, elements are returned in
        ' column major order. This is always a copy:
        elements = m3.ToArray()
        Console.WriteLine("Components:")
        Console.WriteLine($"components(3) = {elements(3)}")
        elements(3) = 1
        Console.WriteLine("m3(0,1) = {0}", m3(0, 1))
        ' The GetComponents method is overloaded, so you can
        ' choose whether you want the elements in row major
        ' or in column major order. The order parameter is
        ' of type MatrixElementOrder:
        elements = m3.ToArray(MatrixElementOrder.RowMajor)
        Console.WriteLine("In row major order:")
        Console.WriteLine($"components(3) = {elements(3)}")

        '
        ' Accessing matrix elements
        '

        ' The DenseMatrix class defines an indexer property
        ' that takes zero-based row and column indices.
        Console.WriteLine("Assigning with private storage:")
        Console.WriteLine("m1(0,2) = {0}", m1(0, 2))
        ' You can assign to this property:
        m1(0, 2) = 7
        Console.WriteLine("m1(0,2) = {0}", m1(0, 2))

        ' The matrices m4 and m5 had the copy parameter in
        ' the constructor set to True. As a result, they
        ' share their component storage. Changing one vector
        ' also changes the other:
        Console.WriteLine("Assigning with shared storage:")
        Console.WriteLine("m4(0,0) = {0}", m7(0, 0))
        m5(0, 0) = 3
        Console.WriteLine("m4(0,0) = {0}", m7(0, 0))

        '
        ' Copying and cloning matrices
        '

        ' A shallow copy of a matrix constructs a matrix
        ' that shares the component storage with the original.
        ' This is done using the ShallowCopy method. Note
        ' that we have to cast the return value since it is
        ' of type MatrixBase, the abstract base type of all
        ' the matrix classes:
        Console.WriteLine("Shallow copy vs. clone:")
        Dim m10 = CType(m2.ShallowCopy(), DenseMatrix(Of Double))
        ' The clone method creates a full copy.
        Dim m11 = m2.Clone()
        ' When we change m2, m10 changes, but m11 is left
        ' unchanged:
        Console.WriteLine("m2(1,1) = {0}", m2(1, 1))
        m2(1, 1) = -2
        Console.WriteLine("m10(1,1) = {0}", m10(1, 1))
        Console.WriteLine("m11(1,1) = {0}", m11(1, 1))
        ' We can give a matrix its own component storage
        ' by calling the CloneData method:
        Console.WriteLine("CloneData:")
        m11.CloneData()
        ' Now, changing the original v2 no longer changes v7:
        m2(1, 1) = 4
        Console.WriteLine("m11(1,1) = {0}", m11(1, 1))

    End Sub

End Module
