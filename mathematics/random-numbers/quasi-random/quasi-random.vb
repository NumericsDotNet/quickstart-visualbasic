'=====================================================================
'
'  File: quasi-random.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Imports Numerics.NET
Imports Numerics.NET.Random

    ' Illustrates the use of quasi-random sequences by computing
    ' a multi-dimensional integral.
    Module QuasiRandomSequences

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample demonstrates the use of
        ' quasi-random sequences by computing
        ' a multi-dimensional integral.

        ' We will use one million points.
        Dim length As Integer = 1000000
        ' The number of dimensions:
        Dim dimension As Integer = 5

        ' We will evaluate the function
        '
        '    Product(i = 1 -> # dimensions) |4 x(i) - 2|
        '
        ' over the hypercube 0 <= x(i) <= 1. The value of this integral
        ' is exactly 1.

        ' Create the sequence
        Dim sequence = QuasiRandom.HaltonSequence(dimension, length)

        ' Compute the integral by summing over all points
        Console.WriteLine("# iter.  Estimate")
        Dim sum As Double = 0.0
        Dim i As Integer = 0
        For Each point As Vector(Of Double) In sequence
            If i Mod 100000 = 0 Then
                Console.WriteLine("{0,6}  {1,8:F4}", i, sum / i)
            End If

            ' Evaluate the integrand
            Dim functionValue As Double = 1.0
            For j As Integer = 0 To dimension - 1
                functionValue *= Math.Abs(4.0 * point(j) - 2.0)
            Next
            sum += functionValue
            i = i + 1
        Next
        Console.WriteLine($"Final estimate: {sum / length,8:F4}")
        Console.WriteLine("Exact value: 1.0000")

        ' Sobol sequences require more data And more initialization.
        ' Fortunately, different sequences of the same dimension
        ' can share much of the work And storage. The
        ' SobolSequenceGenerator class should be used in this case

        Dim skip As Integer = 1000
        Dim sobol As SobolSequenceGenerator = New SobolSequenceGenerator(dimension, length + skip)
        ' Sobol sequences are more flexible: they let you skip
        ' a number of points at the start of the sequence.
        ' The cost of skipping points Is O(1).
        i = 0
        sum = 0.0
        For Each point As Vector(Of Double) In sobol.Generate(length, skip)
            If i Mod 100000 = 0 Then
                Console.WriteLine("{0,6}  {1,8:F4}", i, sum / i)
            End If

            ' Evaluate the integrand
            Dim functionValue As Double = 1.0
            For j As Integer = 0 To dimension - 1
                functionValue *= Math.Abs(4.0 * point(j) - 2.0)
            Next
            sum = sum + functionValue
            i = i + 1
        Next
        ' Print the final result.
        Console.WriteLine($"Final estimate: {sum / length,8:F4}")
        Console.WriteLine("Exact value: 1.0000")


    End Sub

End Module
