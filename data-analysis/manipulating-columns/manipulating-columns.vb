'=====================================================================
'
'  File: manipulating-columns.vb
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

' Illustrates how to transform And manipulate the columns
' of a data frame.
Module ManipulatingColumns

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Let's start with a data frame with a DateTime index:
        Const rowCount = 1000
        Dim dates = Index.CreateDateRange(New DateTime(2016, 1, 17), rowCount, Recurrence.Daily)
        Dim frame = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                    {"values1", Vector.CreateRandom(rowCount)},
                    {"values2", Vector.CreateRandom(rowCount)}},
                    dates)
        Console.WriteLine(frame.Head())

        ' The columns of a data frame are immutable,
        ' but the collection of columns Is Not.

        ' We can add columns
        frame.AddColumn("vzlues3", Vector.CreateRandom(rowCount))
        frame.AddColumn("values4", Vector.CreateRandom(rowCount))
        frame.AddColumn("values6", Vector.CreateRandom(rowCount))
        Console.WriteLine(frame.Head())
        ' Rename columns
        frame = frame.RenameColumn("values4", "vzlues5")
        frame = frame.RenameColumns(
                Function(s) s.StartsWith("vzlues"),
                Function(s) "values" + s.Substring(6))
        Console.WriteLine(frame.Head())
        ' And remove columns
        frame.RemoveColumn("values5")
        frame.RemoveColumnAt(2)
        Console.WriteLine(frame.Head())

        ' You can transform a column And add the result
        ' in various places:
        ' As the last column
        frame.MapAndAppend(Of Double)("values1", Function(x) Vector.Cos(x), "cosValues1")
        ' After a specific column:
        frame.MapAndInsertAfter(Of Double)("values1", Function(x) Vector.Sin(x), "sinValues1")
        ' Replacing the column
        frame.MapAndReplace(Of Double)("values6", Function(x) Vector.Exp(x), "expValues6")
        Console.WriteLine(frame.Head())

        ' The same operations can be performed on multiple columns
        ' at once:
        Dim columns = {"values1", "values2"}
        ' We can supply the keys for the new columns explicitly:
        Dim negColumns = {"-values1", "-values2"}
        frame.MapAndAppend(Of Double)(columns, Function(x) -x, negColumns)
        ' or as a function of the original key:
        frame.MapAndInsertAfter(Of Double)(columns, Function(x) 2.0 * x, Function(s) "2*" + s)
        Console.WriteLine(frame.Head())

        ' A more complex example: replace missing values
        ' with the mean of a group.

        ' We create a categorical variable with 5 categories
        ' so we will have 5 group means.
        Dim group = frame.GetColumn("values1").Bin(5)
        ' and a variable that has some missing values:
        Dim withNAs = frame.GetColumn("values2").Clone _
                .SetValues(Double.NaN, Function(x) x < 0.15)
        Console.WriteLine(withNAs.GetSlice(0, 12))
        ' Now for the actual calculation, which has 3 steps:
        ' First, we compute the means for each group:
        Dim meansPerGroup = withNAs.AggregateBy(group, Aggregators.Mean)
        Console.WriteLine(meansPerGroup)
        ' Next, create a vector with the means of the group
        ' that each element belongs to:
        Dim means = group.WithCategories(meansPerGroup)
        ' Next, we replace the missing values with the corresponding
        ' elements from that vector.
        Dim withNAsReplaced = withNAs.ReplaceMissingValues(means)
        Console.WriteLine(withNAsReplaced.GetSlice(0, 12))

        '
        ' Row-based operations
        '

        ' Data frames are column-based data structures.
        ' Even though it is not recommended, it is possible
        ' to perform operations on rows:

        frame.AddColumn("values3", Vector.CreateRandom(rowCount))
        Dim avg1 = Vector.Create(Of Double)(frame.RowCount)
        Dim i = 0
        For Each row In frame.Rows
            avg1(i) = (row.Get(Of Double)("values1") +
                        row.Get(Of Double)("values2") +
                        row.Get(Of Double)("values3")) / 3
            i = i + 1
        Next
        frame.AddColumn("Average", avg1)

        ' Performing the operation directly on the columns
        ' is much more efficient:
        Dim avg2 = (frame.GetColumn("values1") +
                     frame.GetColumn("values2") +
                     frame.GetColumn("values3")) / 3.0
        frame.AddColumn("Average2", avg2)


    End Sub

End Module
