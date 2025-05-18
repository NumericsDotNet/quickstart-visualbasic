'=====================================================================
'
'  File: indexes-and-labels.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET.DataAnalysis
Imports Numerics.NET

' Illustrates how to use indexes to label the elements
' of a vector, or the rows and columns of a matrix.
Module IndexesAndLabels

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Indexes
        '

        ' An index Is a set of keys that can be used
        ' to label one Or more dimensions of a vector,
        ' matrix, Or data frame.

        '
        ' Construction
        '

        ' The simplest way to create an index Is from an array
        Dim index1 = Index.Create({"a", "b", "c", "d"})
        ' We can then assign this to the Index property of a vector
        Dim v = Vector.Create({1.0, 2.0, 3.0, 4.0})
        v.Index = index1
        Console.WriteLine(v)

        ' An index by position Is very common,
        ' And can be created efficiently using the
        ' Default method
        Dim numbers = Index.Default(10) ' 0, 1, ..., 9
        Dim numbers2 = Index.Default(10, 20) ' 10, 11, ..., 19

        ' Various options exist to create indexes over date ranges,
        ' for example
        Dim dateIndex = Index.CreateDateRange(New DateTime(2015, 4, 25), 10)
        ' 2015/4/25, 2015/4/26, ..., 2015/5/4

        ' Finally, for some purposes it may be useful to create
        ' an index of intervals, for example when you want to
        ' categorize people into age groups
        Dim ages = {0, 18, 35, 65}
        Dim ageGroups = Index.CreateBins(ages, SpecialBins.AboveMaximum)

        '
        ' Properties
        '

        ' Indexes have a length
        Console.WriteLine($"# of keys in index: {index1.Length}")
        ' Indexes usually have unique elements.
        Console.WriteLine($"Keys are unique? {index1.IsUnique}")
        ' The elements may be sorted Or Not.
        Console.WriteLine($"Keys are sorted? {index1.IsSorted}")
        Console.WriteLine($"Sort order: {index1.SortOrder}")

        '
        ' Lookup
        '

        ' Once created, you can look up the position of a key
        Dim position = index1.Lookup("c") '= 2
        If index1.TryLookup("e", position) Then
            Console.WriteLine("We shouldn't be here.")
        End If

        ' You can also look up the nearest date.
        Dim dates = Index.CreateDateRange(DateTime.Today.AddDays(-5), 10)
        Dim rightNow = DateTime.Now
        ' An exact lookup fails in this case
        If Not dates.TryLookup(rightNow, position) Then
            Console.WriteLine("Exact lookup failed.")
        End If
        ' But looking for the nearest key works fine
        position = dates.LookupNearest(rightNow, Direction.Backward) '= 5
        position = dates.LookupNearest(rightNow, Direction.Forward) '= 6

        '
        ' Automatic alignment
        '

        ' One of the useful features of indexes Is that
        ' values are aligned on key values automatically.
        ' For example, given two vectors
        Dim a = Vector.Create(
                {1.0, 2.0, 3.0, 4.0},
                {"a", "b", "c", "d"})
        Dim b = Vector.Create(
                {10.0, 30.0, 40.0, 50.0},
                {"a", "c", "d", "e"})
        ' We can compute their sum
        Console.WriteLine(a + b)
        ' And we find that elements are added
        ' when they have the same key,
        ' Not when they have the same position.

        ' Indexes also propagate through calculations
        Console.WriteLine($"Exp(a) = " + Environment.NewLine + "{Vector.Exp(a)}")
        Console.WriteLine($"a[a mod 2 = 0] =" + Environment.NewLine + "{a(Function(x) x Mod 2 = 0)}")

        ' Matrices can have a row and/or a column index
        Dim c = Matrix.CreateRandom(100, 4)
        c.ColumnIndex = Index.Create({"a", "b", "c", "d"})
        Dim cTc = c.Transpose() * c
        Console.WriteLine($"C^T*C = " + Environment.NewLine + "{cTc.Summarize()}")


    End Sub

End Module
