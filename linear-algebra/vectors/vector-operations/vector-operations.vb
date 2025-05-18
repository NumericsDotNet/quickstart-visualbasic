'=====================================================================
'
'  File: vector-operations.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The Vector class resides in the Numerics.NET.LinearAlgebra namespace.
Imports Numerics.NET.LinearAlgebra
' The delegate classes reside in the Numerics.NET
' namespace.
Imports Numerics.NET

    ' Illustrates operations on Vector objects from the
    ' Numerics.NET.LinearAlgebra namespace of Numerics.NET.
    Module VectorOperations

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
        ' Let's create some vectors to work with.
        Dim v1 = Vector.Create(New Double() {1, 2, 3, 4, 5})
        Dim v2 = Vector.Create(New Double() {1, -2, 3, -4, 5})
        Dim v3 = Vector.Create(New Double() {3, 2, 1, 0, -1})
        ' This one will hold results.
        Dim v As Vector(Of Double)

        '
        ' Vector Arithmetic
        '
        ' The Vector class defines static methods for
        ' addition and subtraction:
        Console.WriteLine($"v1 = {v1:F4}")
        Console.WriteLine($"v2 = {v2:F4}")
        Console.WriteLine("Basic arithmetic:")
        v = Vector.Negate(v1)
        Console.WriteLine($"-v1 = {v:F4}")
        v = Vector.Add(v1, v2)
        Console.WriteLine($"v1 + v2 = {v:F4}")
        v = Vector.Subtract(v1, v2)
        Console.WriteLine($"v1 - v2 = {v:F4}")
        ' Vectors can only be multiplied or divided by
        ' a real number. For dot products, see the
        ' DotProduct method.
        v = Vector.Multiply(5, v1)
        Console.WriteLine($"5 * v1 = {v:F4}")

        ' You can also apply these methods to Vector objects.
        ' In this case, they change the first operand.
        Console.WriteLine($"v3 = {v3:F4}")
        v3.AddInPlace(v1)
        ' Note that this is different from the += operator!
        ' The += operator creates a new Vector object,
        ' whereas the Add method above does not.
        Console.WriteLine($"v3+v1 -> v3 = {v3:F4}")
        ' This method is overloaded so you can directly
        ' add a scaled vector:
        v3.AddScaledInPlace(-2, v1)
        Console.WriteLine($"v3-2v1 -> v3 = {v3:F4}")
        Console.WriteLine()

        '
        ' Norms, dot products, etc.
        '
        Console.WriteLine("Norms, dot products, etc.")
        ' The dot product is calculated in one of two ways:
        ' Using the static DotProduct method:
        Dim a As Double = Vector.DotProduct(v1, v2)
        ' Or using the DotProduct method on one of the two
        ' vectors:
        Dim b As Double = v1.DotProduct(v2)
        Console.WriteLine("DotProduct(v1, v2) = {0} = {0:F4}",
                a, b)
        ' The Norm method returns the standard two norm
        ' of a Vector:
        a = v1.Norm()
        Console.WriteLine($"|v1| = {a}")
        ' .the Norm method is overloaded to allow other norms,
        ' including the one-norm:
        a = v1.Norm(1)
        Console.WriteLine($"one norm(v1) = {a}")
        ' ...the positive infinity norm, which returns the
        ' absolute value of the largest component:
        a = v1.Norm(Double.PositiveInfinity)
        Console.WriteLine($"+inf norm(v1) = {a}")
        ' ...the negative infinity norm, which returns the
        ' absolute value of the smallest component:
        a = v1.Norm(Double.NegativeInfinity)
        Console.WriteLine($"-inf norm(v1) = {a}")
        ' ...and even the zero norm, which simply returns
        ' the number of components of the vector:
        a = v1.Norm(0)
        Console.WriteLine($"zero-norm(v1) = {a}")
        ' You can get the square of the two norm with the
        ' NormSquared method.
        a = v1.NormSquared()
        Console.WriteLine($"|v1|^2 = {a}")
        Console.WriteLine()

        '
        ' Largest and smallest elements
        '
        ' The Vector class defines methods to find the
        ' largest or smallest element or its index.
        Console.WriteLine($"v2 = {v2:F4}")
        ' The Max method returns the largest element:
        Console.WriteLine($"Max(v2) = {v2.Max()}")
        ' The AbsoluteMax method returns the element with
        ' the largest absolute value.
        Console.WriteLine("Absolute max(v2) = {0}",
                v2.AbsoluteMax())
        ' The Min method returns the smallest element:
        Console.WriteLine($"Min(v2) = {v2.Min()}")
        ' The AbsoluteMin method returns the element with
        ' the smallest absolute value.
        Console.WriteLine("Absolute min(v2) = {0}",
                v2.AbsoluteMin())
        ' Each of these methods has an equivalent method
        ' that returns the zero-based index of the element
        ' instead of its value, for example:
        Console.WriteLine("Index of Min(v2) = {0}",
                v2.MinIndex())

        ' Finally, the Map method lets you apply
        ' an arbitrary function to each element of the
        ' vector:
        v3.Map(AddressOf Math.Exp)
        Console.WriteLine($"Exp(v3) = {v3:F4}")
        ' There is also a static method that returns a
        ' new Vector object:
        v = Vector.Map(AddressOf Math.Exp, v3)

    End Sub

End Module
